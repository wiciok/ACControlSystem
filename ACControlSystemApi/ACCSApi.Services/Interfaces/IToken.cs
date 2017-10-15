namespace ACCSApi.Services.Interfaces
{
    public interface IToken
    {
        string TokenString { get; }
        bool IsExpired { get; }
    }
}
