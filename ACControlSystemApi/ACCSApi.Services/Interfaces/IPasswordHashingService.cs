namespace ACCSApi.Services.Interfaces
{
    public interface IPasswordHashingService
    {
        string CreateHash(string password);
    }
}
