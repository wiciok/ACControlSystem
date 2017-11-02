using System;
using System.Collections.Generic;
using System.Linq;
using ACCSApi.Model.Interfaces;
using ACCSApi.Model.Transferable;
using ACCSApi.Services.Interfaces;
using ACCSApi.Services.Models.Exceptions;

namespace ACCSApi.Services.Domain
{
    public class ACSettingsService: IACSettingsService
    {
        private readonly IACDevice _currentAcDevice;
        private readonly ICodeRecordingService _codeRecordingService;

        public ACSettingsService(IACDeviceService acDeviceService, ICodeRecordingService codeRecordingService)
        {
            _codeRecordingService = codeRecordingService;
            _currentAcDevice = acDeviceService.GetCurrentDevice();
        }

        //todo: think about changing it to one, chosen by some king of entry in globalSettings
        public Guid AddRaw(AcSettingAdd settingDto)
        {
            var code = _codeRecordingService.RecordRawCode();

            var acSetting = new ACSetting()
            {
                
                Code = code,
                IsTurnOff = settingDto.IsTurnOff,
                Settings = settingDto.Settings
            };
            _currentAcDevice.AvailableSettings.Add(acSetting);
            return acSetting.UniqueId;
        }

        public Guid AddNec(AcSettingAdd settingDto)
        {
            var code = _codeRecordingService.RecordNecCode();

            var acSetting = new ACSetting()
            {

                Code = code,
                IsTurnOff = settingDto.IsTurnOff,
                Settings = settingDto.Settings
            };
            _currentAcDevice.AvailableSettings.Add(acSetting);
            return acSetting.UniqueId;
        }

        public IACSetting Get(Guid guid)
        {
            var acSetting = _currentAcDevice.AvailableSettings.SingleOrDefault(x => x.UniqueId.Equals(guid));
            if(acSetting==null)
                throw new ItemNotFoundException("ACSetting with guid {guid} not found in current ACDevice available settings list");
            return acSetting;
        }

        public IEnumerable<IACSetting> GetAll()
        {
            return _currentAcDevice.AvailableSettings;
        }

        public void Delete(Guid guid)
        {
            var acSetting = _currentAcDevice.AvailableSettings.SingleOrDefault(x => x.UniqueId.Equals(guid));
            if (acSetting == null)
                throw new ItemNotFoundException("Cannot remove - ACSetting with guid {guid} not found in current ACDevice available settings list");
            _currentAcDevice.AvailableSettings.Remove(acSetting);
        }

        public IACSetting Update(IACSetting setting)
        {
            var acSetting = _currentAcDevice.AvailableSettings.SingleOrDefault(x => x.UniqueId.Equals(setting.UniqueId));
            if (acSetting == null)
                throw new ItemNotFoundException("Cannot update - ACSetting with guid {guid} not found in current ACDevice available settings list");
            acSetting = setting;
            return acSetting;
        }
    }
}
