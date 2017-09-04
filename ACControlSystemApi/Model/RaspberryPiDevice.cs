using System.Collections.Generic;
using System.Linq;
using ACControlSystemApi.Model;

public class RaspberryPiDevice
{
    public RaspberryPiDevice(string name, Dictionary<uint, uint> validPins)
    {
        Name = name;
        _validBroadcomGpioPins = validPins;
        CodesList = new List<ICode>();
    }

    private Dictionary<uint, uint> _validBroadcomGpioPins; //key - broadcom, value - gpio
    private uint _outBroadcomPin;
    private uint _inBroadcomPin;

    public string Name { get; }

    //todo: change it later - if i'm gonna decrypt this codes
    public List<ICode> CodesList { get; }

    public uint BroadcomOutPin
    {
        get { return _outBroadcomPin; }
        set { _outBroadcomPin = value; }
    }
    public uint BroadcomInPin
    {
        get { return _inBroadcomPin; }
        set { _inBroadcomPin = value; }
    }

    public uint GpioOutPin
    {
        get { return _validBroadcomGpioPins.FirstOrDefault(x => x.Key == _outBroadcomPin).Value; }
        set { _outBroadcomPin = _validBroadcomGpioPins.FirstOrDefault(x => x.Value == _outBroadcomPin).Key; }
    }
    public uint GpioInPin
    {
        get { return _validBroadcomGpioPins.FirstOrDefault(x => x.Value == _inBroadcomPin).Key; }
        set { _inBroadcomPin = _validBroadcomGpioPins.FirstOrDefault(x => x.Value == _inBroadcomPin).Key; }
    }
}