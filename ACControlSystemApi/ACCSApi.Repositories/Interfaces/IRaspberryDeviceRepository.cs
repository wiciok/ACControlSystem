using ACControlSystemApi.Repositories.Generic;
using ACCSApi.Model.Interfaces;

namespace ACCSApi.Repositories.Interfaces
{
    public interface IRaspberryPiDeviceRepository : IRepository<IRaspberryPiDevice>
    {
        IRaspberryPiDevice CurrentDevice { get; set; }
    }
}
