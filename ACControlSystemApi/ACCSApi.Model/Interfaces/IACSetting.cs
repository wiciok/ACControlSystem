using System;
using Newtonsoft.Json.Linq;

namespace ACCSApi.Model.Interfaces
{
    public interface IACSetting
    {
        Guid UniqueId { get; }
        bool IsTurnOff { get; }
        JObject Settings { get; }
        ICode Code { get; }
    }
}
