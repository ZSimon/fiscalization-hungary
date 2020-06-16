using System;

namespace Mews.Fiscalization.Hungary.Models
{
    public class Item
    {
        public Item(
            ProductCodeCategory productCodeCategory,
            ItemChoiceType productCodeChoiceType,
            DateTime consumptionDate,
            ItemAmounts amounts,
            MeasurementUnit measurementUnit,
            string description,
            string productCode,
            int quantity,
            decimal unitAmount,
            bool isDeposit = false)
        {
            ProductCodeCategory = productCodeCategory;
            ProductCodeChoiceType = productCodeChoiceType;
            ConsumptionDate = consumptionDate;
            Amounts = amounts;
            MeasurementUnit = measurementUnit;
            Description = description;
            ProductCode = productCode;
            Quantity = quantity;
            UnitAmount = unitAmount;
            IsDeposit = isDeposit;
        }

        public ProductCodeCategory ProductCodeCategory { get; }

        public ItemChoiceType ProductCodeChoiceType { get; }

        public DateTime ConsumptionDate { get; }
        
        public ItemAmounts Amounts { get; }

        public MeasurementUnit MeasurementUnit { get; }

        public string Description { get; }

        public string ProductCode { get; }

        public int Quantity { get; }

        public decimal UnitAmount { get; }

        public bool IsDeposit { get; }
    }
}
