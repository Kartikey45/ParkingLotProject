using CommonLayer.ParkingModel;
using CommonLayer.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IVehicalParkingDetailsRL
    {
        //Car park
        ParkingLotDetails ParkingCarInLot(ParkingInformation Details);

        // Parking Lot Status 
        object ParkingLotStatus();

        // Delete Car parking Details
        object DeleteCarParkingDetails(int ParkingID);

        // Car UnPark 
        object CarUnPark(int ID);

        // Get All Parking Cars Details
        List<ParkingLotDetails> GetAllParkingCarsDetails();


        // Get All UnPark Car Details
        object GetAllUnParkedCarDetail();

        // Get Car Details By Car Number
        List<ParkingLotDetails> GetCarDetailsByVehicleNumber(string VehicleNumber);

        // Get Car Details By Parking Slot
        List<ParkingLotDetails> GetCarDetailsByParkingSlot(string Slot);

        // Get Car Details By Car Brand
        List<ParkingLotDetails> GetCarDetailsByVehicleBrand(string brand);

        // Get All Car Details Of Handicap
        List<ParkingLotDetails> GetAllCarDetailsOfHandicap();

        //Get all car details by color
        List<ParkingLotDetails> GetAllCarDetailsByColor(string VehicalColor);
    }
}