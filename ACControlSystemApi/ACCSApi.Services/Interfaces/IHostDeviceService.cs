using System.Collections.Generic;
using ACCSApi.Model.Interfaces;

namespace ACCSApi.Services.Interfaces
{
    public interface IHostDeviceService
    {
        IRaspberryPiDevice GetCurrentDevice();
    }
}
