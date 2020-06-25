using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer
{
    //Model class for user Authorization detais
    public class UserAuthantication
    {
        //User Id
        [Required(ErrorMessage = "UserId Is Required")]
        public int UserId { get; set; }

        //Email Id
        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress]
        [StringLength(50)]
        public string Email { get; set; }

        //User Type
        [Required(ErrorMessage = "UserRole Is Required")]
        [MaxLength(50)]
        public string UserRole { get; set; }
    }
}
