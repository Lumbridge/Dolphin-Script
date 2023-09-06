using System;
using static DolphinScript.Classes.Backend.Common;
using static DolphinScript.Classes.Backend.WinApi;
using static DolphinScript.Classes.Backend.PointReturns;
using static DolphinScript.Classes.Backend.WindowControl;

using static DolphinScript.Classes.ScriptEventClasses.MouseMove;

namespace DolphinScript.Classes.ScriptEventClasses
{
    /// <summary>
    /// This event moves the mouse cursor to a random point in a given area on a specific window.
    /// </summary>
    [Serializable]
    public class MouseMoveToAreaOnWindow : ScriptEvent
    {
        /// <summary>
        /// this method will move the mouse cursor to a random point in the selected area
        /// </summary>
        /// <param name="clickArea"></param>
        public static void MoveMouseToAreaOnWindow(Rect clickArea)
        {
            // get a random point in the area
            //
            var endPoint = GetRandomPointInArea(clickArea);

            // move the mouse cursor to the random point we got
            //
            MoveMouse(endPoint);
        }

        /// <summary>
        /// main overriden method used to perform this script event
        /// </summary>
        public override void DoEvent()
        {
            // update the status label on the main form
            //
            Status = $"Mouse move to area: {ClickArea.PrintArea()} on window: {WindowToClickTitle}.";

            // bring the window associated with this event to the front
            //
            BringEventWindowToFront(this);

            // don't override original click area or it will cause the mouse position to incrememnt every time this method is called
            var newClickArea = GetClickAreaPositionOnWindow(WindowToClickHandle, ClickArea);

            // call the final mouse move method
            //
            MoveMouseToAreaOnWindow(newClickArea);
        }

        /// <summary>
        /// returns a string which is added to the listbox to give information about the event which was added to the event list
        /// </summary>
        /// <returns></returns>
        public override string GetEventListBoxString()
        {
            if (GroupId == -1)
                return "Move mouse to random point in area " + ClickArea.PrintArea() + " on " + GetWindowTitle(WindowToClickHandle) + " window.";
            return "[Group " + GroupId + " Repeat x" + NumberOfCycles + "] Move mouse to random point in area " + ClickArea.PrintArea() + " on " + GetWindowTitle(WindowToClickHandle) + " window.";
        }
    }
}
