using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACControlSystemApi.Model.Interfaces
{
    public interface IUserPublic
    {
        string EmailAddress { get; set; }
        DateTime RegistrationTimestamp { get; set; }
    }
}
