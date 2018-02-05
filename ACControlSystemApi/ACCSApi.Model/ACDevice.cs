using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ACCSApi.Model.Interfaces;

namespace ACCSApi.Model
{
    public class ACDevice : IACDevice
    {
        private int _id;
        private string _model;
        private string _brand;
        private int _modulationFreq;
        private double _dutyCycle;
        private NecCodeSettings _necCodeSettings;
        private ObservableCollection<IACSetting> _settingsList;
        private IACSetting _defaultTurnOn;
        private IACSetting _defaultTurnOff;
        private IACState _currentState;

        public ACDevice()
        {
            TurnOffSetting = null;
            DefaultTurnOnSetting = null;

            _settingsList = new ObservableCollection<IACSetting>();
            _settingsList.CollectionChanged += _settingsList_CollectionChanged;
        }

        private void _settingsList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            OnChanged?.Invoke();
        }

        public event Action OnChanged;

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnChanged?.Invoke();
            }
        }
        public string Model
        {
            get => _model;
            set
            {
                _model = value;
                OnChanged?.Invoke();
            }
        }

        public string Brand
        {
            get => _brand;
            set
            {
                _brand = value;
                OnChanged?.Invoke();
            }
        }

        //ir control related properties
        public int ModulationFrequencyInHz
        {
            get => _modulationFreq;
            set
            {
                _modulationFreq = value;
                OnChanged?.Invoke();
            }
        }
        public double DutyCycle
        {
            get => _dutyCycle;
            set
            {
                _dutyCycle = value;
                OnChanged?.Invoke();
            }
        }

        public NecCodeSettings NecCodeSettings
        {
            get => _necCodeSettings;
            set
            {
                _necCodeSettings = value;
                OnChanged?.Invoke();
            }
        }
        public bool NecCodeSettingsSaved => NecCodeSettings != null;

        public IList<IACSetting> AvailableSettings
        {
            get => _settingsList;
            set
            {
                if (_settingsList != null)
                {
                    _settingsList.CollectionChanged -= _settingsList_CollectionChanged;
                }
                if (value == null)
                    return;

                _settingsList = new ObservableCollection<IACSetting>(value);
                _settingsList.CollectionChanged += _settingsList_CollectionChanged;
                OnChanged?.Invoke();
            }
        }
        public IACSetting TurnOffSetting
        {
            get => _defaultTurnOff;
            set
            {
                if (value != null && !value.IsTurnOff)
                    throw new ArgumentException("TurnOffSetting must have IACSetting object with property IsOff=true!");
                _defaultTurnOff = value;
                OnChanged?.Invoke();
            }
        }
        public IACSetting DefaultTurnOnSetting
        {
            get => _defaultTurnOn;
            set
            {
                _defaultTurnOn = value;
                OnChanged?.Invoke();
            }
        }

        public IACState CurrentState
        {
            get => _currentState;
            set
            {
                _currentState = value;
                OnChanged?.Invoke();
            }
        }
    }
}
