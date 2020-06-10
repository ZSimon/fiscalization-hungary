using System.Collections.Generic;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Hungary.Dto
{
    public enum InvoiceOperationType
    {
        [XmlEnum("CREATE")]
        Create,
        [XmlEnum("MODIFY")]
        Modify,
        [XmlEnum("STORNO")]
        Storno
    }

    public sealed class ManageInvoiceRequest : Request
    {
        [XmlElement("exchangeToken")]
        public string ExchangeToken { get; set; }
        public InvoiceOperations Operations { get; set; }
    }

    public sealed class InvoiceOperations
    {
        [XmlElement("compressedContent")]
        public bool CompressedContent { get; set; }

        public List<InvoiceOperation> Items { get; set; }
    }

    public sealed class InvoiceOperation
    {
        [XmlElement("index")]
        public int Index { get; set; }

        [XmlElement("invoiceOperation")]
        public InvoiceOperationType Type { get; set; }

        [XmlElement("invoiceData")]
        public string InvoiceDataBase64 { get; set; }
    }
}