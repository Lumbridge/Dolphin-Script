using AutoMapper;
using DolphinScript.Core.Events.BaseEvents;
using DolphinScript.Core.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace DolphinScript.Core.Concrete
{
    public class DiskService : IDiskService
    {
        private readonly IEventFactory _eventFactory;
        private readonly IMapper _mapper;

        public DiskService(IEventFactory eventFactory, IMapper mapper)
        {
            _eventFactory = eventFactory;
            _mapper = mapper;
        }

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