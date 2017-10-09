using ACCSApi.Model.Enums;
using System;

namespace ACCSApi.Model.Interfaces
{
    public interface IACSchedule: IACCSSerializable
    {
        DateTime StartTime { get; }
        DateTime EndTime { get; }
        IACSetting ACSetting { get; } //todo: think about changing it to setting id //todo: think to change it to ACState?
        ScheduleType ScheduleType { get; }
    }
}
