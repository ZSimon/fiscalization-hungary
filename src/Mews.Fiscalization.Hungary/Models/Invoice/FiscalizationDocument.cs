using System;
using System.Collections.Generic;
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
    }
}