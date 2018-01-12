using System;
using ACCSApi.Model.Dto;
using ACCSApi.Model.Interfaces;
using Newtonsoft.Json;

namespace ACCSApi.Model
{
    public class User : IUser
    {
        public int Id { get; set; }
        public string EmailAddress { get; set; } //also login
        public string PasswordHash { get; set; }
        public DateTime RegistrationTimestamp { get; set; }

        [JsonIgnore]
        public IUserPublic PublicData => new UserPublic
        {
            Id = Id,
            EmailAddress = EmailAddress,
            RegistrationTimestamp = RegistrationTimestamp
        };
    }
}

