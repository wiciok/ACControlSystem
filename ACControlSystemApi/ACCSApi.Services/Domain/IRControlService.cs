using ACControlSystemApi.Model;
using ACControlSystemApi.Services.Interfaces;
using ACCSApi.Model.Interfaces;
using ACCSApi.Repositories.Interfaces;
using IRSlingerCsharp;
using System;

namespace ACControlSystemApi.Services
{
    internal class IRControlService: IIRControlService
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
            if (code is NecCode nc)
            {
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
            }

            else if (code is RawCode rc)
            {
                _irService.SendRawMsg
                    (_hostDevice.BroadcomOutPin,
                    _ACDevice.ModulationFrequencyInHz,
                    _ACDevice.DutyCycle,
                    rc.Code);
            }
            else
                throw new ArgumentException();
        }

        //todo: add null checking on turnoffcode and turnoncode properties!

        public void SendDefaultTurnOffMessage()
        {
            SendMessage(TurnOffCode);
        }

        public void SendDefaultTurnOnMessage()
        {
            SendMessage(TurnOnCode);
        }
    }
}
