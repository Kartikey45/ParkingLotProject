using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IParkingLotRL
    {
        //Method to Add User
        UserDetails AddUser(UserDetails user);

        //Method to login user
        UserLogin Login(UserLogin user);
    }
}
