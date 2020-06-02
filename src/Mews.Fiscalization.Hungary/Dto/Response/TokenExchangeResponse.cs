using System;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Hungary.Dto.Response
{
    public sealed class TokenExchangeResponse
    {
        [XmlElement("encodedExchangeToken")]
        public string EncodedToken { get; set; }

        [XmlElement("tokenValidityFrom")]
        public DateTime ValidFrom { get; set; }

        [XmlElement("tokenValidityTo")]
        public DateTime ValidTo { get; set; }
    }
}