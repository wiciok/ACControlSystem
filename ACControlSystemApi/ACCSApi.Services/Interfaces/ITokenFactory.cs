using ACControlSystemApi.Utils.Tokens.Interfaces;

namespace ACControlSystemApi.Utils.Tokens
{
    public interface ITokenFactory
    {
        IToken GenerateToken();
    }
}
