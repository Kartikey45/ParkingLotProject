using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IParkingLotBL
    {

        //Method to register User
        UserDetails register(UserDetails user);

        //Method to login user
        UserLogin Login(UserLogin user);
    }
}
