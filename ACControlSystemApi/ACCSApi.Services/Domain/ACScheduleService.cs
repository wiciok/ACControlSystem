using System.Collections.Generic;
using ACControlSystemApi.Services.Interfaces;
using ACCSApi.Model.Interfaces;
using System;
using ACCSApi.Repositories.Specific;
using ACCSApi.Repositories.Interfaces;
using ACCSApi.Services.Utils;

namespace ACControlSystemApi.Services
{
    public class ACScheduleService : IACScheduleService
    {
        private IACScheduleRepository _scheduleRepository;
        private IScheduleDispatcher _scheduleDispatcher;

        internal ACScheduleService(IACScheduleRepository scheduleRepository, IScheduleDispatcher scheduleDispatcher)
        {
            _scheduleRepository = scheduleRepository;
            _scheduleDispatcher = scheduleDispatcher;
        }


        public int AddNewSchedule(IACSchedule schedule)
        {
            throw new NotImplementedException();
        }

        public void DeleteSchedule(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IACSchedule> GetAllSchedules()
        {
            throw new NotImplementedException();
        }

        public IACSchedule GetSchedule(int id)
        {
            throw new NotImplementedException();
        }
    }
}
