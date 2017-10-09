using ACCSApi.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using ACCSApi.Model.Enums;

namespace ACCSApi.Model.Transferable
{
    public class ACSchedule : IACSchedule
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public IACSetting ACSetting { get; set; }
        public ScheduleType ScheduleType { get; set; }
    }
}
