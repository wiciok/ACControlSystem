using System;
using ACCSApi.Model.Interfaces;

namespace ACCSApi.Model.Transferable
{
    public class UserPublic : IUserPublic
    {
        public string EmailAddress { get; set; }
        public DateTime RegistrationTimestamp { get; set; }
    }
}
