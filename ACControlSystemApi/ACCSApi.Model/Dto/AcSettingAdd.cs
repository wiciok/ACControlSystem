using Newtonsoft.Json.Linq;

namespace ACCSApi.Model.Dto
{
    public class AcSettingAdd
    {
        public bool IsTurnOff { get; set; }
        public JObject Settings { get; set; }
    }
}
