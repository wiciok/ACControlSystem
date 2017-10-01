using ACControlSystemApi.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ACCSApi.Model.Interfaces
{
    public interface IRaspberryPiDevice : IACCSSerializable
    {
        string Name { get; set; }
        IList<ICode> CodesList { get; } //todo: change it later - if i'm gonna decrypt this codes

        IDictionary<uint, uint> ValidBoardAndBroadcomPins { get; set; } //key: XX - board number, value: GPIOXX - broadcom number

        uint BoardOutPin { get; set; }
        uint BoardInPin { get; set; }
        uint BroadcomOutPin { get; set; }
        uint BroadcomInPin { get; set; }
    }
}

