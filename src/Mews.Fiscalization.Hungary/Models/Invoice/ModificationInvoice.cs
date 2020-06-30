using System;

namespace Mews.Fiscalization.Hungary.Models
{
    public sealed class ModificationInvoice : Invoice
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

        public ModificationInvoice(
            InvoiceNumber number,
            int modificationIndex,
            int itemIndexOffset,
            InvoiceNumber originalDocumentNumber,
            DateTime issueDate,
            DateTime paymentDate,
            SupplierInfo supplierInfo,
            CustomerInfo customerInfo,
            CurrencyCode currencyCode,
            ISequentialEnumerable<InvoiceItem> items,
            bool isSelfBilling = false,
            bool isCashAccounting = false)
            : base(number, issueDate, paymentDate, supplierInfo, customerInfo, currencyCode, items, isSelfBilling, isCashAccounting)
        {
            OriginalDocumentNumber = originalDocumentNumber;
            ModificationIndex = modificationIndex;
            ItemIndexOffset = itemIndexOffset;
        }
    }
}