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

        public int AddDevice(IRaspberryPiDevice device)
        {
            if (device == null)
                throw new ArgumentNullException(nameof(device));

            var currentDevices = _raspberryPiDeviceRepository.Find(x => x.Id.Equals(device.Id) || x.Name.Equals(device.Name));
            if (currentDevices.Any())
                throw new ItemAlreadyExistsException();
            return _raspberryPiDeviceRepository.Add(device);
        }

        public IRaspberryPiDevice GetDevice(int id)
        {
            var device = _raspberryPiDeviceRepository.Find(x => x.Id.Equals(id)).SingleOrDefault();
            if (device == null)
                throw new ItemNotFoundException($"RaspberryPiDevice with id {id} not found!");
            return device;
        }

        public IEnumerable<IRaspberryPiDevice> GetAllDevices()
        {
            var devices = _raspberryPiDeviceRepository.GetAll();
            if (devices == null)
                throw new ItemNotFoundException();

            return devices;
        }

        public void DeleteDevice(int id)
        {
            var device = _raspberryPiDeviceRepository.Find(x => x.Id.Equals(id)).SingleOrDefault();
            if (device == null)
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

        public IRaspberryPiDevice GetCurrentDevice()
        {
            if (_currentDevice != null)
                return _currentDevice;

            /*if (_currentDevice == null)
            {
                //throw new ItemNotFoundException("Current device not set!");
                return _currentDevice;
            }  */             
            _currentDevice = _raspberryPiDeviceRepository.CurrentDevice;
            _currentDevice.OnChanged += _currentDevice_OnChanged;

            return _currentDevice;
        }

        public IRaspberryPiDevice SetCurrentDevice(int id)
        {
            var newDevice = _raspberryPiDeviceRepository.Find(x => x.Id.Equals(id)).SingleOrDefault();
            _raspberryPiDeviceRepository.CurrentDevice = newDevice ?? throw new ItemNotFoundException($"RaspberryPiDevice with id {id} not found!");

            if (_currentDevice != null)
                _currentDevice.OnChanged -= _currentDevice_OnChanged;
            _currentDevice = newDevice;
            return _currentDevice;
        }

        private void _currentDevice_OnChanged()
        {
            SaveCurrentDeviceChanges();
        }

        private void SaveCurrentDeviceChanges()
        {
            _raspberryPiDeviceRepository.Update(_currentDevice);
        }
    }
}
