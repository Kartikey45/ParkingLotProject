using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Interface;
using CommonLayer.ParkingLimitForVehical;
using CommonLayer.ParkingModel;
using CommonLayer.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace ParkingLotProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingController : ControllerBase
    {
        public readonly IVehicalParkingDetailsBL parkingLotBusiness;
        private readonly IDistributedCache distributedCache;

        public ParkingController(IVehicalParkingDetailsBL _parkingLotBusiness, IDistributedCache distributedCache)
        {
            parkingLotBusiness = _parkingLotBusiness;
            this.distributedCache = distributedCache;
        }

        //Method to get all car parking details
        [HttpGet]
        [Authorize(Roles = "Owner, Police, Security")]
        public ActionResult GetAllParkingCarsDetails()
        {
            try
            {
                var cacheKey = "parking";

                List<ParkingLotDetails> list;
                string serializedList;

                var encodedList = distributedCache.Get(cacheKey);

                if (encodedList != null)
                {
                    serializedList = Encoding.UTF8.GetString(encodedList);
                    list = JsonConvert.DeserializeObject<List<ParkingLotDetails>>(serializedList);
                }
                else
                {
                    list = parkingLotBusiness.GetAllParkingCarsDetails();
                    serializedList = JsonConvert.SerializeObject(list);
                    encodedList = Encoding.UTF8.GetBytes(serializedList);
                    var options = new DistributedCacheEntryOptions()
                                    .SetSlidingExpiration(TimeSpan.FromMinutes(20))
                                    .SetAbsoluteExpiration(DateTime.Now.AddHours(6));
                    distributedCache.Set(cacheKey, encodedList, options);
                }

                if (list == null)
                {
                    return Ok(new { success = false, message = "Parking Lot is Empty" });
                }
                else
                {
                    return Ok(new { success = true, message = "Successfull to Get All Car Parking Details", data = list });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { success = false, message = e.Message });
            }
        }

        //Method to park the car in parking lot
        [HttpPost]
        [Route("")]
        public IActionResult ParkingCarInLot(ParkingInformation Details)
        {
            try
            {
                string cacheKey = "parking";
                var data = parkingLotBusiness.ParkingCarInLot(Details);
                var count = parkingLotBusiness.ParkingLotStatus();
                
                if (data == null)
                {
                    return Ok(new { success = false, message = "Parking Lot is Full" });
                }
                else
                {
                    distributedCache.Remove(cacheKey);
                    return Ok(new { success = true, message = "Car parked successfully", Data = data });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { success = false, message = e.Message });
            }
        }

        //Method to Delete parking details
        [HttpDelete]
        [Route("{ID}")]
        [Authorize(Roles = "Owner")]
        public ActionResult DeleteCarParkingDetails(int ID)
        {
            try
            {
                var data = parkingLotBusiness.DeleteCarParkingDetails(ID);
                
                if (data == null)
                {
                    return Ok(new { success = false, message = "failed to delete" });
                }
                else
                {
                    return Ok(new { success = true, message = "deleted successfully", Data = data });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { success = false, message = e.Message });
            }
        }

        //Method to return Parking status
        [HttpGet]
        [Route("Status")]
        [Authorize(Roles = "Owner,Security")]
        public ActionResult ParkingLotStatus()
        {
            try
            {

                //Instance of ParkingLimit
                ParkingLimit Limit = new ParkingLimit();

                var data = parkingLotBusiness.ParkingLotStatus();
               
                if (data.Equals(Limit.TotalParkingLimit))
                {
                    return Ok(new { success = true, message = "Parking Full" });
                }
                else
                {
                    return Ok(new { success = true , message = "Parking Avaliable", Data = data });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { success = false, message = e.Message });
            }
        }

        //Method to unpark the car
        [HttpPut]
        [Route("{ID}/Unpark")]
        public ActionResult CarUnPark(int ID)
        {
            try
            {
                string CacheKey = "parking";
                var data = parkingLotBusiness.CarUnPark(ID);
                
                if (data != null)
                {
                    distributedCache.Remove(CacheKey);
                    return Ok(new { success = true, message = "successfully unparked", Data = data });
                }
                else
                {
                    return Ok(new { success = false, message = "failed to unpark" });
                }
            }
            catch (Exception e)
            {
                return Ok(new { success = false, message = e.Message });
            }
        }

        //Method to get Unparkd cars details
        [HttpGet]
        [Route("Unparked")]
        [Authorize(Roles = "Owner, Police, Security")]
        public ActionResult GetAllUnParkedCarDetail()
        {
            try
            {
                var data = parkingLotBusiness.GetAllUnParkedCarDetail();
            
                if (data == null)
                {
                    return Ok(new { success = false, message = "data not found" });
                }
                else
                {
                    return Ok(new { success = true, message = "details fetched successfully", Data = data });
                }
            }
            catch (Exception e)
            {
                return Ok(new { success = false, message = e.Message});
            }
        }

        //Method to get details by vehical number
        [Authorize(Roles = "Owner,Police,Driver")]
        [HttpGet]
        [Route("Vehical/{Number}")]
        public ActionResult GetCarDetailsByVehicleNumber(string Number)
        {
            try
            {
                var cacheKey = Number;

                List<ParkingLotDetails> list;
                string serializedList;

                var encodedList = distributedCache.Get(cacheKey);

                if (encodedList != null)
                {
                    serializedList = Encoding.UTF8.GetString(encodedList);
                    list = JsonConvert.DeserializeObject<List<ParkingLotDetails>>(serializedList);
                }
                else
                {
                    list = parkingLotBusiness.GetCarDetailsByVehicleNumber(Number);
                    serializedList = JsonConvert.SerializeObject(list);
                    encodedList = Encoding.UTF8.GetBytes(serializedList);
                    var options = new DistributedCacheEntryOptions()
                                    .SetSlidingExpiration(TimeSpan.FromMinutes(20))
                                    .SetAbsoluteExpiration(DateTime.Now.AddHours(6));
                    distributedCache.Set(cacheKey, encodedList, options);
                }

                if (list == null)
                {
                    return Ok(new { success = false, message = "details not found" });
                }
                else
                {
                    return Ok(new { success = true, message = "details found successfully", Data = list });
                }
            }
            catch (Exception exception)
            {
                return Ok(new { success = false, message = exception.Message });
            }
        }

        //Method to get details by parking slot
        [Authorize(Roles = "Owner,Police")]
        [HttpGet]
        [Route("Slot/{Slot}")]
        public ActionResult GetCarDetailsByParkingSlot(string Slot)
        {
            try
            {
                var cacheKey = Slot;

                List<ParkingLotDetails> list;
                string serializedList;

                var encodedList = distributedCache.Get(cacheKey);

                if (encodedList != null)
                {
                    serializedList = Encoding.UTF8.GetString(encodedList);
                    list = JsonConvert.DeserializeObject<List<ParkingLotDetails>>(serializedList);
                }
                else
                {
                    list = parkingLotBusiness.GetCarDetailsByParkingSlot(Slot);
                    serializedList = JsonConvert.SerializeObject(list);
                    encodedList = Encoding.UTF8.GetBytes(serializedList);
                    var options = new DistributedCacheEntryOptions()
                                    .SetSlidingExpiration(TimeSpan.FromMinutes(20))
                                    .SetAbsoluteExpiration(DateTime.Now.AddHours(6));
                    distributedCache.Set(cacheKey, encodedList, options);
                }

                if (list == null)
                {
                    return Ok(new { success = false, message = "details not found"});
                }
                else
                {
                    return Ok(new { success = true, message = "details found successfully", Data = list });
                }
            }
            catch (Exception exception)
            {
                return Ok(new { success = true, message = exception.Message });
            }
        }

        //Method to get details by vehical brand
        [Authorize(Roles = "Owner,Police")]
        [HttpGet]
        [Route("Brand/{VehicalBrand}")]
        public ActionResult GetCarDetailsByVehicleBrand(string VehicalBrand)
        {
            try
            {
                var cacheKey = VehicalBrand;

                List<ParkingLotDetails> list;
                string serializedList;

                var encodedList = distributedCache.Get(cacheKey);

                if (encodedList != null)
                {
                    serializedList = Encoding.UTF8.GetString(encodedList);
                    list = JsonConvert.DeserializeObject<List<ParkingLotDetails>>(serializedList);
                }
                else
                {
                    list = parkingLotBusiness.GetCarDetailsByVehicleBrand(VehicalBrand);
                    serializedList = JsonConvert.SerializeObject(list);
                    encodedList = Encoding.UTF8.GetBytes(serializedList);
                    var options = new DistributedCacheEntryOptions()
                                    .SetSlidingExpiration(TimeSpan.FromMinutes(20))
                                    .SetAbsoluteExpiration(DateTime.Now.AddHours(6));
                    distributedCache.Set(cacheKey, encodedList, options);
                }

                if (list == null)
                {
                    return Ok(new { success = false, message = "details not found" });
                }
                else
                {
                    return Ok(new { success = true, message = "details found successfully", Data = list });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, message = ex.Message });
            }
        }

        //Method to get handicap User car details
        [Authorize(Roles = "Owner,Police")]
        [HttpGet]
        [Route("Handicap")]
        public ActionResult GetAllCarDetailsOfHandicap()
        {
            try
            {
                string key = "handicap";
                var cacheKey = key;

                List<ParkingLotDetails> list;
                string serializedList;

                var encodedList = distributedCache.Get(cacheKey);

                if (encodedList != null)
                {
                    serializedList = Encoding.UTF8.GetString(encodedList);
                    list = JsonConvert.DeserializeObject<List<ParkingLotDetails>>(serializedList);
                }
                else
                {
                    list = parkingLotBusiness.GetAllCarDetailsOfHandicap();
                    serializedList = JsonConvert.SerializeObject(list);
                    encodedList = Encoding.UTF8.GetBytes(serializedList);
                    var options = new DistributedCacheEntryOptions()
                                    .SetSlidingExpiration(TimeSpan.FromMinutes(20))
                                    .SetAbsoluteExpiration(DateTime.Now.AddHours(6));
                    distributedCache.Set(cacheKey, encodedList, options);
                }

                if (list == null)
                {
                    return Ok(new { success = false, message = "No details found" });
                }
                else
                {
                    return Ok(new { success = true, message = "details found", Data = list });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, message = ex.Message });
            }
        }

        //Method to get all car details  by color
        [Authorize(Roles = "Owner,Police")]
        [HttpGet]
        [Route("Color/{Color}")]
        public ActionResult GetAllCarDetailsByColor(string Color)
        {
            try
            {
                var cacheKey = Color;

                List<ParkingLotDetails> list;
                string serializedList;

                var encodedList = distributedCache.Get(cacheKey);

                if (encodedList != null)
                {
                    serializedList = Encoding.UTF8.GetString(encodedList);
                    list = JsonConvert.DeserializeObject<List<ParkingLotDetails>>(serializedList);
                }
                else
                {
                    list = parkingLotBusiness.GetAllCarDetailsByColor(Color);
                    serializedList = JsonConvert.SerializeObject(list);
                    encodedList = Encoding.UTF8.GetBytes(serializedList);
                    var options = new DistributedCacheEntryOptions()
                                    .SetSlidingExpiration(TimeSpan.FromMinutes(20))
                                    .SetAbsoluteExpiration(DateTime.Now.AddHours(6));
                    distributedCache.Set(cacheKey, encodedList, options);
                }

                if (list == null)
                {
                    return Ok(new { success = false, message = "details not found" });
                }
                else
                {
                    return Ok(new { success = true, message = "details found", Data = list });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { success = true, message = ex.Message });
            }
        }
    }
}
