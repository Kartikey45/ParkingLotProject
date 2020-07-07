using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.ParkingModel
{
    public class ShowUnparked
    {
        public int ParkingID { get; set; }

        
        public string VehicleOwnerName { get; set; }

        
        public string VehicleNumber { get; set; }

       
        public string VehicalBrand { get; set; }

        
        public string VehicalColor { get; set; }

       
        public string DriverName { get; set; }

        
        public string ParkingSlot { get; set; }

        public string ParkingUserCategory { get; set; }


        public System.DateTime ParkingDate { get; set; }

        public System.DateTime UnParkDate { get; set; }

        public double TotalTime { get; set; }

        public double TotalAmount { get; set; }
    }
}
