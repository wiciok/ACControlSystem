using ACControlSystemApi.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACControlSystemApi.Services
{
    public class RaspberryDevicesService: IRaspberryDevicesService
    {
        public RaspberryDevicesService()
        {
            RaspberryVersionsList = new List<RaspberryPiDevice>();
            Initialize();
        }

        private void Initialize()
        {
            RaspberryVersionsList.Add(new RaspberryPiDevice("Model 3b", ));
        }

        public List<RaspberryPiDevice> RaspberryVersionsList { get; }


    }

    public interface IRaspberryDevicesService
    {
    }
}
