using ACCSApi.Model.Interfaces;

namespace ACCSApi.Services.Interfaces
{
    public interface IIRControlService
    {
        void SendMessage(ICode code);
        void SendDefaultTurnOffMessage();
        void SendDefaultTurnOnMessage();
    }
}
