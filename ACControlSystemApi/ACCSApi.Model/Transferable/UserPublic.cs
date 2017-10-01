using ACControlSystemApi.Model.Interfaces;
using System;

namespace ACControlSystemApi.Model
{
    public class UserPublic : IUserPublic
    {
        public string EmailAddress { get; set; }
        public DateTime RegistrationTimestamp { get; set; }
    }
}
