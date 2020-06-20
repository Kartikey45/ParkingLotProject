using CommonLayer.ParkingModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IVehicalParkingDetailsRL
    {
        ParkingLotDetails ParkingCarInLot(ParkingLotDetails details);

        // Parking Lot Status (Full Or Not)
        object ParkingLotStatus();


    }
}