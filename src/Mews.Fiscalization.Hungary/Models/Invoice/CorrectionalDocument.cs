using System;
using System.Collections.Generic;

namespace Mews.Fiscalization.Hungary.Models
{
    public sealed class CorrectionalDocument : FiscalizationDocument
    {
        public InvoiceNumber OriginalDocumentNumber { get; }

        public CorrectionalDocument(
            InvoiceNumber number,
            InvoiceNumber originalDocumentNumber,
            DateTime issueDate,
            SupplierInfo supplierInfo,
            CustomerInfo customerInfo,
            CurrencyCode currencyCode,
            IEnumerable<CorrectionalItem> items)
            : base(number, issueDate, supplierInfo, customerInfo, currencyCode, null, GetTaxSummary(items))
        {
            OriginalDocumentNumber = originalDocumentNumber;
        }

        private static List<TaxSummaryItem> GetTaxSummary(IEnumerable<CorrectionalItem> items)
        {
            throw new NotImplementedException();
        }
    }
}