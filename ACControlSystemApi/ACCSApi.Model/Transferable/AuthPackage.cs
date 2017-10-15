namespace ACCSApi.Model.Transferable
{
    public class AuthPackage
    {
        public AuthPackage(string email, string pass)
        {
            EmailAddress = email;
            Password = pass;
        }

        public string EmailAddress { get; }
        public string Password { get; }
    }
}
