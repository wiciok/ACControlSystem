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
        public DateTime RegistrationTimestamp { get; }

        public User()
        {
            RegistrationTimestamp = DateTime.Now;
        }

        [JsonIgnore]
        public IUserPublic PublicData => new UserPublic
        {
            Id = Id,
            EmailAddress = EmailAddress,
            RegistrationTimestamp = RegistrationTimestamp
        };

        public override int GetHashCode()
        {
            return RegistrationTimestamp.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is IUser otherObj))
                return false;
            return otherObj.Id.Equals(Id) && otherObj.PasswordHash.Equals(PasswordHash) &&
                   otherObj.EmailAddress.Equals(EmailAddress) &&
                   otherObj.RegistrationTimestamp.Equals(RegistrationTimestamp);
        }
    }
}

