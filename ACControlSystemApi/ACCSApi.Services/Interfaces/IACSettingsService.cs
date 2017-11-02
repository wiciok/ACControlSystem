using System;
using System.Collections.Generic;
using System.Text;
using ACCSApi.Model.Interfaces;
using ACCSApi.Model.Transferable;

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
    }
}
