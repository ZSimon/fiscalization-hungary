using System;

namespace Mews.Fiscalization.Hungary.Models
{
    public sealed class CorrectionalItem : InvoiceItem
    {
        public CorrectionalItem(
            int itemReference,
            DateTime consumptionDate,
            ItemAmounts totalAmounts,
            ItemAmounts unitAmounts,
            MeasurementUnit measurementUnit,
            Description description,
            int quantity,
            ExchangeRate exchangeRate = null,
            bool isDeposit = false)
            : base(consumptionDate, totalAmounts, unitAmounts, measurementUnit, description, quantity, exchangeRate, isDeposit)
        {
            ItemReference = itemReference;
        }

        public int ItemReference { get; }
    }
}