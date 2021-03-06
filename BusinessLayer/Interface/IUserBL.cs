﻿using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IUserBL
    {

        //Method to register User
        UserRegistration register(UserRegistration user);

        //Method to login user
        UserLogin Login(UserLogin user);

        //Method to Delete User record
        object DeleteUserRecord(int UserID);

        //Method to update User record by Id
        Object UpdateUserRecord(int UserId, UserRegistration details);

        //Method to View All the records
        List<UserDetails> GetAllUserDetails();


    }
}
