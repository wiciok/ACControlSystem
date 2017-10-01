using ACCSApi.Model.Transferable;

namespace ACControlSystemApi.Services.Interfaces
{
    public interface IACStateControlService
    {
        void SetCurrentState(ACState newState);
        ACState GetCurrentState();
    }
}
