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

        int? LeadingPulseDuration { get; set; }
        int? LeadingGapDuration { get; set; }
        int? OnePulseDuration { get; set; }
        int? ZeroPulseDuration { get; set; }
        int? OneGapDuration { get; set; }
        int? ZeroGapDuration { get; set; }
        bool? SendTrailingPulse { get; set; }

        bool NecPulseDurationSettingsSaved { get; }
        void ResetSavedNecPulseDurationSettings();

        IList<IACSetting> AvailableSettings { get; }
        IACSetting TurnOffSetting { get; set; }
        IACSetting DefaulTurnOnSetting { get; set; }
    }
}
