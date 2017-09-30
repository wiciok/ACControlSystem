using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACControlSystemApi.Utils.Tokens.Interfaces
{
    public interface IToken
    {
        string TokenString { get; }
        bool IsExpired { get; }
    }
}
