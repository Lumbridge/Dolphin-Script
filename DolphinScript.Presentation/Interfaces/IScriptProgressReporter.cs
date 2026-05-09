using DolphinScript.Core.Events.BaseEvents;
using DolphinScript.Models;
using System;

namespace DolphinScript.Interfaces
{
    public interface IScriptProgressReporter
    {
        event EventHandler<ScriptProgressChangedEventArgs> CurrentEventChanged;
        bool ReportCurrentEvent(ScriptEvent scriptEvent);
        void Clear();
    }
}