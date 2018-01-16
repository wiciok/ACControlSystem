namespace ACCSApi.Model.Dto
{
    public class AuthData
    {
        public AuthData()
        {
            
        }

        public AuthData(string email, string pass)
        {
            EmailAddress = email;
            PasswordHash = pass;
        }

        public string EmailAddress { get; set; }
        public string PasswordHash { get; set; }
    }
}
