using ACCSApi.Model.Interfaces;

namespace ACCSApi.Repositories.Interfaces
{
    public interface IACDeviceRepository
    {
        IACDevice CurrentACDevice { get; set; }
    }
}
