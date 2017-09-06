using ACControlSystemApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACControlSystemApi.Services.Interfaces
{
    public interface IACStateControlService
    {
        void SetCurrentState(ACState newState);
        ACState GetCurrentState();
    }
}
