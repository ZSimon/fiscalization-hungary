using System.Xml.Serialization;

namespace Mews.Fiscalization.Hungary.Dto
{
    internal abstract class Request
    {
        [XmlElement("header")]
        public Header Header { get; set; }

        [XmlElement("user")]
        public User User { get; set; }

        [XmlElement("software")]
        public Software Software { get; set; }
    }

    internal sealed class QueryTaxpayerRequest : Request
    {
        [XmlElement("taxNumber")]
        public string TaxNumber { get; set; }
    }
}