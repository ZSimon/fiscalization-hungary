using System;

namespace Mews.Fiscalization.Hungary.Models
{
    public sealed class ModificationDocument : FiscalizationDocument
    {
        public InvoiceNumber OriginalDocumentNumber { get; }

        /// <summary>
        /// Sequential index of the modification document for one original document.
        /// </summary>
        public int ModificationIndex { get; }

        /// <summary>
        /// Number of items already reported in the original document + all preceding modification documents.
        /// </summary>
        public int ItemIndexOffset { get; }

        public ModificationDocument(
            InvoiceNumber number,
            int modificationIndex,
            int itemIndexOffset,
            InvoiceNumber originalDocumentNumber,
            DateTime issueDate,
            SupplierInfo supplierInfo,
            CustomerInfo customerInfo,
            CurrencyCode currencyCode,
            ISequentialEnumerable<InvoiceItem> items)
            : base(number, issueDate, supplierInfo, customerInfo, currencyCode, items)
        {
            OriginalDocumentNumber = originalDocumentNumber;
            ModificationIndex = modificationIndex;
            ItemIndexOffset = itemIndexOffset;
        }
    }
}