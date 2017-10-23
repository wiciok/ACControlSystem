using System;
using ACCSApi.Model.Interfaces;
using ACCSApi.Services.Interfaces;

namespace ACCSApi.Services.Domain
{
    public class ACStateControlService : IACStateControlService
    {
        private IACState _currentState;
        private readonly IIRControlService _irControlService;

        public ACStateControlService(IIRControlService irControlService)
        {
            _irControlService = irControlService;
        }

        public void SetCurrentState(IACState newState)
        {
            ChangeACState(newState);
            _currentState = newState;            
        }


        public IACState GetCurrentState()
        {
            return _currentState;
        }

        public void ChangeACSetting(IACSetting setting)
        {
            _irControlService.SendMessage(setting.Code);
        }

        private void ChangeACState(IACState newState)
        {
            //business logic:
            //if isoff=false and state null ->default on
            //isoff true - if state notnull and isoff - state code, if its not isoff - exception. if state null - default off
            //isoff null - what is in ACState
            //both null - exception

            switch (newState.IsTurnOff)
            {
                case null:
                    if (newState.ACSetting == null)
                        throw new ArgumentNullException("ACState have both its members null!"); //todo: change this message to be more specific
                    else
                        ChangeACSetting(newState.ACSetting);
                    break;

                case false:
                    if (newState.ACSetting == null)
                        _irControlService.SendDefaultTurnOnMessage();
                    else
                    {
                        if (newState.ACSetting.IsTurnOff == false)
                            ChangeACSetting(newState.ACSetting);
                        else
                            throw new ArgumentException("IACState members inconsistency: IACState.IsTurnOff==false while IACState.ACSetting.IsTurnOff=true!");
                    }
                    break;

                case true:
                    if (newState.ACSetting == null)
                    {
                        _irControlService.SendDefaultTurnOffMessage();
                    }
                    else
                    {
                        if (newState.ACSetting.IsTurnOff == true)
                            ChangeACSetting(newState.ACSetting);
                        else
                            throw new ArgumentException("IACState members inconsistency: IACState.IsTurnOff==true while IACState.ACSetting.IsTurnOff=false!");
                    }
                    break;
            }
        }
    }
}
