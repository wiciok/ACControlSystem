using System;

namespace ACCSApi.Model.Interfaces
{
    public interface IUserPublic
    {
        int Id { get; set; }
        string EmailAddress { get; set; }
        DateTime RegistrationTimestamp { get; set; }
    }
}
