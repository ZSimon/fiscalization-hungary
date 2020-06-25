﻿using Mews.Fiscalization.Hungary.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mews.Fiscalization.Hungary.Models
{
    public class Invoice
    {
        public Invoice(
            InvoiceNumber number,
            DateTime issueDate,
            SupplierInfo supplierInfo,
            CustomerInfo customerInfo,
            IEnumerable<Item> items,
            DateTime paymentDate,
            CurrencyCode currencyCode,
            bool isSelfBilling = false,
            bool isCashAccounting = false)
        {
            Number = Check.NotNull(number, nameof(InvoiceNumber));
            IssueDate = issueDate;
            SupplierInfo = Check.NotNull(supplierInfo, nameof(supplierInfo));
            CustomerInfo = Check.NotNull(customerInfo, nameof(customerInfo));
            Items = Check.NotNull(items, nameof(items)).AsList();
            DeliveryDate = Items.Max(i => i.ConsumptionDate);
            PaymentDate = paymentDate;
            CurrencyCode = Check.NotNull(currencyCode, nameof(currencyCode));
            IsSelfBilling = isSelfBilling;
            IsCashAccounting = isCashAccounting;
            TaxSummary = GetTaxSummary(Items);
            ExchangeRate = GetExchangeRate(Items);
        }

        public InvoiceNumber Number { get; }

        public DateTime IssueDate { get; }

        public SupplierInfo SupplierInfo { get; }

        public CustomerInfo CustomerInfo { get; }

        public List<Item> Items { get; }

        public List<TaxSummaryItem> TaxSummary { get; }

        public DateTime DeliveryDate { get; }

        public DateTime PaymentDate { get; }

        public CurrencyCode CurrencyCode { get; }

        public ExchangeRate ExchangeRate { get; }

        public bool IsSelfBilling { get; }

        public bool IsCashAccounting { get; }

        private List<TaxSummaryItem> GetTaxSummary(IEnumerable<Item> items)
        {
            var itemsByTaxRate = items.GroupBy(i => i.TotalAmounts.TaxRatePercentage);
            var taxSummaryItems = itemsByTaxRate.Select(g => new TaxSummaryItem(
                taxRatePercentage: g.Key,
                amount: Amount.Sum(g.Select(i => i.TotalAmounts.Amount)),
                amountHUF: Amount.Sum(g.Select(i => i.TotalAmounts.AmountHUF))
            ));
            return taxSummaryItems.AsList();
        }

        private ExchangeRate GetExchangeRate(List<Item> items)
        {
            var totalGrossHuf = items.Sum(i => Math.Abs(i.TotalAmounts.AmountHUF.Gross.Value));
            var totalGross = items.Sum(i => Math.Abs(i.TotalAmounts.Amount.Gross.Value));
            if (totalGross != 0)
            {
                return ExchangeRate.Rounded(totalGrossHuf / totalGross);
            }

            return new ExchangeRate(1);
        }
    }
}
