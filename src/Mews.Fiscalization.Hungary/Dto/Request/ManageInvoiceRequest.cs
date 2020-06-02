using System.Xml.Serialization;

namespace Mews.Fiscalization.Hungary.Dto
{
    public enum InvoiceOperationType
    {
        Create
    }

    public sealed class ManageInvoiceRequest
    {
        [XmlElement("exchangeToken")]
        public string ExchangeToken { get; set; }
    }

    public sealed class InvoiceOperations
    {
        [XmlElement("compressedContent")]
        public bool CompressedContent { get; set; }
    }

    public sealed class InvoiceOperation
    {
        [XmlElement("invoiceOperation")]
        public InvoiceOperationType Type { get; set; }
    }
}