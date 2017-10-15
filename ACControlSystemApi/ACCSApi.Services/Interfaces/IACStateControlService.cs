using ACCSApi.Model.Interfaces;

namespace ACCSApi.Services.Interfaces
{
    public interface IACStateControlService
    {
        void SetCurrentState(IACState newState);
        IACState GetCurrentState();
        void ChangeACSetting(IACSetting setting);
    }
}
