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
        private readonly IIRSlingerCsharp _irService;
        private readonly IRaspberryPiDeviceRepository _hardwareDevicesRepo;
        private readonly IACDeviceRepository _acDevicesRepo;

        private readonly IRaspberryPiDevice _hostDevice;
        private readonly IACDevice _ACDevice;

        public ICode TurnOnCode { get; set; }
        public ICode TurnOffCode { get; set; }


        public IRControlService(IIRSlingerCsharp irService, IRaspberryPiDeviceRepository hardwareDevicesRepo, IACDeviceRepository acDeviceRepo)
        {
            _irService = irService;
            _hardwareDevicesRepo = hardwareDevicesRepo;
            _acDevicesRepo = acDeviceRepo;

            _ACDevice = _acDevicesRepo.CurrentACDevice;
            _hostDevice = _hardwareDevicesRepo.CurrentDevice;
        }


        public void SendMessage(ICode code)
        {
            switch (code)
            {
                case NecCode nc:
                    if (_ACDevice.NecCodeSettingsSaved)
                    {
                        _irService.SendNecMsg
                        (_hostDevice.BroadcomOutPin,
                            _ACDevice.ModulationFrequencyInHz,
                            _ACDevice.DutyCycle,
                            _ACDevice.NecCodeSettings.LeadingPulseDuration,
                            _ACDevice.NecCodeSettings.LeadingGapDuration,
                            _ACDevice.NecCodeSettings.OnePulseDuration,
                            _ACDevice.NecCodeSettings.ZeroPulseDuration,
                            _ACDevice.NecCodeSettings.OneGapDuration,
                            _ACDevice.NecCodeSettings.ZeroGapDuration,
                            _ACDevice.NecCodeSettings.SendTrailingPulse,
                            nc.Code);
                    }
                    else
                    {
                        throw new InvalidOperationException("ACDevice does not contain Nec code details");
                    }
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
