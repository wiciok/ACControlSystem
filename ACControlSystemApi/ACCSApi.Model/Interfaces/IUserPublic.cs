using System;

namespace ACCSApi.Model.Interfaces
{
    public interface IUserPublic
    {
        string EmailAddress { get; set; }
        DateTime RegistrationTimestamp { get; set; }
    }
}
