using ACCSApi.Model;

namespace ACCSApi.Services.Interfaces
{
    public interface ICodeRecordingService
    {
        void ResetCurrentAcDeviceNecCodeSettings();
        RawCode RecordRawCode();
        NecCode RecordNecCode();
    }
}
