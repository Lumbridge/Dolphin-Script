using System;
using DolphinScript.Core.Classes;
using DolphinScript.Core.Events.BaseEvents;

namespace DolphinScript.Core.Events.Keyboard
{
    [Serializable]
    public class KeyboardReleaseKey : ScriptEvent
    {
        public override void InvokeScriptEvent()
        {
            ScriptState.Status = "";
            throw new NotImplementedException();
        }

        public override string GetEventListBoxString()
        {
            throw new NotImplementedException();
        }
    }
}
