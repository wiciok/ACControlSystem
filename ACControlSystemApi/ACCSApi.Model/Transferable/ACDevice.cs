using ACCSApi.Model.Interfaces;
using System;
using System.Collections.Generic;

namespace ACControlSystemApi.Model
{
    public class ACDevice : IACDevice, IACCSSerializable
    {
        private IACSetting _turnOffSetting;

        public int Id { get; set; }

        public string Model { get; set; }
        public string Brand { get; set; }

        // ir transmission related properties:
        public int ModulationFrequencyInHz { get; set; }
        public double DutyCycle { get; set; }

        public IList<IACSetting> AvailableSettings { get; set; }
        public IACSetting TurnOffSetting
        {
            get => _turnOffSetting;
            set
            {
                if (!value.IsTurnOff)
                    throw new ArgumentException("TurnOffSetting must have IACSetting object with property IsOff=true!");
                _turnOffSetting = value;
            }
        }
        public IACSetting DefaulTurnOnSetting { get; set; }
    }
}
