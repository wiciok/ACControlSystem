using System;
using System.Linq;
using ACCSApi.Model.Interfaces;
using ACCSApi.Services.Interfaces;
using ACCSApi.Services.Models.Exceptions;

namespace ACCSApi.Services.Domain
{
    public class ACStateControlService : IACStateControlService
    {
        private readonly IIRControlService _irControlService;
        private readonly IACDeviceService _acDeviceService;
        private readonly IACDevice _currentAcDevice;

        public ACStateControlService(IIRControlService irControlService, IACDeviceService acDeviceService)
        {
            _irControlService = irControlService;
            _acDeviceService = acDeviceService;
            _currentAcDevice = _acDeviceService.GetCurrentDevice();
        }

        public void SetCurrentState(IACState newState)
        {
            ChangeACState(newState);
            _currentAcDevice.CurrentState = newState;
        }


        public IACState GetCurrentState()
        {
            if(_currentAcDevice==null)
                throw new CurrentACDeviceNotSetException();

            if (_currentAcDevice.CurrentState != null)
                return _currentAcDevice.CurrentState;
            throw new ACStateUndefinedException();
        }

        public void ChangeACSetting(IACSetting setting)
        {
            _irControlService.SendMessage(setting.Code);
        }

        private void ChangeACState(IACState newState)
        {
            if (_currentAcDevice == null)
                throw new CurrentACDeviceNotSetException();

            IACSetting acSetting = null;

            if (newState.ACSettingGuid != null)
            {
                acSetting = _currentAcDevice.AvailableSettings.SingleOrDefault(x => x.UniqueId.Equals(newState.ACSettingGuid));
            }

            //business logic:
            //if isoff=false and state null ->default on
            //isoff true - if state notnull and isoff - state code, if its not isoff - exception. if state null - default off
            //isoff null - what is in ACState
            //both null - exception

            switch (newState.IsTurnOff)
            {
                case null:
                    if (acSetting == null)
                        throw new ArgumentException("ACState is empty");
                    else
                        ChangeACSetting(acSetting);
                    break;

                case false:
                    if (acSetting == null)
                    {
                        if (_currentAcDevice.DefaultTurnOnSetting != null)
                            ChangeACSetting(_currentAcDevice.DefaultTurnOnSetting);
                        else
                            throw new InvalidOperationException("Default TurnOnSetting in ACDevice not specified!");
                    }

                    else
                    {
                        if (acSetting.IsTurnOff == false)
                            ChangeACSetting(acSetting);
                        else
                            throw new ArgumentException("IACState members inconsistency: IACState.IsTurnOff==false while IACState.ACSetting.IsTurnOff=true!");
                    }
                    break;

                case true:
                    if (acSetting == null)
                    {
                        if (_currentAcDevice.TurnOffSetting != null)
                            ChangeACSetting(_currentAcDevice.TurnOffSetting);
                        else
                            throw new InvalidOperationException("Default TurnOffSetting in ACDevice not specified!");
                    }
                    else
                    {
                        if (acSetting.IsTurnOff == true)
                            ChangeACSetting(acSetting);
                        else
                            throw new ArgumentException("IACState members inconsistency: IACState.IsTurnOff==true while IACState.ACSetting.IsTurnOff=false!");
                    }
                    break;
            }
        }
    }
}
