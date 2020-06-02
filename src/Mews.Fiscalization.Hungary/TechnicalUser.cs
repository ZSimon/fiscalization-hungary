namespace Mews.Fiscalization.Hungary
{
    public sealed class TechnicalUser
    {
        public TechnicalUser(string login, string password, string signingKey, string encryptionKey, string taxNumber)
        {
            Login = login;
            PasswordHash = Sha512.GetHash(password);
            SigningKey = signingKey;
            EncryptionKey = encryptionKey;
            TaxNumber = taxNumber;
        }

        public string Login { get; }

        public string PasswordHash { get; }

        public string SigningKey { get; }

        public string EncryptionKey { get; }

        public string TaxNumber { get; }
    }
}