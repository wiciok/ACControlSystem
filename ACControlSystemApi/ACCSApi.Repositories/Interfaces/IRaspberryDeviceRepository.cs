using ACControlSystemApi.Repositories.Generic;

namespace ACCSApi.Repositories.Interfaces
{
    public interface IRaspberryPiDeviceRepository : IRepository<RaspberryPiDevice>
    {
        RaspberryPiDevice CurrentDevice { get; set; }
    }
}
