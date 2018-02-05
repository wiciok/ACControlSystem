using ACCSApi.Model;

namespace ACCSApi.Services.Interfaces
{
    public interface ICodeRecordingService
    {
        RawCode RecordRawCode();
        (NecCode, NecCodeSettings) RecordNecCode();
    }
}
