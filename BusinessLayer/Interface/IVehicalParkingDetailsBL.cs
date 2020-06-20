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
    }
}
