using ACCSApi.Model.Interfaces;
using System.Collections.Generic;

namespace ACControlSystemApi.Services.Interfaces
{
    public interface IACScheduleService
    {
        int AddNewSchedule(IACSchedule schedule);
        void DeleteSchedule(int id);
        IACSchedule GetSchedule(int id);
        IEnumerable<IACSchedule> GetAllSchedules();        
    }
}
