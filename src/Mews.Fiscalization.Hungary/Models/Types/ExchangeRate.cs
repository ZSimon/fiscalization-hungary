using System;
using Mews.Fiscalization.Hungary.Utils;

namespace Mews.Fiscalization.Hungary.Models
{
    public sealed class ExchangeRate
    {
        private static readonly int MaxPrecision = 6;

        public ExchangeRate(decimal value)
        {
            Check.Precision(value, maxPrecision: MaxPrecision);
            Check.InRange(value, 0, 99_999_999);
            Value = value;
        }

        public decimal Value { get; }

        public static ExchangeRate Rounded(decimal value)
        {
            return new ExchangeRate(Decimal.Round(value, MaxPrecision));
        }
    }
}