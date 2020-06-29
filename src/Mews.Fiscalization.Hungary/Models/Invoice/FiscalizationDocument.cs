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
            ExchangeRate exchangeRate,
            IEnumerable<TaxSummaryItem> taxSummary)
        {
            Number = number;
            IssueDate = issueDate;
            SupplierInfo = supplierInfo;
            CustomerInfo = customerInfo;
            CurrencyCode = currencyCode;
            ExchangeRate = exchangeRate;
            TaxSummary = taxSummary.AsList();
        }

        public InvoiceNumber Number { get; }

        public DateTime IssueDate { get; }

        public SupplierInfo SupplierInfo { get; }

        public CustomerInfo CustomerInfo { get; }

        public CurrencyCode CurrencyCode { get; }

        public ExchangeRate ExchangeRate { get; }

        public List<TaxSummaryItem> TaxSummary { get; }

        protected static ExchangeRate GetExchangeRate(IEnumerable<InvoiceItem> items)
        {
            var totalGrossHuf = items.Sum(i => Math.Abs(i.TotalAmounts.AmountHUF.Gross.Value));
            var totalGross = items.Sum(i => Math.Abs(i.TotalAmounts.Amount.Gross.Value));
            if (totalGross != 0)
            {
                return ExchangeRate.Rounded(totalGrossHuf / totalGross);
            }

            return new ExchangeRate(1);
        }

        protected static List<TaxSummaryItem> GetTaxSummary(IEnumerable<InvoiceItem> items)
        {
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