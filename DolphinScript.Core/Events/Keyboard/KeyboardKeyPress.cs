using System;
using System.Windows.Forms;
using DolphinScript.Core.Classes;
using DolphinScript.Core.Events.BaseEvents;

namespace DolphinScript.Core.Events.Keyboard
{
    [Serializable]
    public class KeyboardKeyPress : ScriptEvent
    {
        public override void InvokeScriptEvent()
        {
            ScriptState.Status = $"Pressing key(s): {KeyboardKeys}";
            SendKeys.SendWait(KeyboardKeys);
        }

        public override string GetEventListBoxString()
        {
            return "Keypress (Key(s): " + KeyboardKeys + ")";
        }
    }
}