using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACControlSystemApi.Services.Interfaces
{
    public interface IPasswordHashingService
    {
        string CreateHash(string password);
    }
}
