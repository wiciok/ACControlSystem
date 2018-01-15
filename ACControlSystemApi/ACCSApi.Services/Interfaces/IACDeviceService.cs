using System.Collections.Generic;
using ACCSApi.Model.Dto;
using ACCSApi.Model.Interfaces;

namespace ACCSApi.Services.Interfaces
{
    public interface IACDeviceService
    {
        int AddDevice(AcDeviceDto device);
        AcDeviceDto GetDeviceDto(int id);
        AcDeviceDto GetCurrentDeviceDto();
        IACDevice GetCurrentDevice();
        IEnumerable<AcDeviceDto> GetAllDevicesDtos();
        void DeleteDevice(int id);
        AcDeviceDto UpdateDevice(AcDeviceDto device);
        AcDeviceDto SetCurrentDevice(int id);
    }
}
