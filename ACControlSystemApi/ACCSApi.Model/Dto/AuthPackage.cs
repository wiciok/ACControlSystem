namespace ACCSApi.Model.Dto
{
    public class AuthPackage
    {
        public AuthPackage()
        {
            
        }

        public AuthPackage(string email, string pass)
        {
            EmailAddress = email;
            Password = pass;
        }

        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}
