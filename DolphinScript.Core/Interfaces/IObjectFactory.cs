using DolphinScript.Core.Classes;
using DolphinScript.Core.Events.BaseEvents;
using System;
using DolphinScript.Core.Constants;

namespace DolphinScript.Core.Interfaces
{
    public interface IObjectFactory
    {
        T CreateObject<T>() where T : class;
        object CreateObject(Type type);
        ScriptEvent CreateObject(ScriptEventConstants.EventType eventType);
    }
}