using System;
using ACControlSystemApi.Model.Interfaces;

namespace ACControlSystemApi.Model
{
    public class Admin : IUser
    {
        public string EmailAddress { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string PasswordHash { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime RegistrationTimestamp { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IUserPublic PublicData => throw new NotImplementedException();

        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
