using System;

namespace Mews.Fiscalization.Hungary.Models
{
    public sealed class ModificationDocument : FiscalizationDocument<CorrectionalItem>
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
            ISequentialEnumerable<CorrectionalItem> items)
            : base(number, issueDate, supplierInfo, customerInfo, currencyCode, items)
        {
            OriginalDocumentNumber = originalDocumentNumber;
            ModificationIndex = modificationIndex;
        }
    }
}