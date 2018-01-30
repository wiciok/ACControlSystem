using System;
using ACCSApi.Model.Enums;
using ACCSApi.Model.Interfaces;
using Newtonsoft.Json;

namespace ACCSApi.Model
{
    public class ACSchedule : IACSchedule
    {
        public ACSchedule()
        {

        }

        [JsonConstructor]
        public ACSchedule(int id, DateTime startTime, DateTime endTime, Guid? acSettingGuid, ScheduleType scheduleType)
        {
            Id = id;
            StartTime = startTime;
            EndTime = endTime;
            ACSettingGuid = acSettingGuid;
            ScheduleType = scheduleType;
        }

        public ACSchedule(IACSchedule oldSchedule, DateTime startTime, DateTime endTime)
        {
            this.Id = oldSchedule.Id;
            this.StartTime = startTime;
            this.EndTime = endTime;
            this.ACSettingGuid = oldSchedule.ACSettingGuid;
            this.ScheduleType = oldSchedule.ScheduleType;
        }

        public int Id { get; set; }
        public DateTime StartTime { get; }
        public DateTime EndTime { get; }
        public Guid? ACSettingGuid { get; set; }
        public ScheduleType ScheduleType { get; }

        public override int GetHashCode()
        {
            return StartTime.GetHashCode() + EndTime.GetHashCode() + ScheduleType.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ACSchedule sched))
                return false;
            return sched.StartTime.Equals(this.StartTime)
                   && sched.EndTime.Equals(this.EndTime)
                   && sched.ACSettingGuid.Equals(this.ACSettingGuid)
                   && sched.Id.Equals(this.Id)
                   && sched.ScheduleType.Equals(this.ScheduleType);
        }
    }
}
