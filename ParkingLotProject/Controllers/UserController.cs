using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Interface;
using CommonLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.DBContext;

namespace ParkingLotProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //references of layers
        readonly IParkingLotBL _BusinessLayer;
        private readonly IConfiguration _configuration;
        private ParkingLotDbContext dBContext;

        //constructor
        public UserController(IParkingLotBL _BusinessDependencyInjection, IConfiguration _configuration, ParkingLotDbContext dBContext)
        {
            _BusinessLayer = _BusinessDependencyInjection;
            this._configuration = _configuration;
            this.dBContext = dBContext;
        }

        //method to register new user
        [Route("Register")]
        [HttpPost]
        public IActionResult RegisterUser( UserDetails user)
        {
            try
            {
                string password = user.Password;
                UserDetails data = _BusinessLayer.register(user);                    
                if (!data.Equals(null))
                {
                    var success = true;
                    var Message = "Registration Successfull";
                    RegistrationResponse responseData = new RegistrationResponse
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        UserType = user.UserType
                    };
                   
                    return Ok(new
                    {
                        success,
                        Message,
                        responseData
                    });                                                           

                }
                else
                {
                    var success = false;
                    var Message = "Registration Failed";
                    return this.BadRequest(new { success, Message });
                }
            }
            catch (Exception e)
            {
                var success = false;
                var Message = "Registration Failed";
                return BadRequest(new { success, error = e.Message, Message });
            }
        }
    }
}
