using FuncSharp;
using Mews.Fiscalization.Core.Model;
using System.Text.RegularExpressions;

namespace Mews.Fiscalization.Hungary.Models
{
    public sealed class Description
    {
        private Description(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static ITry<Description, Error> Create(string value)
        {
            return StringValidations.LengthInRange(value, 1, 512).FlatMap(v =>
            {
                var validDescription = StringValidations.RegexMatch(value, new Regex(".*[^\\s]."));
                return validDescription.Map(d => new Description(d));
            });
        }
    }
}
