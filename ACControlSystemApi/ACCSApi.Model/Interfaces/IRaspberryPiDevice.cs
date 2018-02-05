using System.Collections.Generic;

namespace ACCSApi.Model.Interfaces
{
    public interface IRaspberryPiDevice : IACCSSerializable
    {
        string Name { get; set; }
        IDictionary<uint, uint> ValidBoardAndBroadcomPins { get; set; } //key: XX - board number, value: GPIOXX - broadcom number

        uint BoardOutPin { get; set; }
        uint BoardInPin { get; set; }
        uint BroadcomOutPin { get; set; }
        uint BroadcomInPin { get; set; }
    }
}

