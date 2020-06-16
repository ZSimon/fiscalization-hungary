using System.Collections.Generic;
using System.Linq;

namespace Mews.Fiscalization.Hungary.Models
{
    public sealed class InvoiceStatus
    {
        public InvoiceStatus(InvoiceStatusType status, IEnumerable<InvoiceValidation> validations)
        {
            Status = status;
            Validations = validations;
        }

        public InvoiceStatusType Status { get; }

        public IEnumerable<InvoiceValidation> Validations { get; }

        internal static InvoiceStatus Map(Dto.QueryTransactionStatusResponse response)
        {
            var result = response.processingResults.processingResult.First();
            return new InvoiceStatus(
                status: (InvoiceStatusType)result.invoiceStatus,
                validations: InvoiceValidation.Map(result.businessValidationMessages, result.technicalValidationMessages).ToArray()
            );
        }
    }
}
