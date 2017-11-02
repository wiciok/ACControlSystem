using System;
using Newtonsoft.Json.Linq;

namespace ACCSApi.Model.Interfaces
{
    public interface IACSetting
    {
        Guid UniqueId { get; }
        bool IsTurnOff { get; set; }
        JObject Settings { get; set; }
        ICode Code { get; set; }
    }
}
