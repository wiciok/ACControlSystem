using ACCSApi.Model.Interfaces;
using Newtonsoft.Json.Linq;

namespace ACCSApi.Model.Transferable
{
    public class ACSetting : IACSetting
    {
        public bool IsTurnOff { get; set; } = false;
        public JObject Settings { get; set; }
        public ICode Code { get; set; }
    }
}
