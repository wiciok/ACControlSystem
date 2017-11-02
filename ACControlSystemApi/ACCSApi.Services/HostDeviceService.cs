using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ACCSApi.Model.Interfaces;
using ACCSApi.Repositories.Interfaces;
using ACCSApi.Services.Interfaces;
using ACCSApi.Services.Models.Exceptions;

namespace ACCSApi.Services
{
    public class HostDeviceService: IHostDeviceService
    {
        private readonly IRaspberryPiDeviceRepository _raspberryPiDeviceRepository;

        public HostDeviceService(IRaspberryPiDeviceRepository raspberryPiDeviceRepository)
        {
            _raspberryPiDeviceRepository = raspberryPiDeviceRepository;
        }

        public int AddDevice(IRaspberryPiDevice device)
        {
            if (device == null)
                throw new ArgumentNullException(nameof(device));

            var currentDevices =_raspberryPiDeviceRepository.Find(x => x.Id.Equals(device.Id) || x.Name.Equals(device.Name));
            if(currentDevices.Any())
                throw new ItemAlreadyExistsException();
            return _raspberryPiDeviceRepository.Add(device);
        }

        public IRaspberryPiDevice GetDevice(int id)
        {
            var device = _raspberryPiDeviceRepository.Find(x => x.Id.Equals(id)).SingleOrDefault();
            if(device==null)
                throw new ItemNotFoundException($"RaspberryPiDevice with id {id} not found!");
            return device;
        }

        public IRaspberryPiDevice GetCurrentDevice()
        {
            var currentDevice = _raspberryPiDeviceRepository.CurrentDevice;
            if(currentDevice==null)
                throw new ItemNotFoundException("Current device not set!");
            return currentDevice;
        }

        public IEnumerable<IRaspberryPiDevice> GetAllDevices()
        {
            var devices = _raspberryPiDeviceRepository.GetAll();
            if(devices==null)
                throw new ItemNotFoundException();

            return devices;
        }

        public void DeleteDevice(int id)
        {
            var device = _raspberryPiDeviceRepository.Find(x => x.Id.Equals(id)).SingleOrDefault();
            if(device==null)
                throw new ItemNotFoundException($"RasbperryPiDevice with id {id} not found - cannot delete!");
            _raspberryPiDeviceRepository.Delete(device);
        }

        public IRaspberryPiDevice UpdateDevice(IRaspberryPiDevice device)
        {
            var dev = _raspberryPiDeviceRepository.Find(x => x.Id.Equals(device.Id)).SingleOrDefault();
            if (dev == null)
                throw new ItemNotFoundException($"RaspberryPiDevice with id {device.Id} not found!");

            _raspberryPiDeviceRepository.Update(device);

            return device;
        }

        public IRaspberryPiDevice SetCurrentDevice(int id)
        {
            var device = _raspberryPiDeviceRepository.Find(x => x.Id.Equals(id)).SingleOrDefault();
            if (device == null)
                throw new ItemNotFoundException($"RaspberryPiDevice with id {id} not found!");

            _raspberryPiDeviceRepository.CurrentDevice = device;
            return device;
        }
    }
}
