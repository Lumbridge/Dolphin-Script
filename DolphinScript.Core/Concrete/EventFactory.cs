using System;
using DolphinScript.Core.Events.BaseEvents;
using DolphinScript.Core.Interfaces;
using Unity;

namespace DolphinScript.Core.Concrete
{
    public class EventFactory : IEventFactory
    {
        private readonly IUnityContainer _container;

        public EventFactory(IUnityContainer container)
        {
            _container = container;
        }

        public T CreateEvent<T>() where T : ScriptEvent
        {
            return _container.Resolve<T>();
        }

        public object CreateEvent(Type type)
        {
            return _container.Resolve(type);
        }
    }
}