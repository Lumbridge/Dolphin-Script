using DolphinScript.Core.Interfaces;
using System.IO;
using System.Xml.Serialization;

namespace DolphinScript.Core.Concrete
{
    public class XmlSerializerService : IXmlSerializerService
    {
        public T Deserialize<T>(string xml)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var stringReader = new StringReader(xml))
            {
                return (T)serializer.Deserialize(stringReader);
            }
        }

        public T Deserialize<T>(Stream stream)
        {
            var serializer = new XmlSerializer(typeof(T));
            return (T)serializer.Deserialize(stream);
        }

        public string Serialize<T>(T obj)
        {
            var serializer = new XmlSerializer(obj.GetType());
            using (var stringWriter = new StringWriter())
            {
                serializer.Serialize(stringWriter, obj);
                return stringWriter.ToString();
            }
        }

        public void Serialize<T>(Stream stream, T obj)
        {
            var serializer = new XmlSerializer(obj.GetType());
            serializer.Serialize(stream, obj);
        }
    }
}