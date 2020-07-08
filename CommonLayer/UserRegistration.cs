using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer
{
    //Model class for user Authorization detais
    public class UserRegistration
    {
        //First name
        [Required]
        [RegularExpression(@"^[A-Z][a-zA-Z]*$", ErrorMessage = "Enter Valid First Name")]
        public string FirstName { get; set; }

        //last name
        [Required]
        [RegularExpression(@"^[A-Z][a-zA-Z]*$", ErrorMessage = "Enter Valid Last Name")]
        public string LastName { get; set; }

        //Email Id
        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress]
        [StringLength(50)]
        public string Email { get; set; }

        //User type
        [Required(ErrorMessage = "UserType Is Required")]
        [MaxLength(50)]
        public string UserType { get; set; }

        //Password
        [Required(ErrorMessage = "Password Is Required")]
        [StringLength(50, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
