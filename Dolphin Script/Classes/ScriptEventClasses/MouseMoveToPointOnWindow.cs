using System;

using static DolphinScript.Lib.Backend.Common;
using static DolphinScript.Lib.Backend.WinAPI;
using static DolphinScript.Lib.Backend.PointReturns;
using static DolphinScript.Lib.Backend.WindowControl;

using static DolphinScript.Lib.ScriptEventClasses.MouseMove;

namespace DolphinScript.Lib.ScriptEventClasses
{
    [Serializable]
    class MouseMoveToPointOnWindow : ScriptEvent
    {
        /// <summary>
        /// main overriden method used to perform this script event
        /// </summary>
        public override void DoEvent()
        {
            Status = $"Move mouse to {CoordsToMoveTo.ToString()} on window: {WindowTitle}.";

            BringEventWindowToFront(this);

            POINT NewClickPoint = new POINT(
                GetWindowPosition(WindowToClickHandle).Left + CoordsToMoveTo.X,
                GetWindowPosition(WindowToClickHandle).Top + CoordsToMoveTo.Y);

            MoveMouse(NewClickPoint);
        }

        public override string GetEventListBoxString()
        {
            if (GroupID == -1)
                return "Move mouse to Point X: " + CoordsToMoveTo.X + " Y: " + CoordsToMoveTo.Y + " on " + WindowToClickTitle + " window.";
            else
                return "[Group " + GroupID + " Repeat x" + NumberOfCycles + "] Move mouse to random point in area " + ClickArea.PrintArea() + " on " + WindowToClickTitle + " window.";
        }
    }
}
