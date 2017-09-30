using ACControlSystemApi.Model;

namespace ACControlSystemApi.Services.Interfaces
{
    public interface IACStateControlService
    {
        void SetCurrentState(ACState newState);
        ACState GetCurrentState();
    }
}
