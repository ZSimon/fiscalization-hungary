using System;
using System.Collections.Generic;
using Mews.Fiscalization.Hungary.Utils;

namespace Mews.Fiscalization.Hungary.Models
{
    public sealed class ExchangeRate
    {
        private static int MaxDigits { get; }
        private static int MaxPrecision { get; }
        private static int LowerBound { get; }
        private static int UpperBound { get; }

        public ExchangeRate(decimal value)
        {
            Check.Precision(value, maxPrecision: MaxPrecision);
            Check.InRange(value, LowerBound, UpperBound, closed: false);
            Value = value;
        }

        public decimal Value { get; }

        public static ExchangeRate Rounded(decimal value)
        {
            return new ExchangeRate(Decimal.Round(value, MaxPrecision));
        }

        static ExchangeRate()
        {
            MaxDigits = 14;
            MaxPrecision = 6;
            LowerBound = 0;
            UpperBound = (int)Math.Pow(10, MaxDigits - MaxPrecision);
        }
    }
}