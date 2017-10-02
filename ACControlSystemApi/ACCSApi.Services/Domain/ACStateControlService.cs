using ACControlSystemApi.Services.Interfaces;
using ACCSApi.Model.Transferable;

namespace ACControlSystemApi.Services
{
    public class ACStateControlService: IACStateControlService
    {
        private ACSetting _currentState;

        public void SetCurrentState(ACSetting newState)
        {
            _currentState = newState;
        }

        public ACSetting GetCurrentState()
        {
            return _currentState;
        }
    }
}
