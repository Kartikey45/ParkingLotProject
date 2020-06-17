using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer
{
    public class RegistrationResponse
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string UserType { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
