namespace Mews.Fiscalization.Hungary
{
    public sealed class TechnicalUser
    {
        public TechnicalUser(string login, string password, string taxNumber)
        {
            Login = login;
            PasswordHash = Sha512.GetStringHash(password);
            TaxNumber = taxNumber;
        }

        public string Login { get; }

        public string PasswordHash { get; }

        public string TaxNumber { get; }
    }
}