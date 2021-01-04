using FuncSharp;
using Mews.Fiscalization.Core.Model;
using System;

namespace Mews.Fiscalization.Hungary.Models
{
    public sealed class ExchangeRate
    {
        private static readonly int MaxDecimalPlaces = 6;

        private ExchangeRate(decimal value)
        {
            Value = value;
        }

        public decimal Value { get; }

        public static ExchangeRate Rounded(decimal value)
        {
            return CreateUnsafe(Decimal.Round(value, MaxDecimalPlaces));
        }

        public static ITry<ExchangeRate, Error> Create(decimal value)
        {
            return DecimalValidations.InRange(value, 0, 100_000_000, minIsAllowed: false, maxIsAllowed: false).FlatMap(v =>
            {
                var validExchangeRate = DecimalValidations.MaxDecimalPlaces(v, MaxDecimalPlaces);
                return validExchangeRate.Map(r => new ExchangeRate(r));
            });
        }

        internal static ExchangeRate CreateUnsafe(decimal value)
        {
            return Create(value).Get(error => new ArgumentException(error.Message));
        }
    }
}