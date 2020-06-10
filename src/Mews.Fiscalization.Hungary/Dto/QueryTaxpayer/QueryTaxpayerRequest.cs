using System.Xml.Serialization;

namespace Mews.Fiscalization.Hungary.Dto.QueryTaxpayer
{
    public sealed class QueryTaxpayerRequest : Request
    {
        [XmlElement("taxNumber")]
        public string TaxNumber { get; set; }
    }
}