using System.Xml.Serialization;

namespace Mews.Fiscalization.Hungary.Dto
{
    public sealed class User
    {
        [XmlElement("login")]
        public string Login { get; set; }

        [XmlElement("passwordHash")]
        public string PasswordHash { get; set; }

        [XmlElement("taxNumber")]
        public string TaxNumber { get; set; }

        [XmlElement("requestSignature")]
        public string RequestSignature { get; set; }
    }
}