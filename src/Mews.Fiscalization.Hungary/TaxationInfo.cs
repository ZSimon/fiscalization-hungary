using System.Collections.Generic;

namespace Mews.Fiscalization.Hungary
{
    public static class TaxationInfo
    {
        internal static List<decimal> PercentageTaxRates { get; }

        static TaxationInfo()
        {
            PercentageTaxRates = new List<decimal>
            {
                0.05m,
                0.07m,
                0.12m,
                0.18m,
                0.2m,
                0.25m,
                0.27m,
            };
        }
    }
}