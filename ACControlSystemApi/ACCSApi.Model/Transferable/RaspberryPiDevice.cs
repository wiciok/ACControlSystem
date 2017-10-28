using System;
using System.Collections.Generic;
using System.Linq;
using ACCSApi.Model.Interfaces;
using MessagePack;

namespace ACCSApi.Model.Transferable
{
    [Serializable]
    public class RaspberryPiDevice: IACCSSerializable, IRaspberryPiDevice
    {
        public RaspberryPiDevice()
        {
            CodesList = new List<ICode>();
        }

        public RaspberryPiDevice(string name, IDictionary<uint, uint> validPins): base()
        {
            Name = name;
            ValidBoardAndBroadcomPins = validPins;        
        }

        private IDictionary<uint, uint> _validBoardAndBroadcomPins;
        private uint _outBoardPin;
        private uint _inBoardPin;

        public int Id { get; set; }
        public string Name { get; set; }  
        public IList<ICode> CodesList { get; }
    
        public IDictionary<uint, uint> ValidBoardAndBroadcomPins //key: XX - board number, value: GPIOXX - broadcom number
        {
            get => _validBoardAndBroadcomPins;
            set => _validBoardAndBroadcomPins = value;
        }

        public uint BoardOutPin
        {
            get => _outBoardPin;
            set => _outBoardPin = value;
        }
        public uint BoardInPin
        {
            get => _inBoardPin;
            set => _inBoardPin = value;
        }

        public uint BroadcomOutPin
        {
            get { return _validBoardAndBroadcomPins.FirstOrDefault(x => x.Key == _outBoardPin).Value; }
            set { _outBoardPin = _validBoardAndBroadcomPins.FirstOrDefault(x => x.Value == value).Key; }
        }
        public uint BroadcomInPin
        {
            get { return _validBoardAndBroadcomPins.FirstOrDefault(x => x.Key == _inBoardPin).Value; }
            set { _inBoardPin = _validBoardAndBroadcomPins.FirstOrDefault(x => x.Value == value).Key; }
        }    
    }
}