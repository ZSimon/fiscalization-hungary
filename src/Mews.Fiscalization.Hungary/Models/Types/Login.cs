using FuncSharp;
using Mews.Fiscalization.Core.Model;
using System.Text.RegularExpressions;

namespace Mews.Fiscalization.Hungary.Models
{
    public sealed class Login
    {
        private Login(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static ITry<Login, Error> Create(string value)
        {
            return StringValidations.LengthInRange(value, 1, 15).FlatMap(v =>
            {
                var validLogin = StringValidations.RegexMatch(value, new Regex("^[0-9A-Za-z]{15}$"));
                return validLogin.Map(l => new Login(l));
            });
        }
    }
}
