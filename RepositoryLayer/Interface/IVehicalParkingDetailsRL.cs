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

        // Get All Parking Cars Details
        List<ParkingLotDetails> GetAllParkingCarsDetails();

        // Method to delete Unpark car details
        object DeleteUnparkHistory(int UnparkID);

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

        //Get all car details by color
        object GetAllCarDetailsByColor(string VehicalColor);
    }
}