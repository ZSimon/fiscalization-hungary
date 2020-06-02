using System.Xml.Serialization;

namespace Mews.Fiscalization.Hungary.Dto
{
    public sealed class Header
    {
        public Header()
        {
            RequestVersion = ServiceInfo.Version;
            HeaderVersion = "1.0";
        }

        [XmlElement("requestId")]
        public string RequestId { get; set; }

        [XmlElement("timestamp")]
        public string TimeStamp { get; set; }

        [XmlElement("requestVersion")]
        public string RequestVersion { get; set; }

        [XmlElement("headerVersion")]
        public string HeaderVersion { get; set; }
    }
}