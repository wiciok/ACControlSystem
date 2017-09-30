using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACControlSystemApi.Model
{
    public class AuthPackage
    {
        public AuthPackage(string email, string pass)
        {
            EmailAddress = email;
            Password = pass;
        }

        public string EmailAddress { get; }
        public string Password { get; }
    }
}
