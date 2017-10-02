using ACCSApi.Model.Interfaces;

namespace ACControlSystemApi.Services.Interfaces
{
    internal interface IIRControlService
    {
        void SendMessage(ICode code);
        void SendTurnOffMessage();
        void SendTurnOnMessage();
    }
}
