using System;

namespace ACCSApi.Model.Interfaces
{
    public interface IACState
    {
        bool? IsTurnOff { get; set; }
        Guid? ACSettingGuid { get; set; } //todo: think about changing it to setting id?
    }
}
