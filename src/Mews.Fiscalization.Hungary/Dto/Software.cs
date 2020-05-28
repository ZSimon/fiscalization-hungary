using System.Xml.Serialization;

namespace Mews.Fiscalization.Hungary.Dto
{
    internal sealed class Software
    {
        [XmlElement("id")]
        public string Id { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("operation")]
        public string Operation { get; set; }

        [XmlElement("mainVersion")]
        public string MainVersion { get; set; }

        [XmlElement("developerName")]
        public string DeveloperName { get; set; }

        [XmlElement("developerContact")]
        public string DeveloperContact { get; set; }

        [XmlElement("developerCountry")]
        public string DeveloperCountry { get; set; }

        [XmlElement("developerTaxNumber")]
        public string DeveloperTaxNumber { get; set; }
    }
}