using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Interface;
using CommonLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.DBContext;
using Microsoft.AspNetCore.Authorization;

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
            this._configuration = _configuration;
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
                    RegistrationResponse Data = new RegistrationResponse
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
                        Data
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

        //method for user login
        [Route("Login")]
        [HttpPost]
        public IActionResult LoginUser(UserLogin user)
        {
            try
            {
                UserLogin data = _BusinessLayer.Login(user);

                if (data != null)
                {
                    LoginResponse Data = new LoginResponse
                    {
                        Email = user.Email,
                       
                    };
                    var success = true;
                    var Message = "Login successfull ";
                    string JsonToken = CreateToken(data , "Login");
                    return Ok(new { success, Message, Data, JsonToken });                  
                }
                else
                {
                    var success = false;
                    var Message = "Login Failed";
                    return BadRequest(new { success, Message });                             
                }
            }
            catch(Exception exception)
            {
                var success = false;
                var Message = "Login Failed";
                return BadRequest(new { success, error = exception.Message, Message });
            }
        }

        //Method to delete user details
        [Authorize(Roles = "Owner")]
        [HttpDelete]
        [Route("")]
        public IActionResult DeleteUserRecord(int UserID)
        {
            try
            {
                var data = _BusinessLayer.DeleteUserRecord(UserID);
                bool success = false;
                string message;
                if (data != null)
                {
                    success = true;
                    message = "Deleted successfullly";
                    return Ok(new { success, message, data });

                }
                else
                {
                    success = false;
                    message = "Failed To Delete";
                    return BadRequest(new { success, message });
                }
            }
            catch(Exception exception)
            {
                var success = false;
                var Message = "Failed to delete";
                return BadRequest(new { success, error = exception.Message, Message });
            }
        }

        //Method to Update user data by UserId
        [Authorize(Roles = "Owner")]
        [Route("")]
        [HttpPut]
        public IActionResult UpdateUserRecord(int UserId, UserDetails details)
        {
            try
            {

                var data = _BusinessLayer.UpdateUserRecord(UserId, details);

                if (!data.Equals(null))
                {
                    var success = true;
                    var Message = "Updated Successfull";
                    return Ok(new
                    {
                        success,
                        Message,
                        data
                    });
                }
                else
                {
                    var success = false;
                    var Message = "Failed to update data";
                    return this.BadRequest(new { success, Message });
                }
            }
            catch(Exception exception)
            {
                var success = false;
                var Message = "Failed to update data";
                return BadRequest(new { success, error = exception.Message, Message });
            }
        }

        //Method to create JWT token
        private string CreateToken(UserLogin responseData, string type)
        {
            try
            {
                var symmetricSecuritykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signingCreds = new SigningCredentials(symmetricSecuritykey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>();
                //claims.Add(new Claim(ClaimTypes.Role, type));
                claims.Add(new Claim(ClaimTypes.Role, responseData.UserTypes));
                claims.Add(new Claim("Email", responseData.Email.ToString()));
                claims.Add(new Claim("Password", responseData.Password.ToString()));

                var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                    _configuration["Jwt:Issuer"],
                    claims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: signingCreds);
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
