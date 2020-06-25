namespace Mews.Fiscalization.Hungary.Models.Types
{
    public sealed class EncryptionKey : ValidatedString
    {
        private static readonly string regexValidation = "^[0-9A-Za-z]{16}$";

        public EncryptionKey(string value)
            : base(value, 16, 16, regexValidation)
        {
        }

        public static bool IsValid(string value)
        {
            return IsValid(value, 16, 16, regexValidation);
        }
    }
}
