using System;

namespace ACControlSystemApi.Model.Interfaces
{
    public interface IUser: IACControlSystemSerializableClass
    {
        string EmailAddress { get; set; }
        string PasswordHash { get; set; }
        DateTime RegistrationTimestamp { get; set; }
    }
}