using System;

namespace Mews.Fiscalization.Hungary
{
    public sealed class ExchangeToken
    {
        public ExchangeToken(string value, DateTime validFrom, DateTime validTo)
        {
            Value = value;
            ValidFrom = validFrom;
            ValidTo = validTo;
        }

        public string Value { get; }

        public DateTime ValidFrom { get; }

        public DateTime ValidTo { get; }
    }
}