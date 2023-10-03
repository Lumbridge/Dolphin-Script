using DolphinScript.Core.Classes;
using DolphinScript.Core.Constants;
using DolphinScript.Core.Events.BaseEvents;
using DolphinScript.Core.Interfaces;
using System;
using Unity;

namespace DolphinScript.Core.Concrete
{
    public class ObjectFactory : IObjectFactory
    {
        private readonly IUnityContainer _container;

        public ObjectFactory(IUnityContainer container)
        {
            _container = container;
        }

        public T CreateObject<T>() where T : class
        {
            return _container.Resolve<T>();
        }

        public object CreateObject(Type type)
        {
            return _container.Resolve(type);
        }

        public ScriptEvent CreateObject(ScriptEventConstants.EventType eventType)
        {
            var type = ScriptEventConstants.EventTypeDictionary[eventType];
            return (ScriptEvent)CreateObject(type);
        }
    }
}