using ACControlSystemApi.Model;
using ACControlSystemApi.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACControlSystemApi.Services.Interfaces
{
    public interface IHostDeviceControlService
    {
        void SendMessage(ICode code);
        void SendMessageById(int id);
        void SendTurnOffMessage();
        void SendTurnOnMessage();
    }
}
