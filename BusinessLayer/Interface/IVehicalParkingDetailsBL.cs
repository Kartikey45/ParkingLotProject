using CommonLayer.ParkingModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IVehicalParkingDetailsBL
    {
        ParkingLotDetails ParkingCarInLot(ParkingLotDetails details);

        // Parking Lot Status (Full OR Not)
        object ParkingLotStatus();

        // Delete Car Details
        object DeleteCarParkingDetails(int ParkingID);

        // Method to delete Unpark car details
        object DeleteUnparkCarDetails(int UnparkVehicalID);

        // Car UnPark
        object CarUnPark(VehicalUnpark details);

        // Get All Parking Details
        List<ParkingLotDetails> GetAllParkingCarsDetails();
    }
}
