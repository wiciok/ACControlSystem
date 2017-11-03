using System;
using ACCSApi.Model.Interfaces;

namespace ACCSApi.Model.Dto
{
    public class UserPublic : IUserPublic
    {
        public string EmailAddress { get; set; }
        public DateTime RegistrationTimestamp { get; set; }
    }
}
