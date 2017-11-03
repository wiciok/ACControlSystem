using System;
using ACCSApi.Model.Enums;
using ACCSApi.Model.Interfaces;

namespace ACCSApi.Model
{
    public class ACSchedule : IACSchedule
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid? ACSettingGuid { get; set; }
        public ScheduleType ScheduleType { get; set; }
    }
}
