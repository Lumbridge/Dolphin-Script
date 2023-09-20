using DolphinScript.Core.Events.BaseEvents;
using System.Linq.Expressions;
using System;

namespace DolphinScript.Core.Interfaces
{
    public interface IEventFactory
    {
        T CreateEvent<T>() where T : ScriptEvent;
        object CreateEvent(Type type);
    }
}