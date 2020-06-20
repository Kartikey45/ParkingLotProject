using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonLayer.ParkingModel
{
    public class VehicalUnpark
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VehicleUnParkID { get; set; }

        [ForeignKey("ParkingLotDetails")]
        public int ParkingID { get; set; }

        public string Status { get; set; }

        public System.DateTime UnParkDate { get; set; }

        public double TotalTime { get; set; }

        public double TotalAmount { get; set; }
    }
}
