using FuncSharp;
using Mews.Fiscalization.Core.Model;
using System.Text.RegularExpressions;

namespace Mews.Fiscalization.Hungary.Models
{
    public sealed class AdditionalAddressDetail
    {
        private AdditionalAddressDetail(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static ITry<AdditionalAddressDetail, Error> Create(string value)
        {
            return StringValidations.LengthInRange(value, 1, 255).FlatMap(v =>
            {
                var validAddressDetail = StringValidations.RegexMatch(value, new Regex(".*[^\\s].*"));
                return validAddressDetail.Map(d => new AdditionalAddressDetail(d));
            });
        }
    }
}
