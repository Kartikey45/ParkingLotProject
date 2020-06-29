using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Interface;
using CommonLayer.ParkingLimitForVehical;
using CommonLayer.ParkingModel;
using CommonLayer.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ParkingLotProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingController : ControllerBase
    {
        public readonly IVehicalParkingDetailsBL parkingLotBusiness;

        public ParkingController(IVehicalParkingDetailsBL _parkingLotBusiness)
        {
            parkingLotBusiness = _parkingLotBusiness;
        }

        //Method to get all car parking details
        [HttpGet]
        [Authorize(Roles = "Owner, Police, Security")]
        public ActionResult GetAllParkingCarsDetails()
        {
            try
            {
                var data = parkingLotBusiness.GetAllParkingCarsDetails();
                bool success;
                string message;
                if (data == null)
                {
                    success = false;
                    message = "Failed to Get All Car Parking Details";
                    return Ok(new { success, message });
                }
                else
                {
                    success = true;
                    message = "Successfull to Get All Car Parking Details";
                    return Ok(new { success, message, data });
                }
            }
            catch (Exception e)
            {
                bool success = false;
                string message = e.Message;
                return BadRequest(new { success, message });
            }
        }

        //Method to park the car in parking lot
        [HttpPost]
        [Route("")]
        public IActionResult ParkingCarInLot(ParkingInformation Details)
        {
            try
            {

                var data = parkingLotBusiness.ParkingCarInLot(Details);
                var count = parkingLotBusiness.ParkingLotStatus();
                bool success = false;
                string message;
                if (data == null)
                {
                    success = false;
                    message = "Parking Lot is Full";
                    return Ok(new { success, message });
                }
                else
                {
                    success = true;
                    message = "Car Parked successfully";
                    return Ok(new { success, message, data });

                }
            }
            catch (Exception e)
            {
                bool success = false;
                string message = e.Message;
                return BadRequest(new { success, message });
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
                bool success = false;
                string message;
                if (data == null)
                {
                    success = false;
                    message = "Failed To Delete";
                    return Ok(new { success, message });
                }
                else
                {
                    success = true;
                    message = "Deleted successfully";
                    return Ok(new { success, message, data });
                }
            }
            catch (Exception e)
            {
                bool success = false;
                string message = e.Message;
                return BadRequest(new { success, message });
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
                bool success;
                string message;
                if (data.Equals(Limit.TotalParkingLimit))
                {
                    success = false;
                    message = "Parking Full";
                    return Ok(new { success, message });
                }
                else
                {
                    success = true;
                    message = "Parking Avaliable";
                    return Ok(new { success, message, data });
                }
            }
            catch (Exception e)
            {
                bool success = false;
                string message = e.Message;
                return BadRequest(new { success, message });
            }
        }

        //Method to unpark the car
        [HttpPut]
        [Route("Unpark/{ID}")]
        public ActionResult CarUnPark(int ID)
        {
            try
            {
                var data = parkingLotBusiness.CarUnPark(ID);
                bool success;
                string message;
                if (data != null)
                {
                    success = true;
                    message = "Successfully UnPark";
                    return Ok(new { success, message, data });
                }
                else
                {
                    success = false;
                    message = "Fail To UnPark";
                    return Ok(new { success, message });
                }
            }
            catch (Exception e)
            {
                bool sucess = false;
                string message = e.Message;
                return BadRequest(new { sucess, message });
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
                bool success;
                string message;
                if (data == null)
                {
                    success = false;
                    message = "Failed to fetch details";
                    return Ok(new { success, message });
                }
                else
                {
                    success = true;
                    message = "UnParked cars details fetched successfully";
                    return Ok(new { success, message, data });
                }
            }
            catch (Exception e)
            {
                bool success = false;
                string message = e.Message;
                return BadRequest(new { success, message });
            }
        }

        //Method to get details by vehical number
        [Authorize(Roles = "Owner,Police,Driver")]
        [HttpGet]
        [Route("Number")]
        public ActionResult GetCarDetailsByVehicleNumber(string Number)
        {
            try
            {
                var data = parkingLotBusiness.GetCarDetailsByVehicleNumber(Number);
                bool success = false;
                string message;
                if (data != null)
                {
                    success = true;
                    message = "Details found ";
                    return Ok(new { success, message, data });
                }
                return Ok();
            }
            catch (Exception)
            {
                bool success = false;
                string message = "Not Found";
                return BadRequest(new { success, message });
            }
        }

        //Method to get details by parking slot
        [Authorize(Roles = "Owner,Police")]
        [HttpGet]
        [Route("Slot")]
        public ActionResult GetCarDetailsByParkingSlot(string Slot)
        {
            try
            {
                var data = parkingLotBusiness.GetCarDetailsByParkingSlot(Slot);
                bool success = false;
                string message;
                if (data != null)
                {
                    success = true;
                    message = "Details found ";
                    return Ok(new { success, message, data });
                }
                return Ok();
            }
            catch (Exception)
            {
                bool success = false;
                string message = "Not Found";
                return BadRequest(new { success, message });
            }
        }

        //Method to get details by vehical brand
        [Authorize(Roles = "Owner,Police")]
        [HttpGet]
        [Route("brand")]
        public ActionResult GetCarDetailsByVehicleBrand(string brand)
        {
            try
            {
                var data = parkingLotBusiness.GetCarDetailsByVehicleBrand(brand);
                bool success = false;
                string message;
                if (data != null)
                {
                    success = true;
                    message = "Details found";
                    return Ok(new { success, message, data });
                }
                return Ok();
            }
            catch (Exception)
            {
                bool success = false;
                string message = "Not Found";
                return BadRequest(new { success, message });
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
                var data = parkingLotBusiness.GetAllCarDetailsOfHandicap();
                bool success = false;
                string message;
                if (data == null)
                {
                    success = false;
                    message = "No Details";
                    return Ok(new { success, message });
                }
                else
                {
                    success = true;
                    message = "Successfully Got Details";
                    return Ok(new { success, message, data });
                }
            }
            catch (Exception)
            {
                bool success = false;
                string message = "Not Found";
                return BadRequest(new { success, message });
            }
        }

        //Method to get all car details  by color
        [Authorize(Roles = "Owner,Police")]
        [HttpGet]
        [Route("Color")]
        public ActionResult GetAllCarDetailsByColor(string Color)
        {
            try
            {
                var data = parkingLotBusiness.GetAllCarDetailsByColor(Color);
                bool success = false;
                string message;
                if (data != null)
                {
                    success = true;
                    message = "Details found";
                    return Ok(new { success, message, data });
                }
                return Ok();
            }
            catch (Exception)
            {
                bool success = false;
                string message = "Not Found";
                return BadRequest(new { success, message });
            }
        }
    }
}
