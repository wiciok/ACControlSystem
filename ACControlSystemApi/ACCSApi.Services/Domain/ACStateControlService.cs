using ACControlSystemApi.Model;
using ACControlSystemApi.Services.Interfaces;
using ACCSApi.Model.Transferable;

namespace ACControlSystemApi.Services
{
    public class ACStateControlService: IACStateControlService
    {
        private ACState _currentState;

        public void SetCurrentState(ACState newState)
        {
            _currentState = newState;
        }

        public ACState GetCurrentState()
        {
            return _currentState;
        }
    }
}
