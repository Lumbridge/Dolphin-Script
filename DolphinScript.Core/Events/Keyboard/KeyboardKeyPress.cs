using System;
using System.Windows.Forms;
using DolphinScript.Core.Classes;
using DolphinScript.Core.Events.BaseEvents;

namespace DolphinScript.Core.Events.Keyboard
{
    [Serializable]
    public class KeyboardKeyPress : ScriptEvent
    {
        public override void Invoke()
        {
            // update the status label on the main form
            //
            ScriptState.Status = "";

            SendKeys.SendWait(KeyboardKeys);
        }

        public override string GetEventListBoxString()
        {
            if (!IsPartOfGroup)
                return "Keypress (Key(s): " + KeyboardKeys + ")";
            return "[Group " + GroupId + " Repeat x" + NumberOfCycles + "] Keypress (Key: " + KeyboardKeys + ").";
        }
    }
}