using System.Collections.Generic;
using System.Linq;

namespace Mews.Fiscalization.Hungary.Models
{
    public sealed class Amount
    {
        public Amount(AmountValue net, AmountValue gross, AmountValue tax)
        {
            Net = net;
            Gross = gross;
            Tax = tax;
        }

        public AmountValue Net { get; }

        public AmountValue Gross { get; }

        public AmountValue Tax { get; }

        internal static Amount Sum(IEnumerable<(Amount Amount, int Quantity)> amountsAndQuantities)
        {
            return new Amount(
                net: new AmountValue(amountsAndQuantities.Sum(a => a.Amount.Net.Value * a.Quantity)),
                gross: new AmountValue(amountsAndQuantities.Sum(a => a.Amount.Gross.Value * a.Quantity)),
                tax: new AmountValue(amountsAndQuantities.Sum(a => a.Amount.Tax.Value * a.Quantity))
            );
        }
    }
}
