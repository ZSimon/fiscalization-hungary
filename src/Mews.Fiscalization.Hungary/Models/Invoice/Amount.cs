using System.Collections.Generic;
using System.Linq;

namespace Mews.Fiscalization.Hungary.Models
{
    public sealed class Amount
    {
        public Amount(decimal net, decimal gross, decimal tax)
        {
            Net = net;
            Gross = gross;
            Tax = tax;
        }

        public decimal Net { get; }

        public decimal Gross { get; }

        public decimal Tax { get; }

        internal static Amount Sum(IEnumerable<Amount> amounts)
        {
            return new Amount(
                net: amounts.Sum(a => a.Net),
                gross: amounts.Sum(a => a.Gross),
                tax: amounts.Sum(a => a.Tax)
            );
        }
    }
}
