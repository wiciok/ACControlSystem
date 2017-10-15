using ACCSApi.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Linq;

namespace ACCSApi.Services.Utils
{
    internal interface IScheduleDispatcher
    {
        void AddSchedule(IACSchedule schedule, Timer timerStart, Timer timerEnd);
        void RemoveSchedule(IACSchedule schedule);
    }

    internal class ScheduleDispatcher: IScheduleDispatcher
    {
        private static IDictionary<IACSchedule, Tuple<Timer, Timer>> _schedulesTimersDict = new Dictionary<IACSchedule, Tuple<Timer, Timer>>();

        public void AddSchedule(IACSchedule schedule, Timer timerStart, Timer timerEnd)
        {
            _schedulesTimersDict.Add(schedule, new Tuple<Timer, Timer>(timerStart, timerEnd));
        }

        public void RemoveSchedule(IACSchedule schedule)
        {
            var dictEntry = _schedulesTimersDict.SingleOrDefault(x => x.Key.Equals(schedule)); //???
            throw new NotImplementedException(); //todo: deregistering callback
        }
    }
}
