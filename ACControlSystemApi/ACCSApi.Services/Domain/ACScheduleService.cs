using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ACCSApi.Model.Enums;
using ACCSApi.Model.Interfaces;
using ACCSApi.Model.Transferable;
using ACCSApi.Repositories.Interfaces;
using ACCSApi.Services.Interfaces;
using ACCSApi.Services.Utils;

namespace ACCSApi.Services.Domain
{
    //todo: finish implementation
    //todo: przemyslec usuwanie scheduli ktore nie sa juz aktualne
    public class ACScheduleService : IACScheduleService
    {
        private readonly IACScheduleRepository _scheduleRepository;
        private readonly IScheduleDispatcher _scheduleDispatcher;
        private readonly IACStateControlService _acStateControlService;
        private IACState _turnOffState;

        internal ACScheduleService(IACScheduleRepository scheduleRepository, IScheduleDispatcher scheduleDispatcher, IACStateControlService stateControlService)
        {
            _acStateControlService = stateControlService;
            _scheduleRepository = scheduleRepository;
            _scheduleDispatcher = scheduleDispatcher;

            Initialize();
        }

        private void Initialize()
        {
            _turnOffState = new ACState() { IsTurnOff = true };
        }

        private void ChangeACSettings(object arg)
        {
            switch (arg)
            {
                case IACSetting setting:
                    _acStateControlService.ChangeACSetting(setting);
                    return;
                case true:
                    _acStateControlService.SetCurrentState(_turnOffState);
                    return;
                case Tuple<IACSchedule, bool> tuple:    //ScheduleType.DayOfWeek
                    var schedule = tuple.Item1;
                    var isTurnOff = tuple.Item2;

                    if (!schedule.ScheduleType.Equals(ScheduleType.EveryDayOfWeek))
                        throw new ArgumentException("ScheduleType should be EveryDayOfWeek");

                    var startTimeDayOfWeek = schedule.StartTime.DayOfWeek;
                    var endTimeDayOfWeek = schedule.EndTime.DayOfWeek;

                    if (isTurnOff)
                    {
                        if (endTimeDayOfWeek.Equals(DateTime.Now.DayOfWeek))
                            _acStateControlService.SetCurrentState(_turnOffState);
                        return;
                    }

                    if (startTimeDayOfWeek.Equals(DateTime.Now.DayOfWeek))
                        _acStateControlService.ChangeACSetting(schedule.ACSetting);
                    break;
                default:
                    throw new ArgumentException("ChangeACSettings: argument is not of IACSetting type");
            }
        }

        private bool CheckScheduleTimesValidity(IACSchedule schedule)
        {
            return schedule.StartTime > DateTime.Now 
                && schedule.EndTime > DateTime.Now
                && schedule.StartTime < schedule.EndTime;
        }

        private bool CheckIfAddedScheduleOverlapsExisting(IACSchedule newSchedule)
        {
            var allSchedules = _scheduleRepository.GetAll();
            var overlappingSchedules = allSchedules.Where(x =>
                newSchedule.StartTime <= x.StartTime && newSchedule.EndTime >= x.EndTime
                || newSchedule.StartTime <= x.EndTime && newSchedule.EndTime >= x.StartTime
                || newSchedule.StartTime >= x.StartTime && newSchedule.EndTime <= x.EndTime
                || newSchedule.StartTime <= x.EndTime && newSchedule.EndTime >= x.EndTime).ToList();

            return overlappingSchedules.Any();
        }

        public int AddNewSchedule(IACSchedule schedule)
        {
            if (schedule == null)
                throw new ArgumentNullException(nameof(schedule));

            if (!CheckScheduleTimesValidity(schedule))
                throw new ArgumentException("ACSchedule have incorrect Start and/or End Times!");

            if (!CheckIfAddedScheduleOverlapsExisting(schedule))
                throw new ArgumentException("Currently adding schedule overlaps some of existing schedules");
            

            var timerCallback = new TimerCallback(ChangeACSettings);
            TimeSpan period;
            object timerStartCallbackArg = schedule.ACSetting;
            object timerStopCallbackArg = true;

            switch (schedule.ScheduleType)
            {
                case ScheduleType.Single:
                    period = new TimeSpan(-1);
                    break;

                case ScheduleType.EveryHour:
                    period = new TimeSpan(1, 0, 0);
                    break;

                case ScheduleType.EveryDay:
                    period = new TimeSpan(24, 0, 0);
                    break;
                case ScheduleType.EveryDayOfWeek:
                    period = new TimeSpan(24, 0, 0);
                    timerStartCallbackArg = new Tuple<IACSchedule, bool>(schedule, false);
                    timerStopCallbackArg = new Tuple<IACSchedule, bool>(schedule, true);
                    //day of week logic is implemented in ChangeACSetting method
                    break;
            }

            var timerStart = new Timer(timerCallback, timerStartCallbackArg, schedule.StartTime - DateTime.Now, period);
            var timerStop = new Timer(timerCallback, timerStopCallbackArg, schedule.EndTime - DateTime.Now, period);

            _scheduleDispatcher.AddSchedule(schedule, timerStart, timerStop);
            return _scheduleRepository.Add(schedule);
        }

        public void DeleteSchedule(int id)
        {
            throw new NotImplementedException();
            //_scheduleRepository
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
