using ACControlSystemApi.Model;
using ACControlSystemApi.Repositories;
using ACControlSystemApi.Services.Interfaces;
using IRSlingerCsharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ACControlSystemApi.Services
{
    public class HostDeviceControlService: IHostDeviceControlService
    {
        private IIRSlingerCsharp _irService;
        private IRaspberryPiDeviceRepository _hardwareDevicesRepo;
        private IACDeviceRepository _acDevicesRepo;

        private RaspberryPiDevice _hostDevice;
        private ACDevice _ACDevice;

        public ICode TurnOnCode { get; set; }
        public ICode TurnOffCode { get; set; }


        public HostDeviceControlService(IIRSlingerCsharp irService, IRaspberryPiDeviceRepository hardwareDevicesRepo, IACDeviceRepository acDeviceRepo)
        {
            _irService = irService;
            _hardwareDevicesRepo = hardwareDevicesRepo;
            _acDevicesRepo = acDeviceRepo;

            _ACDevice = acDeviceRepo.CurrentACDevice;
            _hostDevice = _hardwareDevicesRepo.GetCurrentDevice();
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
                    nc.Code.ToString());
            }

            else if (code is RawCode rc)
            {
                _irService.SendRawMsg
                    (_hostDevice.BroadcomOutPin,
                    _ACDevice.ModulationFrequencyInHz,
                    _ACDevice.DutyCycle,
                    rc.Code.ToString());
            }
            else
                throw new ArgumentException();
        }

        public void SendMessageById(int id)
        {
            SendMessage(_ACDevice.AvailableCodes.SingleOrDefault(x => x.Id == id));
        }

        public void SendTurnOffMessage()
        {
            SendMessage(TurnOffCode);
        }

        public void SendTurnOnMessage()
        {
            SendMessage(TurnOnCode);
        }
    }
}
