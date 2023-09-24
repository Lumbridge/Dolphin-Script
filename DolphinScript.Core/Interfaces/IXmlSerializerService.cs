using System.IO;

namespace DolphinScript.Core.Interfaces
{
    public interface IXmlSerializerService
    {
        T Deserialize<T>(string xml);
        T Deserialize<T>(Stream stream);
        string Serialize<T>(T obj);
        void Serialize<T>(Stream stream, T obj);
    }
}