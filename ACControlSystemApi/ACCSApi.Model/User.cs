using System;
using ACCSApi.Model.Interfaces;
using ACCSApi.Model.Transferable;

namespace ACCSApi.Model
{
    public class User : IUser
    {
        public int Id { get; set; }
        public string EmailAddress { get; set; } //also login
        public string PasswordHash { get; set; }
        public DateTime RegistrationTimestamp { get; set; }

        public IUserPublic PublicData
        {
            get => new UserPublic()
            {
                EmailAddress = EmailAddress,
                RegistrationTimestamp = RegistrationTimestamp
            };
        }
    }
}

