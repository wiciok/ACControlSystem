using System;
using ACCSApi.Model.Dto;
using ACCSApi.Model.Interfaces;

namespace ACCSApi.Model
{
    public class User : IUser
    {
        public int Id { get; set; }
        public string EmailAddress { get; set; } //also login
        public string PasswordHash { get; set; }
        public DateTime RegistrationTimestamp { get; set; }

        public IUserPublic PublicData => new UserPublic
        {
            EmailAddress = EmailAddress,
            RegistrationTimestamp = RegistrationTimestamp
        };
    }
}

