using Mews.Fiscalization.Hungary.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mews.Fiscalization.Hungary.Models
{
    public sealed class Invoice : FiscalizationDocument
    {
        public Invoice(
            InvoiceNumber number,
            DateTime issueDate,
            SupplierInfo supplierInfo,
            CustomerInfo customerInfo,
            IEnumerable<IndexedItem<InvoiceItem>> items,
            DateTime paymentDate,
            CurrencyCode currencyCode,
            bool isSelfBilling = false,
            bool isCashAccounting = false)
        : base(
            number,
            issueDate,
            supplierInfo,
            customerInfo,
            currencyCode,
            GetExchangeRate(items),
            GetTaxSummary(items)
        )
        {
            Items = Check.NonEmpty(items, nameof(items)).AsList();
            DeliveryDate = Items.Max(i => i.Item.ConsumptionDate);
            PaymentDate = paymentDate;
            IsSelfBilling = isSelfBilling;
            IsCashAccounting = isCashAccounting;

            CheckConsistency(this);
        }

        public List<IndexedItem<InvoiceItem>> Items { get; }

        public DateTime DeliveryDate { get; }

        public DateTime PaymentDate { get; }

        public bool IsSelfBilling { get; }

        public bool IsCashAccounting { get; }

        private static List<TaxSummaryItem> GetTaxSummary(IEnumerable<IndexedItem<InvoiceItem>> input)
        {
            var items = input.Select(i => i.Item);
            var itemsByTaxRate = items.GroupBy(i => i.TotalAmounts.TaxRatePercentage);
            var taxSummaryItems = itemsByTaxRate.Select(g => new TaxSummaryItem(
                taxRatePercentage: g.Key,
                amount: Amount.Sum(g.Select(i => i.TotalAmounts.Amount)),
                amountHUF: Amount.Sum(g.Select(i => i.TotalAmounts.AmountHUF))
            ));
            return taxSummaryItems.AsList();
        }

        private static ExchangeRate GetExchangeRate(IEnumerable<IndexedItem<InvoiceItem>> input)
        {
            var items = input.Select(i => i.Item).AsList();
            var totalGrossHuf = items.Sum(i => Math.Abs(i.TotalAmounts.AmountHUF.Gross.Value));
            var totalGross = items.Sum(i => Math.Abs(i.TotalAmounts.Amount.Gross.Value));
            if (totalGross != 0)
            {
                return ExchangeRate.Rounded(totalGrossHuf / totalGross);
            }

            return new ExchangeRate(1);
        }

        private static void CheckConsistency(Invoice invoice)
        {
            var nonDefaultCurrency = !invoice.CurrencyCode.Equals(TaxationInfo.DefaultCurrencyCode);
            var hasRequiredTaxRates = nonDefaultCurrency.Implies(() => invoice.Items.All(i => i.Item.ExchangeRate != null));
            if (!hasRequiredTaxRates)
            {
                throw new InvalidOperationException("Exchange rate needs to be specified for all items.");
            }
        }
    }
}
