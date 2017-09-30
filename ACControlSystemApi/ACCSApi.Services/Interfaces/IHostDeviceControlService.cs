using ACControlSystemApi.Model.Interfaces;

namespace ACControlSystemApi.Services.Interfaces
{
    public interface IHostDeviceControlService
    {
        void SendMessage(ICode code);
        void SendMessageById(int id);
        void SendTurnOffMessage();
        void SendTurnOnMessage();
    }
}
