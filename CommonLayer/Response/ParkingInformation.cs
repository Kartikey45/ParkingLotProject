using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Response
{
    public class ParkingInformation
    {
        [RegularExpression(@"^[A-Z][a-zA-Z]*$", ErrorMessage = "First Letter Must Be Capital ")]
        [Required(ErrorMessage = "Wrong Field Name Please Write Vehicle_Owner_Name")]
        public string VehicleOwnerName { get; set; }

        //[RegularExpression(@"^(([A-Za-z]){2}(|-)(?:[0-9]){1,2}(|-)(?:[A-Za-z]){2}(|-)([0-9]){1,4})|(([A-Za-z]){2,3}(|-)([0-9]){1,4})$", ErrorMessage = "Please Enter In This Way MH-12-AA-1235 ")]
        [RegularExpression(@"^[a-z||A-Z]{2}[ ]{1}[0-9]{2}[ ]{1}[a-z||A-Z]{1,2}[ ]{1}[0-9]{4}$", ErrorMessage = "Invalid Vehical Number , Please Enter In This Way    MH 12 AA 1235 ")]
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

        public string ParkingUserCategory { get; set; }
    }
}
