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
        readonly IUserBL _BusinessLayer;
        private readonly IConfiguration _configuration;

        //Instance of Sender class
        Sender sender = new Sender();

        //constructor
        public UserController(IUserBL _BusinessDependencyInjection, IConfiguration _configuration)
        {
            _BusinessLayer = _BusinessDependencyInjection;
            this._configuration = _configuration;
        }

        //method to register new user
        [Route("Register")]
        [HttpPost]
        public IActionResult RegisterUser( UserRegistration user)
        {
            try
            {
                string password = user.Password;
                UserRegistration data = _BusinessLayer.register(user);                    
                if (!data.Equals(null))
                {
                    RegistrationResponse Data = new RegistrationResponse
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        UserType = user.UserType
                    };
                    
                    string MSMQ = "\n First Name : " + Convert.ToString(user.FirstName) + "\n Last Name : " + Convert.ToString(user.LastName) +
                                    "\n User Role : " + Convert.ToString(user.UserType) + 
                                    "\n Email : " + Convert.ToString(user.Email);
                    sender.Message(MSMQ);
                    return Ok(new { success = true, Message = "Registration Successfull", Data = data });                                                           
                }
                else
                {
                    return this.Ok(new { success = false, Message = "Registration Failed" });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { success = false , Message = e.Message });
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
                string JsonToken = CreateToken(data, "AuthenticateUserRole");
                if (data != null)
                {
                    LoginResponse Data = new LoginResponse
                    {
                        UserRole = user.UserTypes,
                        UserId = user.UserId,
                        Email = user.Email,
                        JwtToken = JsonToken
                    };
                   
                    return Ok(new { success = true, Message = "Login successfull", Data = Data });                  
                }
                else
                {
                    return Ok(new { success =false, Message = "Login Failed" });                             
                }
            }
            catch(Exception exception)
            {
                return BadRequest(new { success = false, Message = exception.Message });
            }
        }
        
        //Method to delete user details
        [Authorize(Roles = "Owner")]
        [HttpDelete]
        [Route("{UserID}")]
        public IActionResult DeleteUserRecord(int UserID)
        {
            try
            {
                var data = _BusinessLayer.DeleteUserRecord(UserID);
                if (data != null)
                {
                    return Ok(new { success = true, message = "Deleted succcessfully", data = data });
                }
                else
                {
                    return Ok(new { success = false, message = "Failed to delete" });
                }
            }
            catch(Exception exception)
            {
                return BadRequest(new { success = false, Message = exception.Message });
            }
        }

        //Method to Update user data by UserId
        [Authorize(Roles = "Owner")]
        [Route("{UserId}")]
        [HttpPut]
        public IActionResult UpdateUserRecord(int UserId, UserRegistration details)
        {
            try
            {
                var data = _BusinessLayer.UpdateUserRecord(UserId, details);

                if (!data.Equals(null))
                {
                    return Ok(new { success = true, message = "updated successfully", data = data });
                }
                else
                {
                    return Ok(new { success = false, message = "Failed to update data" });
                }
            }
            catch(Exception exception)
            {
                return BadRequest(new { success = false, Message = exception.Message });
            }
        }

        //Method to get all user details
        [Authorize(Roles = "Owner")]
        [HttpGet]
        public IActionResult GetAllUserDetails()
        {
            try
            {
                var data = _BusinessLayer.GetAllUserDetails();
                if (!data.Equals(null))
                {
                    return Ok(new { success = true, message = "Successfull", data = data });
                }
                else
                {
                    return Ok(new { success = false, message = "Failed" });
                }
            }
            catch(Exception e)
            {
                return BadRequest(new { success = false, Message = e.Message });
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
                claims.Add(new Claim(ClaimTypes.Role, responseData.UserTypes));
                claims.Add(new Claim("Email", responseData.Email.ToString()));
                claims.Add(new Claim("UserId", responseData.UserId.ToString()));
                claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                
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
