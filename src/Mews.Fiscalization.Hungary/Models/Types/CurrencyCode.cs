using FuncSharp;
using Mews.Fiscalization.Core.Model;
using System;
using System.Text.RegularExpressions;

namespace Mews.Fiscalization.Hungary.Models
{
    public sealed class CurrencyCode
    {
        private CurrencyCode(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public override bool Equals(object other)
        {
            return other is CurrencyCode otherCurrencyCode && otherCurrencyCode.Value == Value;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static ITry<CurrencyCode, Error> Create(string value)
        {
            return StringValidations.NonEmptyNorWhitespace(value).FlatMap(v =>
            {
                var validCurrencyCode = StringValidations.RegexMatch(value, new Regex("[A-Z]{3}"));
                return validCurrencyCode.Map(c => new CurrencyCode(c));
            });
        }

        public static CurrencyCode CreateUnsafe(string value)
        {
            return Create(value).Get(error => new ArgumentException(error.Message));
        }
    }
}
