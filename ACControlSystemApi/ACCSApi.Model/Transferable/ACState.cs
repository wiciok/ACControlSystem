using ACCSApi.Model.Interfaces;

namespace ACCSApi.Model.Transferable
{
    public class ACState : IACState
    {
        public bool? IsOn { get; set; }
        public object Settings { get; set; }
    }
}
