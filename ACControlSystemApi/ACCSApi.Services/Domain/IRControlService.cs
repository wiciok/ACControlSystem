using System;
using ACCSApi.Model.Interfaces;
using ACCSApi.Model.Transferable;
using ACCSApi.Repositories.Interfaces;
using ACCSApi.Services.Interfaces;
using ACCSApi.Services.Models.Exceptions;
using IRSlingerCsharp;

namespace ACCSApi.Services.Domain
{
    internal class IRControlService : IIRControlService
    {
        private IIRSlingerCsharp _irService;
        private IRaspberryPiDeviceRepository _hardwareDevicesRepo;
        private IACDeviceRepository _acDevicesRepo;

        private IRaspberryPiDevice _hostDevice;
        private IACDevice _ACDevice;

        public ICode TurnOnCode { get; set; }
        public ICode TurnOffCode { get; set; }


        public IRControlService(IIRSlingerCsharp irService, IRaspberryPiDeviceRepository hardwareDevicesRepo, IACDeviceRepository acDeviceRepo)
        {
            _irService = irService;
            _hardwareDevicesRepo = hardwareDevicesRepo;
            _acDevicesRepo = acDeviceRepo;

            _ACDevice = acDeviceRepo.CurrentACDevice;
            _hostDevice = _hardwareDevicesRepo.CurrentDevice;
        }


        public void SendMessage(ICode code)
        {
            switch (code)
            {
                case NecCode nc:
                    _irService.SendNecMsg
                    (_hostDevice.BroadcomOutPin,
                        _ACDevice.ModulationFrequencyInHz,
                        _ACDevice.DutyCycle,
                        nc.LeadingPulseDuration,
                        nc.LeadingGapDuration,
                        nc.OnePulseDuration,
                        nc.ZeroPulseDuration,
                        nc.OneGapDuration,
                        nc.ZeroGapDuration,
                        nc.SendTrailingPulse,
                        nc.Code);
                    break;
                case RawCode rc:
                    _irService.SendRawMsg
                    (_hostDevice.BroadcomOutPin,
                        _ACDevice.ModulationFrequencyInHz,
                        _ACDevice.DutyCycle,
                        rc.Code);
                    break;
                default:
                    throw new ArgumentException();
            }
        }


        public void SendDefaultTurnOffMessage()
        {
            if (TurnOffCode == null)
                throw new ItemNotFoundException("Default Turn Off Code not specified!");
            SendMessage(TurnOffCode);
        }

        public void SendDefaultTurnOnMessage()
        {
            if (TurnOnCode == null)
                throw new ItemNotFoundException("Default Turn On Code not specified!");
            SendMessage(TurnOnCode);
        }
    }
}
