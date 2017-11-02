using System;
using System.Collections.Generic;
using System.Text;
using ACCSApi.Model.Transferable;

namespace ACCSApi.Services.Interfaces
{
    public interface ICodeRecordingService
    {
        void ResetCurrentAcDeviceNecCodeSettings();
        RawCode RecordRawCode();
        NecCode RecordNecCode();
    }
}
