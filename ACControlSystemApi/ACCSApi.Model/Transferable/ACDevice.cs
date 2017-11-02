using System;
using System.Collections.Generic;
using ACCSApi.Model.Interfaces;

namespace ACCSApi.Model.Transferable
{
    public class ACDevice : IACDevice, IACCSSerializable
    {
        private IACSetting _turnOffSetting;

        public ACDevice()
        {
            TurnOffSetting = null;
            DefaultTurnOnSetting = null;
        }

        public int Id { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }

        //ir control related properties
        public int ModulationFrequencyInHz { get; set; }
        public double DutyCycle { get; set; }

        public NecCodeSettings NecCodeSettings { get; set; }     
        public bool NecCodeSettingsSaved => NecCodeSettings!=null;        

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
        public IACSetting DefaultTurnOnSetting { get; set; }
    }
}
