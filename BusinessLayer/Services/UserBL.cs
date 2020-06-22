using BusinessLayer.Interface;
using CommonLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserBL : IUserBL
    {
        //reference of repository layer
        private IUserRL parkingLot;

        //constructor 
        public UserBL(IUserRL data)
        {
            this.parkingLot = data;
        }

        //Method to delete User details by its ID
        public object DeleteUserRecord(int UserID)
        {
            try
            {
                var data = parkingLot.DeleteUserRecord(UserID);
                if (data != null)
                {
                    return data;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception e)
            {
                // Exception
                throw new Exception(e.Message);

            }
        }

        //Method to Login User
        public UserLogin Login(UserLogin user)
        {
            try
            {
                var Result = parkingLot.Login(user);                               
                if (Result != null)
                {
                    return user;
                }
                else
                {
                    throw new Exception("Login Failed");
                }
            }
            catch(Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        //method to register new user
        public UserDetails register(UserDetails user)
        {
            try
            {
                string Encrypted = user.Password;
                user.Password = EncryptedPassword.EncodePasswordToBase64(Encrypted);
                var Result = parkingLot.AddUser(user);
                if(!Result.Equals(null))
                {
                    return user;
                }
                else
                {
                    throw new Exception("Not found");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        //Method to update user record by Id
        public object UpdateUserRecord(int UserId, UserDetails details)
        {
            try
            {
                var updateData = parkingLot.UpdateUserRecord(UserId, details);
                if (updateData != null)
                {
                    return updateData;
                }
                else
                {
                    throw new Exception("Record not updated");
                }
            }
            catch(Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}
