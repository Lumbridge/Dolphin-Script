using System;

using static DolphinScript.Lib.Backend.Common;
using static DolphinScript.Lib.Backend.WinAPI;
using static DolphinScript.Lib.Backend.PointReturns;
using static DolphinScript.Lib.Backend.WindowControl;

using static DolphinScript.Lib.ScriptEventClasses.MouseMove;

namespace DolphinScript.Lib.ScriptEventClasses
{
    [Serializable]
    class MouseMoveToAreaOnWindow : ScriptEvent
    {
        static public void MoveMouseToAreaOnWindow(RECT ClickArea)
        {
            POINT EndPoint = GetRandomPointInArea(ClickArea);

            MoveMouse(EndPoint); // move mouse to picked pixel
        }

        /// <summary>
        /// main overriden method used to perform this script event
        /// </summary>
        public override void DoEvent()
        {
            Status = $"Mouse move to area: {ClickArea.PrintArea()} on window: {WindowToClickTitle}.";

            BringEventWindowToFront(this);

            // don't override original click area or it will cause the mouse position to incrememnt every time this method is called
            RECT NewClickArea = GetClickAreaPositionOnWindow(WindowToClickHandle, ClickArea);

            MoveMouseToAreaOnWindow(NewClickArea);
        }

        public override string GetEventListBoxString()
        {
            if (GroupID == -1)
                return "Move mouse to random point in area " + ClickArea.PrintArea() + " on " + GetWindowTitle(WindowToClickHandle) + " window.";
            else
                return "[Group " + GroupID + " Repeat x" + NumberOfCycles + "] Move mouse to random point in area " + ClickArea.PrintArea() + " on " + GetWindowTitle(WindowToClickHandle) + " window.";
        }
    }
}
