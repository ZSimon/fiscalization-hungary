using System;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Hungary.Dto
{
    [Serializable]
    public abstract class Request
    {
        [XmlElement("header")]
        public Header Header { get; set; }

        [XmlElement("user")]
        public User User { get; set; }

        [XmlElement("software")]
        public Software Software { get; set; }
    }
}