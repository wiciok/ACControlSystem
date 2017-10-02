using ACControlSystemApi.Services.Interfaces;
using ACCSApi.Model.Interfaces;
using ACCSApi.Model.Transferable;
using System;

namespace ACControlSystemApi.Services
{
    public class ACStateControlService: IACStateControlService
    {
        private IACState _currentState;
        private IIRControlService _irControlService;

        internal ACStateControlService(IIRControlService irControlService) //todo: check if it can be internal or must be public
        {
            _irControlService = irControlService;
        }

        public void SetCurrentState(IACState newState)
        {
            _currentState = newState;
            ChangeACState(newState);
        }

        public IACState GetCurrentState()
        {
            return _currentState;
        }

        private void ChangeACState(IACState newState)
        {
           // ValidateNewState(newState);

            //TODO: implement business logic
            //if isoff=false and state null ->default on
            //isoff true - if state notnull and isoff - state code, if its not isoff - exception. if state null - default off
            //isoff null - what is in ACState
            //both null - exception
            throw new NotImplementedException();
        }

        private void ValidateNewState(IACState newState)
        {
            throw new NotImplementedException();

            if (newState.ACSetting == null || newState.ACSetting.Code == null)
                throw new ArgumentNullException("New ACState have missing properties");
            if (newState.IsTurnOff == true && newState.ACSetting.IsTurnOff != true)
                throw new ArgumentException("New ACState is inconsistent");
        }
    }
}
