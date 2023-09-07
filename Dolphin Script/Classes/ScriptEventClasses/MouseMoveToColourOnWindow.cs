using System;
using static DolphinScript.Classes.Backend.Common;
using static DolphinScript.Classes.Backend.PointReturns;
using static DolphinScript.Classes.Backend.WindowControl;
using static DolphinScript.Classes.ScriptEventClasses.MouseMoveToColour;

namespace DolphinScript.Classes.ScriptEventClasses
{
    [Serializable]
    public class MouseMoveToColourOnWindow : ScriptEvent
    {
        /// <summary>
        /// main overriden method used to perform this script event
        /// </summary>
        public override void Invoke()
        {
            // update the status label on the main form
            //
            Status = $"Mouse move to colour: {SearchColour} on window: {WindowToClickTitle}.";

            // bring the window associated with this event to the front
            //
            BringEventWindowToFront(this);

            // don't override original click area or it will cause the mouse position to incrememnt every time this method is called
            var newSearchArea = GetClickAreaPositionOnWindow(WindowToClickHandle, ClickArea);

            MoveMouseToColour(newSearchArea, SearchColour);
        }

        /// <summary>
        /// returns a string which is added to the listbox to give information about the event which was added to the event list
        /// </summary>
        /// <returns></returns>
        public override string GetEventListBoxString()
        {
            if (GroupId == -1)
                return "Move mouse to random pixel matching colour " + SearchColour + " in area " + ClickArea.PrintArea() + " on " + GetWindowTitle(WindowToClickHandle) + " window.";
            return "[Group " + GroupId + " Repeat x" + NumberOfCycles + "] Move mouse to random pixel matching colour " + SearchColour + " in area " + ClickArea.PrintArea() + " on " + GetWindowTitle(WindowToClickHandle) + " window.";
        }
    }
}
