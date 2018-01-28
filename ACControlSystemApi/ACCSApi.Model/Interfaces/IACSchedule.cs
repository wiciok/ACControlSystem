using ACCSApi.Model.Enums;
using System;

namespace ACCSApi.Model.Interfaces
{
    public interface IACSchedule: IACCSSerializable
    {
        DateTime StartTime { get; set; }
        DateTime EndTime { get; set; }
        Guid? ACSettingGuid { get; }
        ScheduleType ScheduleType { get;}
    }
}
