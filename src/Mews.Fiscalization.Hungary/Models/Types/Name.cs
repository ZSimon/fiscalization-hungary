using FuncSharp;
using Mews.Fiscalization.Core.Model;
using System.Text.RegularExpressions;

namespace Mews.Fiscalization.Hungary.Models
{
    public sealed class Name
    {
        private Name(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static ITry<Name, INonEmptyEnumerable<Error>> Create(string value)
        {
            return StringValidations.LengthInRange(value, 1, 512).FlatMap(v =>
            {
                var validName = StringValidations.RegexMatch(v, new Regex(".*[^\\s]."));
                return validName.Map(n => new Name(n));
            });
        }
    }
}
