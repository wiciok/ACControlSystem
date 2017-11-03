using System;
using System.Collections.Generic;
using System.Linq;
using ACCSApi.Model.Interfaces;
using ACCSApi.Repositories.Interfaces;
using ACCSApi.Services.Interfaces;
using ACCSApi.Services.Models.Exceptions;

namespace ACCSApi.Services.Domain
{
    public class ACDeviceService: IACDeviceService
    {
        private readonly IACDeviceRepository _acDeviceRepository;
        private IACDevice _currentDevice;

        public ACDeviceService(IACDeviceRepository acDeviceRepository)
        {
            _acDeviceRepository = acDeviceRepository;
        }

        public int AddDevice(IACDevice device)
        {
            if (device == null)
                throw new ArgumentNullException(nameof(device));

            var currentDevices = _acDeviceRepository.Find(x => x.Id.Equals(device.Id) || x.Model.Equals(device.Model) && x.Brand.Equals(device.Brand));
            if (currentDevices.Any())
                throw new ItemAlreadyExistsException();
            return _acDeviceRepository.Add(device);
        }

        public IACDevice GetDevice(int id)
        {
            var device = _acDeviceRepository.Find(x => x.Id.Equals(id)).SingleOrDefault();
            if (device == null)
                throw new ItemNotFoundException($"AcDevice with id {id} not found!");
            return device;
        }

        public IEnumerable<IACDevice> GetAllDevices()
        {
            var devices = _acDeviceRepository.GetAll();
            if (devices == null)
                throw new ItemNotFoundException();

            return devices;
        }

        public void DeleteDevice(int id)
        {
            var device = _acDeviceRepository.Find(x => x.Id.Equals(id)).SingleOrDefault();
            if (device == null)
                throw new ItemNotFoundException($"AcDevice with id {id} not found - cannot delete!");
            _acDeviceRepository.Delete(device);
        }

        public IACDevice UpdateDevice(IACDevice device)
        {
            var dev = _acDeviceRepository.Find(x => x.Id.Equals(device.Id)).SingleOrDefault();
            if (dev == null)
                throw new ItemNotFoundException($"RaspberryPiDevice with id {device.Id} not found!");

            _acDeviceRepository.Update(device);

            return device;
        }

        public IACDevice GetCurrentDevice()
        {
            if (_currentDevice == null)
            {
                _currentDevice = _acDeviceRepository.CurrentDevice;
                _currentDevice.OnChanged += _currentDevice_OnChanged;
            }              

            if (_currentDevice == null)
                throw new ItemNotFoundException("Current device not set!");
            return _currentDevice;
        }

        public IACDevice SetCurrentDevice(int id)
        {
            var newDevice = _acDeviceRepository.Find(x => x.Id.Equals(id)).SingleOrDefault();
            _acDeviceRepository.CurrentDevice = newDevice ?? throw new ItemNotFoundException($"AcDevice with id {id} not found!");
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
            _acDeviceRepository.Update(_currentDevice);
        }
    }
}

