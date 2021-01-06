using FuncSharp;
using Mews.Fiscalization.Core.Model;
using System.Text.RegularExpressions;

namespace Mews.Fiscalization.Hungary.Models
{
    public sealed class City
    {
        private City(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static ITry<City, INonEmptyEnumerable<Error>> Create(string value)
        {
            return StringValidations.LengthInRange(value, 1, 255).FlatMap(v =>
            {
                var validCity = StringValidations.RegexMatch(v, new Regex(".*[^\\s].*"));
                return validCity.Map(c => new City(c));
            });
        }
    }
}
