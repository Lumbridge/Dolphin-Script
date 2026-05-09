using DolphinScript.Core.Events.BaseEvents;
using DolphinScript.Core.Interfaces;
using DolphinScript.Interfaces;
using DolphinScript.Models;
using System;

namespace DolphinScript.Services
{
    public class ScriptProgressReporter : IScriptProgressReporter
    {
        private readonly IScriptState _scriptState;

        public ScriptProgressReporter(IScriptState scriptState)
        {
            _scriptState = scriptState;
        }

        public event EventHandler<ScriptProgressChangedEventArgs> CurrentEventChanged;

        public bool ReportCurrentEvent(ScriptEvent scriptEvent)
        {
            if (!_scriptState.IsRunning)
            {
                Clear();
                return true;
            }

            CurrentEventChanged?.Invoke(this, new ScriptProgressChangedEventArgs(scriptEvent, _scriptState.CurrentAction));
            return false;
        }

        public void Clear()
        {
            CurrentEventChanged?.Invoke(this, new ScriptProgressChangedEventArgs(null, string.Empty));
        }
    }
}