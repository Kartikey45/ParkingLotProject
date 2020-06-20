using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Interface;
using CommonLayer.ParkingModel;
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
                    return BadRequest(new { success, message });
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
    }
}
