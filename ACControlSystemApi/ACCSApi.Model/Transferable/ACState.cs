using ACCSApi.Model.Interfaces;

namespace ACCSApi.Model.Transferable
{
    class ACState : IACState
    {
        public bool? IsTurnOff { get; set; }
        public IACSetting ACSetting { get; set; }
    }
}
