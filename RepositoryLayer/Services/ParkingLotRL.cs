using CommonLayer;
using RepositoryLayer.DBContext;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class ParkingLotRL : IParkingLotRL
    {
        //db context reference
        private ParkingLotDbContext dbContext;

        //Constructor
        public ParkingLotRL(ParkingLotDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //method to register user
        public UserDetails AddUser(UserDetails user)
        {
            try
            {
                //validating UserType
                bool inputUserType = Enum.TryParse<UserTypes.User>(user.UserType, true, out UserTypes.User type);
                if (inputUserType != true)
                {
                    throw new Exception("Invalid User Category");
                }

                //variable declared
                string Email = user.Email;

                //validation of email registration
                var Validation = dbContext.UserDetails.Where(v => v.Email == Email).FirstOrDefault();
                if (Validation != null)
                {
                    throw new Exception("User Already Exist");
                }

                //model class attributes
                UserDetails details = new UserDetails
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    UserType = user.UserType,
                    Password = user.Password
                    
                };

                //Adding new user data in the database
                var Result = dbContext.UserDetails.Add(details);

                //save changes in database
                dbContext.SaveChanges();

                //return valid user data
                if(Result != null)
                {
                    return user;
                }
                else
                {
                    throw new Exception("User Not Registered");
                }
            }
            catch(Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        //Method to delete User Record by ID
        public object DeleteUserRecord(int UserID)
        {
            try
            {
                //
                var record = dbContext.UserDetails.First(v => v.ID == UserID);

                if (record != null)
                {
                    dbContext.UserDetails.Remove(record);

                    dbContext.SaveChanges();

                    return record;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch(Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public UserLogin Login(UserLogin user)
        {
            try
            {
                //validating UserType
                bool inputUserType = Enum.TryParse<UserTypes.User>(user.UserTypes, true, out UserTypes.User type);
                if (inputUserType != true)
                {
                    throw new Exception("Invalid User Category");
                }

                //variable declared
                string Email = user.Email;

                //password encrypted
                string Password = EncryptedPassword.EncodePasswordToBase64(user.Password);

                //User category 
                string UserType = user.UserTypes;

                //Validating Login details
                var Result = dbContext.UserDetails.Where(v => v.Email == Email && v.Password == Password && v.UserType == UserType).FirstOrDefault();
                if (Result != null)
                {
                    return user;
                }
                else
                {
                    throw new Exception("Login failed");
                }
            }
            catch(Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }


    }
}
