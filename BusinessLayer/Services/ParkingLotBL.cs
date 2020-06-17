using BusinessLayer.Interface;
using CommonLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class ParkingLotBL : IParkingLotBL
    {
        //reference of repository layer
        private IParkingLotRL parkingLot;

        //constructor 
        public ParkingLotBL(IParkingLotRL data)
        {
            this.parkingLot = data;
        }

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
    }
}
