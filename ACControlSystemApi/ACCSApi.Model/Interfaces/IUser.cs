using ACCSApi.Model.Interfaces;
using System;

namespace ACControlSystemApi.Model.Interfaces
{
    public interface IUser: IACCSSerializable
    {
        string EmailAddress { get; set; }
        string PasswordHash { get; set; }
        DateTime RegistrationTimestamp { get; set; }

        IUserPublic PublicData { get; }
    }
}