using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonLayer.ParkingModel
{
    public class ParkingLotDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ParkingID { get; set; }

        [RegularExpression(@"^[A-Z][a-zA-Z]*$", ErrorMessage = "First Letter Must Be Capital ")]
        [Required(ErrorMessage = "Wrong Field Name Please Write Vehicle_Owner_Name")]
        public string VehicleOwnerName { get; set; }

        [RegularExpression(@"^(([A-Za-z]){2}(|-)(?:[0-9]){1,2}(|-)(?:[A-Za-z]){2}(|-)([0-9]){1,4})|(([A-Za-z]){2,3}(|-)([0-9]){1,4})$", ErrorMessage = "Please Enter In This Way MH-12-AA-1235 ")]
        [Required(ErrorMessage = "Wrong Field Name Please Write Vehicles_Number")]
        public string VehicleNumber { get; set; }

        [Required(ErrorMessage = "Wrong Field Name Please Write Brand")]
        [RegularExpression(@"^[A-Z][a-z]*$", ErrorMessage = "First Letter Must Be Capital ")]
        public string VehicalBrand { get; set; }

        [Required(ErrorMessage = "Wrong Field Name Please Write Color")]
        [RegularExpression(@"^[A-Z][a-z]*$", ErrorMessage = "First Letter Must Be Capital ")]
        public string VehicalColor { get; set; }

        [RegularExpression(@"^[A-Z][a-zA-Z]*$", ErrorMessage = "First Letter Must Be Capital ")]
        [Required(ErrorMessage = "Wrong Field Name Please Write Driver_Name")]
        public string DriverName { get; set; }

        //[Required(ErrorMessage = "Wrong Field Name Please Write Parking_Slot")]
        [RegularExpression(@"^([A-Z]){1,2}$", ErrorMessage = "Parking Slot A To Z")]
        public string ParkingSlot { get; set; }

        public string ParkingUserCategory { get; set; }

        public string Status { get; set; }

        public System.DateTime ParkingDate { get; set; }
    }
}
