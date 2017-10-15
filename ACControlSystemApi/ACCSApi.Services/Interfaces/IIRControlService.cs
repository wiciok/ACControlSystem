using ACCSApi.Model.Interfaces;

namespace ACCSApi.Services.Interfaces
{
    internal interface IIRControlService
    {
        void SendMessage(ICode code);
        void SendDefaultTurnOffMessage();
        void SendDefaultTurnOnMessage();
    }
}
