using System;

namespace Mews.Fiscalization.Hungary.Models
{
    public sealed class TokenExchange
    {
        internal TokenExchange(string value, DateTime validFrom, DateTime validTo)
        {
            Value = value;
            ValidFrom = validFrom;
            ValidTo = validTo;
        }

        public string Value { get; }

        public DateTime ValidFrom { get; }

        public DateTime ValidTo { get; }

        internal static TokenExchange Map(Dto.TokenExchangeResponse response)
        {
            return new TokenExchange(
                value: response.EncodedExchangeToken,
                validFrom: response.TokenValidityFrom,
                validTo: response.TokenValidityTo
            );
        }
    }
}
