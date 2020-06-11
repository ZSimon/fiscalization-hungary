using System.Xml.Serialization;

namespace Mews.Fiscalization.Hungary.Dto
{
    public class BasicResponse
    {
        [XmlElement(ElementName = "header", Namespace = "http://schemas.nav.gov.hu/OSA/2.0/api")]
        public Header Header { get; set; }

        [XmlElement(ElementName = "software", Namespace = "http://schemas.nav.gov.hu/OSA/2.0/api")]
        public Software Software { get; set; }

        [XmlElement(ElementName = "taxpayerValidity", Namespace = "http://schemas.nav.gov.hu/OSA/2.0/api")]
        public bool IsValidTaxPayer { get; set; }
    }
}
