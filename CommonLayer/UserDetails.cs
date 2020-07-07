using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonLayer
{
    //class of user details
    public class UserDetails
    {
        //User Id
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        //First namee
        [Required]
        [RegularExpression(@"^[A-Z][a-zA-Z]*$", ErrorMessage = "Enter Valid First Name")]
        public string FirstName { get; set; }

        //last name
        [Required]
        [RegularExpression(@"^[A-Z][a-zA-Z]*$" , ErrorMessage = "Enter Valid Last Name")]
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

        //create date and time
        public DateTime CreateDate { get; set; } = DateTime.Now;

        //modify date and time
        public DateTime ModifiedDate { get; set; }
    }
}
