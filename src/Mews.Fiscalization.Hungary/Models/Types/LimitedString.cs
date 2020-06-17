﻿using System;

namespace Mews.Fiscalization.Hungary.Models
{
    public abstract class LimitedString
    {
        public LimitedString(string value, int minLength, int maxLength)
        {
            if (value.Length > maxLength)
            {
                throw new ArgumentException($"Max length of string is {maxLength}.");
            }

            if (value.Length < minLength)
            {
                throw new ArgumentException($"Min length of string is {minLength}.");
            }

            Value = value;
        }

        public string Value { get; }

        protected static bool IsValid(string value, int minLength, int maxLength)
        {
            return value != null && value.Length <= maxLength && value.Length >= minLength;
        }
    }
}