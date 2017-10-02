using ACCSApi.Model.Interfaces;

namespace ACControlSystemApi.Services.Interfaces
{
    public interface IACStateControlService
    {
        void SetCurrentState(IACState newState);
        IACState GetCurrentState();
    }
}
