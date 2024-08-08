using DynamicMapping.Model;
using System.Text.Json;
using System.Text;
using System.Xml.Serialization;
using System.Xml;

namespace DynamicMapping.Utilities
{
    public class MapHandler
    {
        public static object Map<T>(string sourceType,  string targetType, string context)
        {
            if(sourceType == "XML" && targetType == "JSON")
            {
                return XmlToJson<T>(context);
            }
            else if(sourceType == "JSON" && targetType == "XML")
            {
                return JsonToXml<T>(context);
            }

            //To test the error handler
            throw new NotImplementedException();
        }
        public static string JsonToXml<T>(string json)
        {
            var obj = JsonSerializer.Deserialize<T>(json)!;

            return ObjectToXml(obj);
        }

        static string ObjectToXml<T>(T obj)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));

            var sb = new StringBuilder();
            using var xmlWriter = XmlWriter.Create(sb);

            var ns = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            xmlSerializer.Serialize(xmlWriter, obj, ns);

            return sb.ToString();
        }

        public static string XmlToJson<T>(string xml)
        {
            var obj = XmlToObject<T>(xml);

            return JsonSerializer.Serialize(obj);
        }

        static T XmlToObject<T>(string xml)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));

            using var stringReader = new StringReader(xml);

            return (T)xmlSerializer.Deserialize(stringReader)!;
        }
    }
}
