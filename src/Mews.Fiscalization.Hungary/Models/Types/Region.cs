using FuncSharp;
using Mews.Fiscalization.Core.Model;
using System.Text.RegularExpressions;

namespace Mews.Fiscalization.Hungary.Models
{
    public sealed class Region
    {
        private Region(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static ITry<Region, Error> Create(string value)
        {
            return StringValidations.LengthInRange(value, 0, 50).FlatMap(v =>
            {
                var validRegion = StringValidations.RegexMatch(value, new Regex(".*[^\\s]."));
                return validRegion.Map(r => new Region(r));
            });
        }
    }
}
