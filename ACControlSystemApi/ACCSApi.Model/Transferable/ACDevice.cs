using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using ACCSApi.Model.Interfaces;

namespace ACCSApi.Model.Transferable
{
    public class ACDevice : IACDevice, IACCSSerializable
    {
        private IACSetting _turnOffSetting;

        public ACDevice()
        {
            TurnOffSetting = null;
            DefaulTurnOnSetting = null;
        }

        public int Id { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }

        // ir transmission related properties:
        public int ModulationFrequencyInHz { get; set; }
        public double DutyCycle { get; set; } = 0.5;

        public int? LeadingPulseDuration { get; set; }
        public int? LeadingGapDuration { get; set; }
        public int? OnePulseDuration { get; set; }
        public int? ZeroPulseDuration { get; set; }
        public int? OneGapDuration { get; set; }
        public int? ZeroGapDuration { get; set; }
        public bool? SendTrailingPulse { get; set; }

        public bool NecPulseDurationSettingsSaved => LeadingPulseDuration != null
                                                     && LeadingGapDuration != null
                                                     && OneGapDuration != null
                                                     && OnePulseDuration != null
                                                     && ZeroGapDuration != null
                                                     && ZeroPulseDuration != null
                                                     && SendTrailingPulse != null;

        public void ResetSavedNecPulseDurationSettings()
        {
            LeadingPulseDuration = null;
            LeadingGapDuration = null;
            OneGapDuration = null;
            OnePulseDuration = null;
            ZeroGapDuration = null;
            ZeroPulseDuration = null;
            SendTrailingPulse = null;
        }
        

        public IList<IACSetting> AvailableSettings { get; set; }
        public IACSetting TurnOffSetting
        {
            get => _turnOffSetting;
            set
            {
                if (value != null && !value.IsTurnOff)
                    throw new ArgumentException("TurnOffSetting must have IACSetting object with property IsOff=true!");
                _turnOffSetting = value;
            }
        }
        public IACSetting DefaulTurnOnSetting { get; set; }
    }
}
