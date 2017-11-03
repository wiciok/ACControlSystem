using System;
using System.Collections.Generic;

namespace ACCSApi.Model.Interfaces
{
    public interface IACDevice: IACCSSerializable
    {
        event Action OnChanged;

        string Model { get; set; }
        string Brand { get; set; }

        //ir control related properties
        int ModulationFrequencyInHz { get; set; }
        double DutyCycle { get; set; }

        NecCodeSettings NecCodeSettings { get; set; }
        bool NecCodeSettingsSaved { get; }

        IList<IACSetting> AvailableSettings { get; }
        IACSetting TurnOffSetting { get; set; }
        IACSetting DefaultTurnOnSetting { get; set; }
    }
}
