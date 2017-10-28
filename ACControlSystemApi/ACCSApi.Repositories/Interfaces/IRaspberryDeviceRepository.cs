using ACCSApi.Model.Interfaces;
using ACCSApi.Model.Transferable;
using ACCSApi.Repositories.Generic;

namespace ACCSApi.Repositories.Interfaces
{
    public interface IRaspberryPiDeviceRepository : IRepository<IRaspberryPiDevice>
    {
        IRaspberryPiDevice CurrentDevice { get; set; }
    }
}
