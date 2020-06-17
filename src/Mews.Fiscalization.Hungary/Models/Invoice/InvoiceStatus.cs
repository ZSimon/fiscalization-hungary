using System.Collections.Generic;
using System.Linq;

namespace Mews.Fiscalization.Hungary.Models
{
    public sealed class InvoiceStatus
    {
        public InvoiceStatus(InvoiceState status, IEnumerable<InvoiceValidationResult> validations)
        {
            Status = status;
            Validations = validations;
        }

        public InvoiceState Status { get; }

        public IEnumerable<InvoiceValidationResult> Validations { get; }

        internal static InvoiceStatus Map(Dto.QueryTransactionStatusResponse response)
        {
            var result = response.processingResults.processingResult.First();
            return new InvoiceStatus(
                status: (InvoiceState)result.invoiceStatus,
                validations: InvoiceValidationResult.Map(result.businessValidationMessages, result.technicalValidationMessages).ToArray()
            );
        }
    }
}
