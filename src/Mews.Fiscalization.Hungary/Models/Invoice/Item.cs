using System;

namespace Mews.Fiscalization.Hungary.Models
{
    public class Item
    {
        public Item(
            string number,
            ProductCodeCategory productCodeCategory,
            ItemChoiceType productCodeChoiceType,
            DateTime consumptionDate,
            TaxSummaryItem taxSummary,
            MeasurementUnit measurementUnit,
            string description,
            string productCode,
            int quantity,
            decimal unitAmount,
            bool isDeposit = false)
        {
            Number = number;
            ProductCodeCategory = productCodeCategory;
            ProductCodeChoiceType = productCodeChoiceType;
            ConsumptionDate = consumptionDate;
            TaxSummary = taxSummary;
            MeasurementUnit = measurementUnit;
            Description = description;
            ProductCode = productCode;
            Quantity = quantity;
            UnitAmount = unitAmount;
            IsDeposit = isDeposit;
        }

        public string Number { get; }

        public ProductCodeCategory ProductCodeCategory { get; }

        public ItemChoiceType ProductCodeChoiceType { get; }

        public DateTime ConsumptionDate { get; }
        
        public TaxSummaryItem TaxSummary { get; }

        public MeasurementUnit MeasurementUnit { get; }

        public string Description { get; }

        public string ProductCode { get; }

        public int Quantity { get; }

        public decimal UnitAmount { get; }

        public bool IsDeposit { get; }
    }
}
