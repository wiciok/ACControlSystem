using ACControlSystemApi.Model;

namespace ACControlSystemApi.Services
{
    public class ACStateControlService
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
