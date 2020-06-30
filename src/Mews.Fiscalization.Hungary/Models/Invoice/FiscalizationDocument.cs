using System;
using System.Collections.Generic;
using System.Linq;
using Mews.Fiscalization.Hungary.Utils;

namespace Mews.Fiscalization.Hungary.Models
{
    public abstract class FiscalizationDocument
    {
        protected FiscalizationDocument(
            InvoiceNumber number,
            DateTime issueDate,
            SupplierInfo supplierInfo,
            CustomerInfo customerInfo,
            CurrencyCode currencyCode,
            ISequentialEnumerable<InvoiceItem> items)
        {
            Number = number;
            IssueDate = issueDate;
            SupplierInfo = supplierInfo;
            CustomerInfo = customerInfo;
            CurrencyCode = currencyCode;
            Items = items;
            DeliveryDate = Items.Max(i => i.Item.ConsumptionDate);
            ExchangeRate = GetExchangeRate(items);
            TaxSummary = GetTaxSummary(items);
        }

        public InvoiceNumber Number { get; }

        public DateTime DeliveryDate { get; }

        public DateTime IssueDate { get; }

        public SupplierInfo SupplierInfo { get; }

        public CustomerInfo CustomerInfo { get; }

        public CurrencyCode CurrencyCode { get; }

        public ExchangeRate ExchangeRate { get; }

        public List<TaxSummaryItem> TaxSummary { get; }

        public ISequentialEnumerable<InvoiceItem> Items { get; }

        private ExchangeRate GetExchangeRate(ISequentialEnumerable<InvoiceItem> indexedItems)
        {
            var totalGrossHuf = indexedItems.Items.Sum(i => Math.Abs(i.TotalAmounts.AmountHUF.Gross.Value));
            var totalGross = indexedItems.Items.Sum(i => Math.Abs(i.TotalAmounts.Amount.Gross.Value));
            if (totalGross != 0)
            {
                return ExchangeRate.Rounded(totalGrossHuf / totalGross);
            }

            return new ExchangeRate(1);
        }

        private List<TaxSummaryItem> GetTaxSummary(ISequentialEnumerable<InvoiceItem> indexedItems)
        {
            var itemsByTaxRate = indexedItems.Items.GroupBy(i => i.TotalAmounts.TaxRatePercentage);
            var taxSummaryItems = itemsByTaxRate.Select(g => new TaxSummaryItem(
                taxRatePercentage: g.Key,
                amount: Amount.Sum(g.Select(i => i.TotalAmounts.Amount)),
                amountHUF: Amount.Sum(g.Select(i => i.TotalAmounts.AmountHUF))
            ));
            return taxSummaryItems.AsList();
        }
    }
}