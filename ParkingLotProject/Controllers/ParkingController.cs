﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Interface;
using CommonLayer.ParkingLimitForVehical;
using CommonLayer.ParkingModel;
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
        [Route("Park")]
        public IActionResult ParkingCarInLot(ParkingLotDetails details)
        {
            try
            {

                var data = parkingLotBusiness.ParkingCarInLot(details);
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
        [Route("Record")]
        [Authorize(Roles = "Owner")]
        public ActionResult DeleteCarParkingDetails(int ParkingID)
        {
            try
            {
                var data = parkingLotBusiness.DeleteCarParkingDetails(ParkingID);
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


        //Method to delete Unpark car details
        [HttpDelete]
        [Route("UnparkHistory")]
        [Authorize(Roles = "Owner")]
        public IActionResult DeleteUnparkHistoryByID(int UnparkCarId)
        {
            try
            {
                var data = parkingLotBusiness.DeleteUnparkHistory(UnparkCarId);
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
                    message = "Deleted Successfully";
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
        [HttpPost]
        [Route("UnPark")]
        public ActionResult CarUnPark(VehicalUnpark details)
        {
            try
            {
                var data = parkingLotBusiness.CarUnPark(details);
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
        [Route("UnParks")]
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
        [Route("VehicalNumber")]
        public ActionResult GetCarDetailsByVehicleNumber(string vehicleNumber)
        {
            try
            {
                var data = parkingLotBusiness.GetCarDetailsByVehicleNumber(vehicleNumber);
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
        [Route("Brand")]
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
        public ActionResult GetAllCarDetailsByColor(string VehicalColor)
        {
            try
            {
                var data = parkingLotBusiness.GetAllCarDetailsByColor(VehicalColor);
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
