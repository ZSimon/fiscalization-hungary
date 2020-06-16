using System;
using System.Collections.Generic;

namespace Mews.Fiscalization.Hungary.Models
{
    public class Invoice
    {
        public Invoice(
            string number,
            DateTime issueDate,
            Info supplierInfo,
            Info customerInfo,
            IEnumerable<Item> items,
            TaxSummaryItem taxSummary,
            Amount amount,
            Amount amountHUF,
            DateTime deliveryDate,
            DateTime paymentDate,
            string currencyCode,
            bool isSelfBilling = false,
            bool isCashAccounting = false)
        {
            Number = number;
            IssueDate = issueDate;
            SupplierInfo = supplierInfo;
            CustomerInfo = customerInfo;
            Items = items;
            TaxSummary = taxSummary;
            Amount = amount;
            AmountHUF = amountHUF;
            DeliveryDate = deliveryDate;
            PaymentDate = paymentDate;
            CurrencyCode = currencyCode;
            IsSelfBilling = isSelfBilling;
            IsCashAccounting = isCashAccounting;
        }

        public string Number { get; }

        public DateTime IssueDate { get; }

        public Info SupplierInfo { get; }

        public Info CustomerInfo { get; }

        public IEnumerable<Item> Items { get; }

        public TaxSummaryItem TaxSummary { get; }

        public Amount Amount { get; }

        public Amount AmountHUF { get; }

        public DateTime DeliveryDate { get; }

        public DateTime PaymentDate { get; }

        public string CurrencyCode { get; }

        public bool IsSelfBilling { get; }

        public bool IsCashAccounting { get; }
    }
}
