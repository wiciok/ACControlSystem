using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACControlSystemApi.Repositories
{
    public interface IRaspberryPiDeviceRepository
    {
        RaspberryPiDevice GetCurrentDevice();
        void SetCurrentDevice(RaspberryPiDevice device);
    }

    public class RaspberryPiDeviceRepository : IRaspberryPiDeviceRepository
    {
        public RaspberryPiDevice GetCurrentDevice()
        {
            throw new NotImplementedException();
        }

        public void SetCurrentDevice(RaspberryPiDevice device)
        {
            throw new NotImplementedException();
        }
    }
}
