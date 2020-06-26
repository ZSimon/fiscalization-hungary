using System;
using System.Collections.Generic;
using Mews.Fiscalization.Hungary.Utils;

namespace Mews.Fiscalization.Hungary.Models
{
    public sealed class ExchangeRate
    {
        private static readonly int lowerBound = 0;
        private static readonly int upperBound = 100_000_000;
        private static readonly int maxPrecision  = 6;

        public ExchangeRate(decimal value)
        {
            Check.Precision(value, maxPrecision: maxPrecision);
            Check.InRange(value, lowerBound, upperBound, closed: false);
            Value = value;
        }

        public decimal Value { get; }

        public static ExchangeRate Rounded(decimal value)
        {
            return new ExchangeRate(Decimal.Round(value, maxPrecision));
        }
    }
}