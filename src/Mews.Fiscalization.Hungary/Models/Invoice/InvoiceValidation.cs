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
    }
}
