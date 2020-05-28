using System.Xml;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Hungary
{
    public static class XmlManipulator
    {
        public static XmlDocument Serialize<T>(T value, XmlSerializerNamespaces namespaces = null)
            where T : class
        {
            var xmlDocument = new XmlDocument();
            var navigator = xmlDocument.CreateNavigator();
            using (var writer = navigator.AppendChild())
            {
                var xmlSerializer = new XmlSerializer(typeof(T));
                xmlSerializer.Serialize(writer, value, namespaces);
            }
            return xmlDocument;
        }
    }
}