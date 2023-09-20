using System;
using DolphinScript.Core.Classes;
using DolphinScript.Core.Events.BaseEvents;
using DolphinScript.Core.Interfaces;

namespace DolphinScript.Core.Events.Mouse
{
    [Serializable]
    public class MouseMoveToColourOnWindow : MouseMoveEvent
    {
        /// <summary>
        /// main overriden method used to perform this script event
        /// </summary>
        public override void Invoke()
        {
            // update the status label on the main form
            //
            ScriptState.Status = $"Mouse move to colour: {SearchColour} on window: {WindowToClickTitle}.";

            // bring the window associated with this event to the front
            //
            WindowControlService.BringWindowToFront(WindowToClickHandle);

            // don't override original click area or it will cause the mouse position to incrememnt every time this method is called
            var newSearchArea = PointService.GetClickAreaPositionOnWindow(WindowToClickHandle, ClickArea);

            MouseMovementService.MoveMouseToColour(newSearchArea, SearchColour);
        }

        /// <summary>
        /// returns a string which is added to the listbox to give information about the event which was added to the event list
        /// </summary>
        /// <returns></returns>
        public override string GetEventListBoxString()
        {
            if (!IsPartOfGroup)
                return "Move mouse to random pixel matching colour " + SearchColour + " in area " + ClickArea.PrintArea() + " on " + WindowControlService.GetWindowTitle(WindowToClickHandle) + " window.";
            return "[Group " + GroupId + " Repeat x" + NumberOfCycles + "] Move mouse to random pixel matching colour " + SearchColour + " in area " + ClickArea.PrintArea() + " on " + WindowControlService.GetWindowTitle(WindowToClickHandle) + " window.";
        }

        public MouseMoveToColourOnWindow(IMouseMovementService mouseMovementService, IPointService pointService, IWindowControlService windowControlService, IRandomService randomService) : base(mouseMovementService, pointService, windowControlService, randomService)
        {
        }

        public MouseMoveToColourOnWindow() { }
    }
}
