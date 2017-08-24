using System;

using static DolphinScript.Lib.Backend.Common;
using static DolphinScript.Lib.Backend.WindowControl;

namespace DolphinScript.Lib.ScriptEventClasses
{
    [Serializable]
    class MoveWindowToFront : ScriptEvent
    {
        /// <summary>
        /// main overriden method used to perform this script event
        /// </summary>
        public override void DoEvent()
        {
            Status = $"Bring window to front: {WindowToClickTitle}.";

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
