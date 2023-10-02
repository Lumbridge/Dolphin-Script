using System;
using System.Windows.Forms;
using DolphinScript.Core.Classes;
using DolphinScript.Core.Events.BaseEvents;

namespace DolphinScript.Core.Events.Keyboard
{
    [Serializable]
    public class KeyboardKeyPress : ScriptEvent
    {
        public KeyboardKeyPress()
        {
            EventType = Constants.EventType.KeyboardKeyPress;
        }

        public override void Setup()
        {
            ScriptState.CurrentAction = $"Pressing key(s): {KeyboardKeys}";
        }

        public override void Execute()
        {
            SendKeys.SendWait(KeyboardKeys);
        }

        public override string EventDescription()
        {
            return "Keypress (Key(s): " + KeyboardKeys + ")";
        }
    }
}