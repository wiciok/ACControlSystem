using System;
using ACCSApi.Model;
using ACCSApi.Model.Interfaces;
using ACCSApi.Services.Interfaces;
using ACCSApi.Services.Models.Exceptions;
using IRSlingerCsharp;

namespace ACCSApi.Services.Domain
{
    internal class IRControlService : IIRControlService
    {
        private readonly IIRSlingerCsharp _irService;
        private readonly IRaspberryPiDevice _hostDevice;
        private readonly IACDevice _currentAcDevice;


        public IRControlService(IIRSlingerCsharp irService, IACDeviceService acDeviceService, IHostDeviceService hostDeviceService)
        {
            _irService = irService;

            _currentAcDevice = acDeviceService.GetCurrentDevice();
            _hostDevice = hostDeviceService.GetCurrentDevice();
        }

        public void SendMessage(ICode code)
        {
            if (_currentAcDevice == null)
                throw new CurrentACDeviceNotSetException();

            switch (code)
            {
                case NecCode nc:
                    if (_currentAcDevice.NecCodeSettingsSaved)
                    {
                        _irService.SendNecMsg
                        (_hostDevice.BroadcomOutPin,
                            _currentAcDevice.ModulationFrequencyInHz,
                            _currentAcDevice.DutyCycle,
                            _currentAcDevice.NecCodeSettings.LeadingPulseDuration,
                            _currentAcDevice.NecCodeSettings.LeadingGapDuration,
                            _currentAcDevice.NecCodeSettings.OnePulseDuration,
                            _currentAcDevice.NecCodeSettings.ZeroPulseDuration,
                            _currentAcDevice.NecCodeSettings.OneGapDuration,
                            _currentAcDevice.NecCodeSettings.ZeroGapDuration,
                            _currentAcDevice.NecCodeSettings.SendTrailingPulse,
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
                        _currentAcDevice.ModulationFrequencyInHz,
                        _currentAcDevice.DutyCycle,
                        rc.Code);
                    break;
                default:
                    throw new ArgumentException();
            }
        }
    }
}
