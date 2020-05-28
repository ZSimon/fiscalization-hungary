using System.Xml.Serialization;

namespace Mews.Fiscalization.Hungary.Dto
{
    internal sealed class Header
    {
        public Header()
        {
            RequestVersion = "2.0";
            HeaderVersion = "1.0";
        }

        [XmlElement("requestId")]
        public string RequestId { get; set; }

        [XmlElement("timeStamp")]
        public string TimeStamp { get; set; }

        [XmlElement("requestVersion")]
        public string RequestVersion { get; set; }

        [XmlElement("headerVersion")]
        public string HeaderVersion { get; set; }
    }
}