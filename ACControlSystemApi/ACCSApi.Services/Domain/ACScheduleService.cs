using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ACCSApi.Model;
using ACCSApi.Model.Enums;
using ACCSApi.Model.Interfaces;
using ACCSApi.Repositories.Interfaces;
using ACCSApi.Services.Interfaces;
using ACCSApi.Services.Models.Exceptions;

namespace ACCSApi.Services.Domain
{
    public class ACScheduleService : IACScheduleService
    {
        private readonly IACScheduleRepository _scheduleRepository;
        private readonly IACStateControlService _acStateControlService;
        private readonly IACDevice _currentDevice;
        private readonly IACState _turnOffState = new ACState { IsTurnOff = true };
        private static readonly IDictionary<IACSchedule, Tuple<Timer, Timer>> SchedulesTimersDict = new Dictionary<IACSchedule, Tuple<Timer, Timer>>();
        private static bool _isFirstInstance = true;

        public ACScheduleService(IACScheduleRepository scheduleRepository, IACStateControlService stateControlService, IACDeviceService acDeviceService)
        {
            _acStateControlService = stateControlService;
            _scheduleRepository = scheduleRepository;
            _currentDevice = acDeviceService.GetCurrentDevice();

            if(_isFirstInstance)
                RegisterAllSchedulesFromRepository();
            _isFirstInstance = false;
        }


        public int AddNewSchedule(IACSchedule schedule)
        {
            RegisterSchedule(schedule);
            return _scheduleRepository.Add(schedule);
        }

        public void DeleteSchedule(int id)
        {
            var scheduleToDel = _scheduleRepository.Find(x => x.Id.Equals(id)).SingleOrDefault();
            if (scheduleToDel == null)
                throw new ItemNotFoundException($"ACSchedule of id={id} not found in repository!");
            DeregisterSchedule(scheduleToDel);
            _scheduleRepository.Delete(scheduleToDel);
        }

        public IEnumerable<IACSchedule> GetAllSchedules()
        {
            return _scheduleRepository.GetAll().ToList();
        }

        public IACSchedule GetSchedule(int id)
        {
            var schedule = _scheduleRepository.Find(x => x.Id.Equals(id)).SingleOrDefault();
            if (schedule == null)
                throw new ItemNotFoundException($"ACSchedule of id={id} not found in repository!");
            return schedule;
        }

        public void RegisterAllSchedulesFromRepository()
        {
            var allSchedules = _scheduleRepository.GetAll();
            foreach (var sched in allSchedules)
            {
                try
                {
                    RegisterSchedule(sched);
                }
                catch (ArgumentException e)
                {
                    _scheduleRepository.Delete(sched.Id);
                    //todo: logging
                }
            }
        }

        private void RegisterSchedule(IACSchedule schedule)
        {
            VerifySchedule(schedule);

            var acSetting =_currentDevice.AvailableSettings.Single(x => x.UniqueId.Equals(schedule.ACSettingGuid));

            var timerCallback = new TimerCallback(ChangeACSettings);
            TimeSpan period;
            object timerStartCallbackArg = acSetting;
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

            SchedulesTimersDict.Add(schedule, new Tuple<Timer, Timer>(timerStart, timerStop));
        }

        private void DeregisterSchedule(IACSchedule schedule)
        {
            var dictEntry = SchedulesTimersDict.SingleOrDefault(x => x.Key.Equals(schedule));
            var timerStart = dictEntry.Value.Item1;
            var timerStop = dictEntry.Value.Item2;
            SchedulesTimersDict.Remove(schedule);
            timerStart.Dispose();
            timerStop.Dispose();
        }

        private void VerifySchedule(IACSchedule schedule)
        {
            if (schedule == null)
                throw new ArgumentNullException(nameof(schedule));

            if (!CheckScheduleTimesValidity(schedule))
                throw new ArgumentException("ACSchedule have incorrect Start and/or End Times!");

            if (!CheckIfAddedScheduleOverlapsExisting(schedule))
                throw new ArgumentException("Currently adding schedule overlaps some of existing schedules");
        }

        private bool CheckScheduleTimesValidity(IACSchedule schedule)
        {
            if (schedule.ScheduleType != ScheduleType.Single)
                return true;
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
                    {
                        var acSetting = _currentDevice.AvailableSettings.Single(x => x.UniqueId.Equals(schedule.ACSettingGuid));
                        _acStateControlService.ChangeACSetting(acSetting);
                    }
                       
                    break;
                default:
                    throw new ArgumentException("ChangeACSettings: argument is not of IACSetting type");
            }
        }
    }
}
