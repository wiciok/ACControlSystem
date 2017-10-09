using ACControlSystemApi.Services.Interfaces;
using ACCSApi.Model.Interfaces;
using System;

namespace ACControlSystemApi.Services
{
    public class ACStateControlService : IACStateControlService
    {
        private IACState _currentState;
        private IIRControlService _irControlService;

        internal ACStateControlService(IIRControlService irControlService) //todo: check if it can be internal or must be public
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
                        _irControlService.SendMessage(newState.ACSetting.Code);
                    break;

                case false:
                    if (newState.ACSetting == null)
                        _irControlService.SendDefaultTurnOnMessage();
                    else
                    {
                        if (newState.ACSetting.IsTurnOff == false)
                            _irControlService.SendMessage(newState.ACSetting.Code);
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
                            _irControlService.SendMessage(newState.ACSetting.Code);
                        else
                            throw new ArgumentException("IACState members inconsistency: IACState.IsTurnOff==true while IACState.ACSetting.IsTurnOff=false!");
                    }
                    break;
            }
        }
    }
}
