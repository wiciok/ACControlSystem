using System;
using System.Collections.Generic;
using ACCSApi.Model.Dto;
using ACCSApi.Model.Interfaces;

namespace ACCSApi.Services.Interfaces
{
    public interface IACSettingsService
    {
        Guid AddRaw(AcSettingAdd settingDto);
        Guid AddNec(AcSettingAdd setting);
        IACSetting Get(Guid guid);
        IEnumerable<IACSetting> GetAll();
        void Delete(Guid guid);
        IACSetting Update(IACSetting setting);

        IACSetting GetDefaultOn();
        IACSetting GetDefaultOff();
        IACSetting SetDefaultOn(Guid defaultOnGuid);
        IACSetting SetDefaultOff(Guid defaultOffGuid);
    }
}
