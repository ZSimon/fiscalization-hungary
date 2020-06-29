using System;
using System.Collections.Generic;
using System.Linq;

namespace Mews.Fiscalization.Hungary.Models
{
    public sealed class ModificationDocument : FiscalizationDocument
    {
        public InvoiceNumber OriginalDocumentNumber { get; }

        /// <summary>
        /// Sequential index of the modification document for one original document.
        /// </summary>
        public int ModificationIndex { get; }

        public ModificationDocument(
            InvoiceNumber number,
            int modificationIndex,
            InvoiceNumber originalDocumentNumber,
            DateTime issueDate,
            SupplierInfo supplierInfo,
            CustomerInfo customerInfo,
            CurrencyCode currencyCode,
            IEnumerable<IndexedItem<CorrectionalItem>> items)
            : base(number, issueDate, supplierInfo, customerInfo, currencyCode, GetExchangeRate(items.Select(i => i.Item)), GetTaxSummary(items.Select(i => i.Item)))
        {
            OriginalDocumentNumber = originalDocumentNumber;
            ModificationIndex = modificationIndex;
        }
    }
}