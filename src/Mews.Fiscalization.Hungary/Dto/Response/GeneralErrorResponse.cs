using System.Xml.Serialization;

namespace Mews.Fiscalization.Hungary.Dto
{
    [XmlRoot(ElementName = "GeneralErrorResponse", Namespace = "http://schemas.nav.gov.hu/OSA/2.0/api")]
    public class GeneralErrorResponse
    {
        [XmlElement(ElementName = "result", Namespace = "http://schemas.nav.gov.hu/OSA/2.0/api")]
        public ErrorResult Result { get; set; }
    }

    [XmlRoot(ElementName = "result", Namespace = "http://schemas.nav.gov.hu/OSA/2.0/api")]
    public class ErrorResult
    {
        [XmlElement(ElementName = "errorCode", Namespace = "http://schemas.nav.gov.hu/OSA/2.0/api")]
        public ErrorCode ErrorCode { get; set; }

        [XmlElement(ElementName = "message", Namespace = "http://schemas.nav.gov.hu/OSA/2.0/api")]
        public string Message { get; set; }
    }
}
