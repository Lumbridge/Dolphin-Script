using System;
using System.Windows.Forms;

namespace DolphinScript.Lib.ScriptEventClasses
{
    [Serializable]
    class KeyboardKeyPress : ScriptEvent
    {
        public KeyboardKeyPress()
        {
            EventType = Event.Keyboard_Keypress;
        }

        public override void DoEvent()
        {
            SendKeys.SendWait(KeyboardKeys);
        }

        public override string GetEventListBoxString()
        {
            if (GroupID == -1)
                return "Keypress (Key(s): " + KeyboardKeys + ")";
            else
                return "[Group " + GroupID + " Repeat x" + NumberOfCycles + "] Keypress (Key: " + KeyboardKeys + ").";
        }
    }
}