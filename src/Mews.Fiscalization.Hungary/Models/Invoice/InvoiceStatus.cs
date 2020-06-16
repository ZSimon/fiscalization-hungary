using Mews.Fiscalization.Hungary.Utils;
using System;
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
                validations: MapValidation(result.businessValidationMessages, result.technicalValidationMessages).ToArray()
            );
        }

        internal static IEnumerable<InvoiceValidation> MapValidation(IEnumerable<Dto.BusinessValidationResultType> businessValidations, IEnumerable<Dto.TechnicalValidationResultType> technicalValidations)
        {
            return Enumerable.Concat(
                businessValidations.NullToEmpty().Select(v => new InvoiceValidation(
                    message: v.message,
                    resultCode: (ValidationResultCode)v.validationResultCode)
                ),
                technicalValidations.NullToEmpty().Select(v => new InvoiceValidation(
                    message: v.message,
                    resultCode: (ValidationResultCode)v.validationResultCode)
                )
            );
        }
    }
}
