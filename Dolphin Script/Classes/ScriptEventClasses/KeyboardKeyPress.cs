using System;
using System.Windows.Forms;
using static DolphinScript.Classes.Backend.Common;

namespace DolphinScript.Classes.ScriptEventClasses
{
    [Serializable]
    public class KeyboardKeyPress : ScriptEvent
    {
        public override void Invoke()
        {
            // update the status label on the main form
            //
            Status = "";

            SendKeys.SendWait(KeyboardKeys);
        }

        public override string GetEventListBoxString()
        {
            if (GroupId == -1)
                return "Keypress (Key(s): " + KeyboardKeys + ")";
            return "[Group " + GroupId + " Repeat x" + NumberOfCycles + "] Keypress (Key: " + KeyboardKeys + ").";
        }
    }
}