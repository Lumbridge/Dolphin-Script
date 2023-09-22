using DolphinScript.Core.Interfaces;
using System.IO;
using System.Xml.Serialization;

namespace DolphinScript.Core.Concrete
{
    public class DiskService : IDiskService
    {
        public void SaveObjectToDisk<T>(Stream stream, T obj)
        {
            var serializer = new XmlSerializer(obj.GetType());
            serializer.Serialize(stream, obj);
        }

        public T LoadObjectFromDisk<T>(Stream stream)
        {
            var serializer = new XmlSerializer(typeof(T));
            return (T)serializer.Deserialize(stream);
        }
    }
}