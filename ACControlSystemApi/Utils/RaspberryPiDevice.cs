using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACControlSystemApi.Utils
{
    //todo: write converter from different pin codes
    public class RaspberryPiDevice
    {
        public RaspberryPiDevice(string name, Dictionary<int, int> validGpioPins)
        {

        }

        public string Name { get; }
        private Dictionary<int,int> ValidGpioPins { get; }
    }
}
