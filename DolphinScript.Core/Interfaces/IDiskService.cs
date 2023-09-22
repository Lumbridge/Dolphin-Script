using System.IO;

namespace DolphinScript.Core.Interfaces
{
    public interface IDiskService
    {
        void SaveObjectToDisk<T>(Stream stream, T obj);
        T LoadObjectFromDisk<T>(Stream stream);
    }
}