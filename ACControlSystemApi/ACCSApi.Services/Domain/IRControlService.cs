using System;
using ACCSApi.Model.Interfaces;
using ACCSApi.Model.Transferable;
using ACCSApi.Services.Interfaces;
using IRSlingerCsharp;

namespace ACCSApi.Services.Domain
{
    internal class IRControlService : IIRControlService
    {
        private readonly IIRSlingerCsharp _irService;
        private readonly IRaspberryPiDevice _hostDevice;
        private readonly IACDevice _acDevice;
        private readonly IACDeviceService _acDeviceService;
        private readonly IHostDeviceService _hostDeviceService;


        public IRControlService(IIRSlingerCsharp irService, IACDeviceService acDeviceService, IHostDeviceService hostDeviceService)
        {
            _irService = irService;
            _acDeviceService = acDeviceService;
            _hostDeviceService = hostDeviceService;

            _acDevice = _acDeviceService.GetCurrentDevice();
            _hostDevice = _hostDeviceService.GetCurrentDevice();
        }

        public void SendMessage(ICode code)
        {
            switch (code)
            {
                case NecCode nc:
                    if (_acDevice.NecCodeSettingsSaved)
                    {
                        _irService.SendNecMsg
                        (_hostDevice.BroadcomOutPin,
                            _acDevice.ModulationFrequencyInHz,
                            _acDevice.DutyCycle,
                            _acDevice.NecCodeSettings.LeadingPulseDuration,
                            _acDevice.NecCodeSettings.LeadingGapDuration,
                            _acDevice.NecCodeSettings.OnePulseDuration,
                            _acDevice.NecCodeSettings.ZeroPulseDuration,
                            _acDevice.NecCodeSettings.OneGapDuration,
                            _acDevice.NecCodeSettings.ZeroGapDuration,
                            _acDevice.NecCodeSettings.SendTrailingPulse,
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
                        _acDevice.ModulationFrequencyInHz,
                        _acDevice.DutyCycle,
                        rc.Code);
                    break;
                default:
                    throw new ArgumentException();
            }
        }
    }
}
