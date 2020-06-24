using Mews.Fiscalization.Hungary.Utils;
using System;

namespace Mews.Fiscalization.Hungary.Models
{
    public class Item
    {
        public Item(
            DateTime consumptionDate,
            ItemAmounts amounts,
            ItemAmounts unitAmounts,
            MeasurementUnit measurementUnit,
            Description description,
            int quantity,
            ExchangeRate exchangeRate = null,
            bool isDeposit = false)
        {
            ConsumptionDate = consumptionDate;
            Amounts = Check.NotNull(amounts, nameof(amounts));
            UnitAmounts = Check.NotNull(unitAmounts, nameof(unitAmounts));
            MeasurementUnit = measurementUnit;
            Description = Check.NotNull(description, nameof(description));
            Quantity = quantity;
            ExchangeRate = exchangeRate; // TODO - check that it is provided if price is not in HUF.
            IsDeposit = isDeposit;
        }

        public DateTime ConsumptionDate { get; }

        public ItemAmounts Amounts { get; }

        public ItemAmounts UnitAmounts { get; }

        public MeasurementUnit MeasurementUnit { get; }

        public Description Description { get; }

        public int Quantity { get; }

        public ExchangeRate ExchangeRate { get; }

        public bool IsDeposit { get; }
    }
}
