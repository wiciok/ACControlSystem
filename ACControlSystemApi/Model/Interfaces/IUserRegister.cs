using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACControlSystemApi.Model.Interfaces
{
    public interface IUserRegister
    {
        AuthPackage AuthenticationData { get; set; }
    }
}
