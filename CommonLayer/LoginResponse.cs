using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer
{
    public class LoginResponse
    {
        public string Email { get; set; }

        public string UserRole { get; set; }

        public string JwtToken { get; set; }
    }
}
