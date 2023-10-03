using DolphinScript.Core.Classes;
using DolphinScript.Core.Events.BaseEvents;
using System;

namespace DolphinScript.Core.Interfaces
{
    public interface IObjectFactory
    {
        T CreateObject<T>() where T : class;
        object CreateObject(Type type);
        ScriptEvent CreateObject(Constants.EventType eventType);
    }
}