using ACCSApi.Model.Enums;
using System;

namespace ACCSApi.Model.Interfaces
{
    public interface IACSchedule: IACCSSerializable
    {
        DateTime StartTime { get; }
        DateTime EndTime { get; }
        Guid? ACSettingGuid { get; }
        ScheduleType ScheduleType { get; }
    }
}
