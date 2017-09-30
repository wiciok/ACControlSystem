using ACControlSystemApi.Utils.Tokens.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACControlSystemApi.Utils.Tokens
{
    public interface ITokenFactory
    {
        IToken GenerateToken();
    }
}
