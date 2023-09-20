using System;
using DolphinScript.Core.Classes;
using DolphinScript.Core.WindowsApi;
using static DolphinScript.Event.Mouse.MouseMove;

namespace DolphinScript.Event.Mouse
{
    [Serializable]
    public class MouseMoveToColour : ScriptEvent
    {
        /// <summary>
        /// main overriden method used to perform this script event
        /// </summary>
        public override void Invoke()
        {
            // update the status label on the main form
            //
            _scriptState.Status = $"Mouse move to colour: {SearchColour} in area: {ColourSearchArea.PrintArea()}.";

            // perform the mouse move method
            //
            _mouseMovementService.MoveMouseToColour(ColourSearchArea, SearchColour);
        }

        /// <summary>
        /// returns a string which is added to the listbox to give information about the event which was added to the event list
        /// </summary>
        /// <returns></returns>
        public override string GetEventListBoxString()
        {
            if (GroupId == -1)
                return "Move mouse to random pixel of colour " + SearchColour + " in area " + ColourSearchArea.PrintArea() + ".";
            return "[Group " + GroupId + " Repeat x" + NumberOfCycles + "] Move mouse to random pixel of colour " + SearchColour + " in area " + ColourSearchArea.PrintArea() + ".";
        }
    }
}
