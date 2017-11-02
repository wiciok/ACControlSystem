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
        private readonly ICodeRecordingService _codeRecordingService;

        public ACDeviceService(IACDeviceRepository acDeviceRepository, ICodeRecordingService codeRecordingService)
        {
            _acDeviceRepository = acDeviceRepository;
            _codeRecordingService = codeRecordingService;
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

        public IACDevice GetCurrentDevice()
        {
            var currentDevice = _acDeviceRepository.CurrentDevice;
            if (currentDevice == null)
                throw new ItemNotFoundException("Current device not set!");
            return currentDevice;
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

        public IACDevice SetCurrentDevice(int id)
        {
            var device = _acDeviceRepository.Find(x => x.Id.Equals(id)).SingleOrDefault();
            _acDeviceRepository.CurrentDevice = device ?? throw new ItemNotFoundException($"AcDevice with id {id} not found!");
            return device;
        }
    }
}

