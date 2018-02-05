using System;
using System.Collections.Generic;
using System.Linq;
using ACCSApi.Model.Interfaces;
using ACCSApi.Repositories.Interfaces;
using ACCSApi.Services.Interfaces;
using ACCSApi.Services.Models.Exceptions;

namespace ACCSApi.Services.Domain
{
    public class HostDeviceService : IHostDeviceService
    {
        private readonly IRaspberryPiDeviceRepository _raspberryPiDeviceRepository;
        private IRaspberryPiDevice _currentDevice;


        public HostDeviceService(IRaspberryPiDeviceRepository raspberryPiDeviceRepository)
        {
            _raspberryPiDeviceRepository = raspberryPiDeviceRepository;
        }

        public IRaspberryPiDevice GetCurrentDevice()
        {
            if (_currentDevice != null)
                return _currentDevice;
            _currentDevice = _raspberryPiDeviceRepository.CurrentDevice;

            if (_currentDevice == null)
               throw new ItemNotFoundException("Current host device not set!");
            

            return _currentDevice;
        }
    }
}
