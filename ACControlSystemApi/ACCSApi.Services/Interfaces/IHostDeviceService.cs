using System;
using System.Collections.Generic;
using System.Text;
using ACCSApi.Model.Interfaces;

namespace ACCSApi.Services.Interfaces
{
    public interface IHostDeviceService
    {
        int AddDevice(IRaspberryPiDevice device);
        IRaspberryPiDevice GetDevice(int id);
        IRaspberryPiDevice GetCurrentDevice();
        IEnumerable<IRaspberryPiDevice> GetAllDevices();
        void DeleteDevice(int id);
        IRaspberryPiDevice UpdateDevice(IRaspberryPiDevice device);
        IRaspberryPiDevice SetCurrentDevice(int id);
    }
}
