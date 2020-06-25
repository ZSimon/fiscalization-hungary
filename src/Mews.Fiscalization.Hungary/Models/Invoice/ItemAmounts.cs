﻿using Mews.Fiscalization.Hungary.Utils;

namespace Mews.Fiscalization.Hungary.Models
{
    public sealed class ItemAmounts
    {
        public ItemAmounts(Amount amount, Amount amountHUF, decimal? taxRatePercentage)
        {
            Amount = Check.NotNull(amount, nameof(amount));
            AmountHUF = Check.NotNull(amountHUF, nameof(amountHUF));
            TaxRatePercentage = taxRatePercentage;

            if (taxRatePercentage.HasValue)
            {
                Check.In(taxRatePercentage.Value, TaxationInfo.PercentageTaxRates, nameof(taxRatePercentage));
            }
        }

        public Amount Amount { get; }

        public Amount AmountHUF { get; }

        public decimal? TaxRatePercentage { get; }
    }
}
