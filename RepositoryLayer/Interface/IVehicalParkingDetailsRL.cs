using CommonLayer.ParkingModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IVehicalParkingDetailsRL
    {
        //Car park
        ParkingLotDetails ParkingCarInLot(ParkingLotDetails details);

        // Parking Lot Status 
        object ParkingLotStatus();

        // Delete Car parking Details
        object DeleteCarParkingDetails(int ParkingID);

        // Car UnPark 
        object CarUnPark(VehicalUnpark details);
    }
}