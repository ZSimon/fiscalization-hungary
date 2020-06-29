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
            GetExchangeRate(items.Select(i => i.Item)),
            GetTaxSummary(items.Select(i => i.Item))
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
