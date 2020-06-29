using CommonLayer.ParkingModel;
using CommonLayer.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IVehicalParkingDetailsBL
    {
        //Car park
        ParkingLotDetails ParkingCarInLot(ParkingInformation Details);

        // Parking Lot Status (Full OR Not)
        object ParkingLotStatus();

        // Delete Car Details
        object DeleteCarParkingDetails(int ParkingID);

        // Car UnPark
        object CarUnPark(int ParkingID);

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

        //Get Car all car details by color
        object GetAllCarDetailsByColor(string VehicalColor);
    }
}
