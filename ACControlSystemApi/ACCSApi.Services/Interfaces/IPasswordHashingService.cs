namespace ACControlSystemApi.Services.Interfaces
{
    public interface IPasswordHashingService
    {
        string CreateHash(string password);
    }
}
