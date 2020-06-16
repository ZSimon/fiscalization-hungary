using Mews.Fiscalization.Hungary.Utils;
using System.Collections.Generic;
using System.Linq;

namespace Mews.Fiscalization.Hungary.Models
{
    public sealed class InvoiceValidation
    {
        public InvoiceValidation(string message, ValidationResultCode resultCode)
        {
            Message = message;
            ResultCode = resultCode;
        }

        public string Message { get; }

        public ValidationResultCode ResultCode { get; }

        internal static IEnumerable<InvoiceValidation> Map(
            IEnumerable<Dto.BusinessValidationResultType> businessValidations,
            IEnumerable<Dto.TechnicalValidationResultType> technicalValidations)
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
