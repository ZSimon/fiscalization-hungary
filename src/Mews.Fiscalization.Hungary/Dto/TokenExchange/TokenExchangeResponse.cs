using System;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Hungary.Dto
{
    [XmlRoot(ElementName = "TokenExchangeResponse", Namespace = "http://schemas.nav.gov.hu/OSA/2.0/api")]
    public class TokenExchangeResponse : BasicResponse
    {
        [XmlElement(ElementName = "encodedExchangeToken", Namespace = "http://schemas.nav.gov.hu/OSA/2.0/api")]
        public string EncodedExchangeToken { get; set; }

        [XmlElement(ElementName = "tokenValidityFrom", Namespace = "http://schemas.nav.gov.hu/OSA/2.0/api")]
        public DateTime TokenValidityFrom { get; set; }

        [XmlElement(ElementName = "tokenValidityTo", Namespace = "http://schemas.nav.gov.hu/OSA/2.0/api")]
        public DateTime TokenValidityTo { get; set; }
    }
}
