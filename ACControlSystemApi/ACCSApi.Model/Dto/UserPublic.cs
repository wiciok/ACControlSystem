using System;
using ACCSApi.Model.Interfaces;

namespace ACCSApi.Model.Dto
{
    public class UserPublic : IUserPublic
    {
        public int Id { get; set; }
        public string EmailAddress { get; set; }
        public DateTime RegistrationTimestamp { get; set; }
    }
}
