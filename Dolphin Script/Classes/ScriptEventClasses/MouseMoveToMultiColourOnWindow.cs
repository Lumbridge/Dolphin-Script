using System;
using static DolphinScript.Classes.Backend.Common;
using static DolphinScript.Classes.Backend.PointReturns;
using static DolphinScript.Classes.Backend.RandomNumber;
using static DolphinScript.Classes.Backend.WindowControl;
using static DolphinScript.Classes.ScriptEventClasses.MouseMoveToColour;

namespace DolphinScript.Classes.ScriptEventClasses
{
    [Serializable]
    public class MouseMoveToMultiColourOnWindow : ScriptEvent
    {
        /// <summary>
        /// main overriden method used to perform this script event
        /// </summary>
        public override void Invoke()
        {
            // update the status label on the main form
            //
            Status = $"Mouse move to any colour in list of selected colours on window: {WindowToClickTitle}.";

            // bring the window associated with this event to the front
            //
            BringEventWindowToFront(this);

            // don't override original click area or it will cause the mouse position to incrememnt every time this method is called
            //
            var newSearchColour = SearchColours[GetRandomNumber(0, SearchColours.Count - 1)];
            var newClickArea = GetClickAreaPositionOnWindow(WindowToClickHandle, ClickArea);
            
            MoveMouseToColour(newClickArea, newSearchColour);
        }

        /// <summary>
        /// returns a string which is added to the listbox to give information about the event which was added to the event list
        /// </summary>
        /// <returns></returns>
        public override string GetEventListBoxString()
        {
            if (GroupId == -1)
                return "Move mouse to random pixel matching colour " + SearchColours.ToString() + " in area " + ClickArea.PrintArea() + " on " + WindowToClickTitle + " window.";
            return "[Group " + GroupId + " Repeat x" + NumberOfCycles + "] Move mouse to random pixel matching colour " + SearchColour + " in area " + ClickArea.PrintArea() + " on " + WindowToClickTitle + " window.";
        }
    }
}
