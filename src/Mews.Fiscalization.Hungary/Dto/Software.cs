using System.Xml.Serialization;

namespace Mews.Fiscalization.Hungary.Dto
{
    public sealed class Software
    {
        [XmlElement("softwareId")]
        public string Id { get; set; }

        [XmlElement("softwareName")]
        public string Name { get; set; }

        [XmlElement("softwareOperation")]
        public string Operation { get; set; }

        [XmlElement("softwareMainVersion")]
        public string MainVersion { get; set; }

        [XmlElement("softwareDevName")]
        public string DeveloperName { get; set; }

        [XmlElement("softwareDevContact")]
        public string DeveloperContact { get; set; }

        [XmlElement("softwareDevCountryCode")]
        public string DeveloperCountry { get; set; }

        [XmlElement("softwareDevTaxNumber")]
        public string DeveloperTaxNumber { get; set; }
    }
}