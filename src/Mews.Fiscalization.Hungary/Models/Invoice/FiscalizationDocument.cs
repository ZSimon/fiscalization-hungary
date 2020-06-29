using System;
using System.Collections.Generic;
using System.Linq;
using Mews.Fiscalization.Hungary.Utils;

namespace Mews.Fiscalization.Hungary.Models
{
    public abstract class FiscalizationDocument<TItem>
        where TItem : InvoiceItem
    {
        protected FiscalizationDocument(
            InvoiceNumber number,
            DateTime issueDate,
            SupplierInfo supplierInfo,
            CustomerInfo customerInfo,
            CurrencyCode currencyCode,
            IIndexedEnumerable<TItem> items)
        {
            Number = number;
            IssueDate = issueDate;
            SupplierInfo = supplierInfo;
            CustomerInfo = customerInfo;
            CurrencyCode = currencyCode;
            ExchangeRate = GetExchangeRate(items);
            TaxSummary = GetTaxSummary(items);
        }

        public InvoiceNumber Number { get; }

        public DateTime IssueDate { get; }

        public SupplierInfo SupplierInfo { get; }

        public CustomerInfo CustomerInfo { get; }

        public CurrencyCode CurrencyCode { get; }

        public ExchangeRate ExchangeRate { get; }

        public List<TaxSummaryItem> TaxSummary { get; }

        public IIndexedEnumerable<InvoiceItem> Items { get; }

        private ExchangeRate GetExchangeRate(IIndexedEnumerable<InvoiceItem> indexedItems)
        {
            var items = indexedItems.Select(i => i.Item);
            var totalGrossHuf = items.Sum(i => Math.Abs(i.TotalAmounts.AmountHUF.Gross.Value));
            var totalGross = items.Sum(i => Math.Abs(i.TotalAmounts.Amount.Gross.Value));
            if (totalGross != 0)
            {
                return ExchangeRate.Rounded(totalGrossHuf / totalGross);
            }

            return new ExchangeRate(1);
        }

        private List<TaxSummaryItem> GetTaxSummary(IIndexedEnumerable<InvoiceItem> indexedItems)
        {
            var items = indexedItems.Select(i => i.Item);
            var itemsByTaxRate = items.GroupBy(i => i.TotalAmounts.TaxRatePercentage);
            var taxSummaryItems = itemsByTaxRate.Select(g => new TaxSummaryItem(
                taxRatePercentage: g.Key,
                amount: Amount.Sum(g.Select(i => i.TotalAmounts.Amount)),
                amountHUF: Amount.Sum(g.Select(i => i.TotalAmounts.AmountHUF))
            ));
            return taxSummaryItems.AsList();
        }
    }
}