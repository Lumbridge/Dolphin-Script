using System;

namespace DolphinScript.Core.Interfaces
{
    public interface IObjectFactory
    {
        T CreateObject<T>() where T : class;
        object CreateObject(Type type);
    }
}