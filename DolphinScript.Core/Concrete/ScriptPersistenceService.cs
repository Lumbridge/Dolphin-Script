using DolphinScript.Core.Events.BaseEvents;
using DolphinScript.Core.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using AutoMapper;

namespace DolphinScript.Core.Concrete
{
    public class ScriptPersistenceService : IScriptPersistenceService
    {
        private readonly IEventFactory _eventFactory;

        public ScriptPersistenceService(IEventFactory eventFactory)
        {
            _eventFactory = eventFactory;
        }

        public List<ScriptEvent> LoadScriptEvents(Stream stream)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<ScriptEvent, ScriptEvent>());
            var mapper = config.CreateMapper();

            var serializer = new XmlSerializer(typeof(List<ScriptEvent>));
            var loadedEvents = (List<ScriptEvent>)serializer.Deserialize(stream);
            var events = new List<ScriptEvent>();
            
            foreach (var loadedEvent in loadedEvents)
            {
                var ev = (ScriptEvent)_eventFactory.CreateEvent(loadedEvent.GetType());
                mapper.Map(loadedEvent, ev);
                events.Add(ev);
            }

            return events;
        }
    }
}