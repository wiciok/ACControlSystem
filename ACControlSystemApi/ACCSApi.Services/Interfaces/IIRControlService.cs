using ACCSApi.Model.Interfaces;

namespace ACControlSystemApi.Services.Interfaces
{
    internal interface IIRControlService
    {
        void SendMessage(ICode code);
        void SendDefaultTurnOffMessage();
        void SendDefaultTurnOnMessage();
    }
}
