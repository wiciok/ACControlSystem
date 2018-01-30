using System;
using System.Collections.Generic;
using System.Linq;
using ACCSApi.Model;
using ACCSApi.Model.Dto;
using ACCSApi.Model.Interfaces;
using ACCSApi.Repositories.Interfaces;
using ACCSApi.Services.Interfaces;
using ACCSApi.Services.Models.Exceptions;

namespace ACCSApi.Services.Domain
{
    public class ACDeviceService : IACDeviceService
    {
        private readonly IACDeviceRepository _acDeviceRepository;
        private IACDevice _currentDevice;

        public ACDeviceService(IACDeviceRepository acDeviceRepository)
        {
            _acDeviceRepository = acDeviceRepository;
        }

        public int AddDevice(AcDeviceDto deviceDto)
        {
            var device = new ACDevice()
            {
                Id = deviceDto.Id,
                Brand = deviceDto.Brand,
                Model = deviceDto.Model
            };

            return AddDevice(device);
        }

        private int AddDevice(IACDevice device)
        {
            if (device == null)
                throw new ArgumentNullException(nameof(device));

            var currentDevices = _acDeviceRepository.Find(x => x.Id.Equals(device.Id) || x.Model.Equals(device.Model) && x.Brand.Equals(device.Brand));
            if (currentDevices.Any())
                throw new ItemAlreadyExistsException();
            return _acDeviceRepository.Add(device);
        }

        private IACDevice GetDevice(int id)
        {
            var device = _acDeviceRepository.Find(x => x.Id.Equals(id)).SingleOrDefault();
            if (device == null)
                throw new ItemNotFoundException($"AcDevice with id {id} not found!");
            return device;
        }

        public AcDeviceDto GetDeviceDto(int id)
        {
            return new AcDeviceDto(GetDevice(id));
        }

        private IEnumerable<IACDevice> GetAllDevices()
        {
            var devices = _acDeviceRepository.GetAll();
            if (devices == null)
                throw new ItemNotFoundException();

            return devices;
        }

        public IEnumerable<AcDeviceDto> GetAllDevicesDtos()
        {
            return GetAllDevices().Select(x => new AcDeviceDto(x));
        }

        public void DeleteDevice(int id)
        {
            var device = _acDeviceRepository.Find(x => x.Id.Equals(id)).SingleOrDefault();
            if (device == null)
                throw new ItemNotFoundException($"AcDevice with id {id} not found - cannot delete!");
            _acDeviceRepository.Delete(device);
        }

        public AcDeviceDto UpdateDevice(AcDeviceDto deviceDto)
        {
            var dev = _acDeviceRepository.Find(x => x.Id.Equals(deviceDto.Id)).SingleOrDefault();
            if (dev == null)
                throw new ItemNotFoundException($"AcDevice with id {deviceDto.Id} not found!");

            dev.Brand = deviceDto.Brand;
            dev.Model = deviceDto.Model;

            _acDeviceRepository.Update(dev);

            return deviceDto;
        }

        public IACDevice GetCurrentDevice()
        {
            if (_currentDevice != null)
                return _currentDevice;

            _currentDevice = _acDeviceRepository.CurrentDevice;

            if (_currentDevice == null)
            {
                return _currentDevice;
            }

            _currentDevice.OnChanged += _currentDevice_OnChanged;
            return _currentDevice;
        }

        public AcDeviceDto GetCurrentDeviceDto()
        {
            var currentDevice = GetCurrentDevice();
            return currentDevice != null ? new AcDeviceDto(currentDevice) : null;
        }

        public AcDeviceDto SetCurrentDevice(int id)
        {
            var newDevice = _acDeviceRepository.Find(x => x.Id.Equals(id)).SingleOrDefault();
            _acDeviceRepository.CurrentDevice = newDevice ?? throw new ItemNotFoundException($"AcDevice with id {id} not found!");
            if (_currentDevice != null)
                _currentDevice.OnChanged -= _currentDevice_OnChanged;
            _currentDevice = newDevice;
            return new AcDeviceDto(_currentDevice);
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

