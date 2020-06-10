using System.Xml.Serialization;

namespace Mews.Fiscalization.Hungary.Dto
{
    public sealed class QueryTaxpayerRequest : Request
    {
        [XmlElement("taxNumber")]
        public string TaxNumber { get; set; }
    }
}