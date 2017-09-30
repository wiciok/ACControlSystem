using System;

namespace ACControlSystemApi.Model.Interfaces
{
    public interface IUserPublic
    {
        string EmailAddress { get; set; }
        DateTime RegistrationTimestamp { get; set; }
    }
}
