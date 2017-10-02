using System.Collections.Generic;

namespace ACCSApi.Model.Interfaces
{
    public interface IACDevice: IACCSSerializable
    {
        string Model { get; set; }
        string Brand { get; set; }

        // ir transmission related properties:
        int ModulationFrequencyInHz { get; set; }
        double DutyCycle { get; set; }

        IList<IACSetting> AvailableSettings { get; }
        IACSetting TurnOffSetting { get; set; }
        IACSetting DefaulTurnOnSetting { get; set; }
    }
}
