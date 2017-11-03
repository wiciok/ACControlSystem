using System;
using System.Collections.Generic;
using System.Linq;
using ACCSApi.Model.Interfaces;

namespace ACCSApi.Model
{
    [Serializable]
    public class RaspberryPiDevice : IRaspberryPiDevice
    {
        public RaspberryPiDevice()
        {
        }

        public RaspberryPiDevice(string name, IDictionary<uint, uint> validPins)
        {
            Name = name;
            ValidBoardAndBroadcomPins = validPins;
        }

        private IDictionary<uint, uint> _validBoardAndBroadcomPins;
        private uint _outBoardPin;
        private uint _inBoardPin;
        private int _id;
        private string _name;

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
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnChanged?.Invoke();                 
            } 
        }

        public IDictionary<uint, uint> ValidBoardAndBroadcomPins //key: XX - board number, value: GPIOXX - broadcom number
        {
            get => _validBoardAndBroadcomPins;
            set
            {
                _validBoardAndBroadcomPins = value;
                OnChanged?.Invoke();
            }
        }

        public uint BoardOutPin
        {
            get => _outBoardPin;
            set
            {
                _outBoardPin = value;
                OnChanged?.Invoke();
            } 
        }
        public uint BoardInPin
        {
            get => _inBoardPin;
            set
            {
                _inBoardPin = value;
                OnChanged?.Invoke();
            }
        }

        public uint BroadcomOutPin
        {
            get { return _validBoardAndBroadcomPins.FirstOrDefault(x => x.Key == _outBoardPin).Value; }
            set
            {
                _outBoardPin = _validBoardAndBroadcomPins.FirstOrDefault(x => x.Value == value).Key;
                OnChanged?.Invoke();
            }
        }
        public uint BroadcomInPin
        {
            get { return _validBoardAndBroadcomPins.FirstOrDefault(x => x.Key == _inBoardPin).Value; }
            set
            {
                _inBoardPin = _validBoardAndBroadcomPins.FirstOrDefault(x => x.Value == value).Key;
                OnChanged?.Invoke();
            }
        }
    }
}