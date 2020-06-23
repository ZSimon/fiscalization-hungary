namespace Mews.Fiscalization.Hungary.Models
{
    public sealed class CountryCode : ValidatedString
    {
        private static readonly string regexValidation = "[A-Z]{2}";

        public CountryCode(string value)
            : base(value, 2, 2, regexValidation)
        {
        }

        public static bool IsValid(string value)
        {
            return IsValid(value, 2, 2, regexValidation);
        }
    }
}
