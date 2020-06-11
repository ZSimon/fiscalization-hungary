using System;

namespace Mews.Fiscalization.Hungary.Models
{
    public sealed class TokenExchangeData
    {
        public TokenExchangeData(string encodedExchangeToken, DateTime validFrom, DateTime validTo)
        {
            EncodedExchangeToken = encodedExchangeToken;
            ValidFrom = validFrom;
            ValidTo = validTo;
        }

        public string EncodedExchangeToken { get; }

        public DateTime ValidFrom { get; }

        public DateTime ValidTo { get; }

        internal static TokenExchangeData Map(Dto.TokenExchangeResponse response)
        {
            return new TokenExchangeData(
                encodedExchangeToken: response.EncodedExchangeToken,
                validFrom: response.TokenValidityFrom,
                validTo: response.TokenValidityTo
            );
        }
    }
}
