using DolphinScript.Core.Events.BaseEvents;
using System;

namespace DolphinScript.Models
{
    public class ScriptProgressChangedEventArgs : EventArgs
    {
        public ScriptProgressChangedEventArgs(ScriptEvent scriptEvent, string currentAction)
        {
            ScriptEvent = scriptEvent;
            CurrentAction = currentAction;
        }

        public ScriptEvent ScriptEvent { get; }
        public string CurrentAction { get; }
    }
}