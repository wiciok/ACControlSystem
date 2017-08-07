using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ACControlSystemApi.Model;

namespace ACControlSystemApi.Services
{
    public class ACControlService
    {
        private ACControlStates _currentState;

        public ACControlStates CurrentState
        {
            get
            {
                return _currentState;
            }

            set
            {
                _currentState = value;
            }
        }

        private bool ChangeCurrentState(ACControlStates newState)
        {
            throw new NotImplementedException();
        }
    }
}
