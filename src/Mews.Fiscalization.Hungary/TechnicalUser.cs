namespace Mews.Fiscalization.Hungary
{
    public sealed class TechnicalUser
    {
        public TechnicalUser(string login, string password, string xmlSigningKey, string taxNumber)
        {
            Login = login;
            PasswordHash = Sha512.GetHash(password);
            XmlSigningKey = xmlSigningKey;
            TaxNumber = taxNumber;
        }

        public string Login { get; }

        public string PasswordHash { get; }

        public string XmlSigningKey { get; }

        public string TaxNumber { get; }
    }
}