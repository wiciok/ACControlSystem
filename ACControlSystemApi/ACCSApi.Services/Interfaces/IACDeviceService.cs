using System.Collections.Generic;
using ACCSApi.Model.Interfaces;

namespace ACCSApi.Services.Interfaces
{
    public interface IACDeviceService
    {
        int AddDevice(IACDevice device);
        IACDevice GetDevice(int id);
        IACDevice GetCurrentDevice();
        IEnumerable<IACDevice> GetAllDevices();
        void DeleteDevice(int id);
        IACDevice UpdateDevice(IACDevice device);
        IACDevice SetCurrentDevice(int id);
    }
}
