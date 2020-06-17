using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer
{
    //class for login
    public class UserLogin
    {
        //User Type
        [Required(ErrorMessage = "UserType Is Required")]
        [MaxLength(50)]
        public string UserTypes { get; set; }

        //Email Id
        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress]
        [StringLength(50)]
        public string Email { get; set; }

        //Password
        [Required(ErrorMessage = "Password Is Required")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 6)]
        public string Password { get; set; }
    }
}
