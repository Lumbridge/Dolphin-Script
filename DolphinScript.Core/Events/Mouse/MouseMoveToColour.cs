using System;
using DolphinScript.Core.Classes;
using DolphinScript.Core.Events.BaseEvents;
using DolphinScript.Core.Interfaces;

namespace DolphinScript.Core.Events.Mouse
{
    [Serializable]
    public class MouseMoveToColour : MouseMoveEvent
    {
        public MouseMoveToColour() { }

        public MouseMoveToColour(IMouseMovementService mouseMovementService, IPointService pointService, IWindowControlService windowControlService, IRandomService randomService) : base(mouseMovementService, pointService, windowControlService, randomService)
        {
        }

        /// <summary>
        /// main overriden method used to perform this script event
        /// </summary>
        public override void Invoke()
        {
            // update the status label on the main form
            ScriptState.Status = $"Mouse move to colour: {SearchColour} in area: {ColourSearchArea.PrintArea()}.";

            // perform the mouse move method
            MouseMovementService.MoveMouseToColour(ColourSearchArea, SearchColour);
        }

        /// <summary>
        /// returns a string which is added to the listbox to give information about the event which was added to the event list
        /// </summary>
        /// <returns></returns>
        public override string GetEventListBoxString()
        {
            if (!IsPartOfGroup)
                return "Move mouse to random pixel of colour " + SearchColour + " in area " + ColourSearchArea.PrintArea() + ".";
            return "[Group " + GroupId + " Repeat x" + NumberOfCycles + "] Move mouse to random pixel of colour " + SearchColour + " in area " + ColourSearchArea.PrintArea() + ".";
        }
    }
}
