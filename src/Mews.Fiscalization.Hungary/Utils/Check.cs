using System;
using System.Collections.Generic;
using System.Linq;

namespace Mews.Fiscalization.Hungary.Utils
{
    internal static class Check
    {
        public static T NotNull<T>(T value, string name)
            where T : class
        {
            return value ?? throw new ArgumentNullException(name);
        }

        public static void Digits(decimal value, int maxdigitCount)
        {
            if (!value.HasFewerDigitsThan(maxdigitCount))
            {
                throw new ArgumentOutOfRangeException($"Value cannot have more than {maxdigitCount} digits.");
            }
        }

        public static void Precision(decimal value, int maxPrecision)
        {
            if (!value.PrecisionSmallerThanOrEqualTo(maxPrecision))
            {
                throw new ArgumentOutOfRangeException($"Precision cannot be higher than {maxPrecision}.");
            }
        }

        public static void InRange(decimal value, decimal? min = null, decimal? max = null, bool closed = true)
        {
            if (value < min || (!closed && min == value))
            {
                var hint = closed ? "" : " higher than";
                throw new ArgumentException($"Min allowed value is{hint} {min}.");
            }
            if (value > max || (!closed && max == value))
            {
                var hint = closed ? "" : " lower than";
                throw new ArgumentException($"Max allowed value is{hint} {max}.");
            }
        }

        public static void LengthInRange(string value, int? minLength = null, int? maxLength = null)
        {
            if (minLength != null)
            {
                MinLength(value, minLength.Value);
            }
            if (maxLength != null)
            {
                MaxLength(value, maxLength.Value);
            }
        }

        public static void In<T>(T value, IEnumerable<T> values, string name)
        {
            if (!values.Contains(value))
            {
                throw new ArgumentOutOfRangeException($"Value ({value}) is not allowed.", name);
            }
        }

        public static string Regex(string value, string regex)
        {
            return value.MatchesRegex(regex)
                ? value
                : throw new ArgumentException($"Value doesn't match regular expression '{regex}'.");
        }

        private static void MinLength(string value, int minLength)
        {
            if (value.Length < minLength)
            {
                throw new ArgumentException($"Min length of string is {minLength}.");
            }
        }

        private static void MaxLength(string value, int maxLength)
        {
            if (value.Length > maxLength)
            {
                throw new ArgumentException($"Max length of string is {maxLength}.");
            }
        }
    }
}
