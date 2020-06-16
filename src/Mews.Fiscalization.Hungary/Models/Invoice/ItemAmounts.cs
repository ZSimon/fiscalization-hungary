namespace Mews.Fiscalization.Hungary.Models
{
    public sealed class ItemAmounts
    {
        public ItemAmounts(Amount amount, Amount amountHUF, decimal taxRatePercentage)
        {
            Amount = amount;
            AmountHUF = amountHUF;
            TaxRatePercentage = taxRatePercentage;
        }

        public Amount Amount { get; }
        
        public Amount AmountHUF { get; }

        public decimal TaxRatePercentage { get; }
    }
}
