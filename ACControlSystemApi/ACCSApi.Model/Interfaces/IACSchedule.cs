using ACCSApi.Model.Enums;
using System;

namespace ACCSApi.Model.Interfaces
{
    public interface IACSchedule
    {
        DateTime StartTime { get; }
        DateTime EndTime { get; }
        IACSetting ACSetting { get; } //todo: think about changing it to setting id
        ScheduleType ScheduleType { get; }
    }
}
