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
        object DeleteUnparkHistory(int UnparkVehicalID);

        // Car UnPark
        object CarUnPark(VehicalUnpark details);

        // Get All Parking Details
        List<ParkingLotDetails> GetAllParkingCarsDetails();

        // Get All UnPark Car Details
        object GetAllUnParkedCarDetail();

        // Get Car Details By Car Number
        object GetCarDetailsByVehicleNumber(string VehicleNumber);

        // Get Car Details By Parking Slot
        object GetCarDetailsByParkingSlot(string Slot);

        // Get Car Details By Car Brand
        object GetCarDetailsByVehicleBrand(string brand);

        // Get All Car Details Of Handicap
        object GetAllCarDetailsOfHandicap();
    }
}
