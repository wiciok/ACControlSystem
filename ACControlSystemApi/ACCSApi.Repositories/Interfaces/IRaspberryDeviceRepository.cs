using ACCSApi.Model.Interfaces;
using ACCSApi.Repositories.Generic;

namespace ACCSApi.Repositories.Interfaces
{
    public interface IRaspberryPiDeviceRepository : IRepository<IRaspberryPiDevice>
    {
        IRaspberryPiDevice CurrentDevice { get; set; }
    }
}
