using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Hungary
{
    public static class XmlManipulator
    {
        public static string Serialize<T>(T value)
            where T : class
        {
            var xmlDocument = new XmlDocument();
            var navigator = xmlDocument.CreateNavigator();
            using (var writer = navigator.AppendChild())
            {
                var xmlSerializer = new XmlSerializer(typeof(T), ServiceInfo.XmlNamespace);
                xmlSerializer.Serialize(writer, value);
            }
            return xmlDocument.OuterXml;
        }

        internal static T Deserialize<T>(string input)
            where T : class, new()
        {
            using (var reader = new StringReader(input))
            {
                var xmlSerializer = new XmlSerializer(typeof(T), ServiceInfo.XmlNamespace);
                return xmlSerializer.Deserialize(reader) as T;
            }
        }
    }
}