using System;

using static DolphinScript.Lib.Backend.Common;
using static DolphinScript.Lib.Backend.WindowControl;

namespace DolphinScript.Lib.ScriptEventClasses
{
    [Serializable]
    class MoveWindowToFront : ScriptEvent
    {
        public MoveWindowToFront()
        {
            EventType = Event.Move_Window_To_Front;
        }

        public override void DoEvent()
        {
            SetWindowTopMostIfExists(WindowClass, WindowTitle);
        }

        public override string GetEventListBoxString()
        {
            if (GroupID == -1)
                return "Move " + WindowTitle + " window to front.";
            else
                return "[Group " + GroupID + " Repeat x" + NumberOfCycles + "] Move " + WindowTitle + " window to front.";
        }
    }
}
