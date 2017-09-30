namespace ACControlSystemApi.Utils.Tokens.Interfaces
{
    public interface IToken
    {
        string TokenString { get; }
        bool IsExpired { get; }
    }
}
