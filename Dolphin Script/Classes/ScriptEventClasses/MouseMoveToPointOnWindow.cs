using System;
using static DolphinScript.Classes.Backend.Common;
using static DolphinScript.Classes.Backend.WinApi;
using static DolphinScript.Classes.Backend.PointReturns;
using static DolphinScript.Classes.Backend.WindowControl;

using static DolphinScript.Classes.ScriptEventClasses.MouseMove;

namespace DolphinScript.Classes.ScriptEventClasses
{
    [Serializable]
    public class MouseMoveToPointOnWindow : ScriptEvent
    {
        /// <summary>
        /// main overriden method used to perform this script event
        /// </summary>
        public override void DoEvent()
        {
            // update the status label on the main form
            //
            Status = $"Move mouse to {CoordsToMoveTo.ToString()} on window: {WindowTitle}.";

            // bring the window associated with this event to the front
            //
            BringEventWindowToFront(this);

            var newClickPoint = new Point(
                GetWindowPosition(WindowToClickHandle).Left + CoordsToMoveTo.X,
                GetWindowPosition(WindowToClickHandle).Top + CoordsToMoveTo.Y);

            MoveMouse(newClickPoint);
        }

        /// <summary>
        /// returns a string which is added to the listbox to give information about the event which was added to the event list
        /// </summary>
        /// <returns></returns>
        public override string GetEventListBoxString()
        {
            if (GroupId == -1)
                return "Move mouse to Point X: " + CoordsToMoveTo.X + " Y: " + CoordsToMoveTo.Y + " on " + WindowToClickTitle + " window.";
            return "[Group " + GroupId + " Repeat x" + NumberOfCycles + "] Move mouse to random point in area " + ClickArea.PrintArea() + " on " + WindowToClickTitle + " window.";
        }
    }
}
