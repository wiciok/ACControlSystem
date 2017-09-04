using ACControlSystemApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACControlSystemApi.Repositories
{
    public interface IACDeviceRepository
    {
        ACDevice CurrentACDevice { get; set; }
    }

    public class ACDeviceRepository : IACDeviceRepository
    {
        public ACDevice CurrentACDevice
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
    }
}
