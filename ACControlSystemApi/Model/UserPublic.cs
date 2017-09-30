using ACControlSystemApi.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACControlSystemApi.Model
{
    public class UserPublic : IUserPublic
    {
        public string EmailAddress { get; set; }
        public DateTime RegistrationTimestamp { get; set; }
    }
}
