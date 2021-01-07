using FuncSharp;
using Mews.Fiscalization.Core.Model;
using System.Text.RegularExpressions;

namespace Mews.Fiscalization.Hungary.Models
{
    public sealed class InvoiceNumber
    {
        private InvoiceNumber(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static ITry<InvoiceNumber, INonEmptyEnumerable<Error>> Create(string value)
        {
            return StringValidations.LengthInRange(value, 1, 50).FlatMap(v =>
            {
                var validInvoiceNumber = StringValidations.RegexMatch(v, new Regex(".*[^\\s].*"));
                return validInvoiceNumber.Map(n => new InvoiceNumber(n));
            });
        }
    }
}
