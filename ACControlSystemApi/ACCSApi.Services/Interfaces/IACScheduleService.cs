using System.Collections.Generic;
using ACCSApi.Model.Interfaces;

namespace ACCSApi.Services.Interfaces
{
    public interface IACScheduleService
    {
        int AddNewSchedule(IACSchedule schedule);
        void DeleteSchedule(int id);
        IACSchedule GetSchedule(int id);
        IEnumerable<IACSchedule> GetAllCurrentDeviceSchedules();
        void RegisterAllSchedulesFromRepository();
    }
}
