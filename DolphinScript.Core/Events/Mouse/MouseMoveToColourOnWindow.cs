using System;
using DolphinScript.Core.Classes;
using DolphinScript.Core.Events.BaseEvents;
using DolphinScript.Core.Interfaces;

namespace DolphinScript.Core.Events.Mouse
{
    [Serializable]
    public class MouseMoveToColourOnWindow : MouseMoveEvent
    {
        public MouseMoveToColourOnWindow() { }

        public MouseMoveToColourOnWindow(IMouseMovementService mouseMovementService, IPointService pointService, IWindowControlService windowControlService, IRandomService randomService) : base(mouseMovementService, pointService, windowControlService, randomService)
        {
        }

        /// <summary>
        /// main overriden method used to perform this script event
        /// </summary>
        public override void InvokeScriptEvent()
        {
            var windowHandle = WindowControlService.GetWindowHandle(WindowTitle);

            ScriptState.Status = $"Mouse move to colour: {SearchColour} on window: {WindowTitle}.";

            // bring the window associated with this event to the front
            WindowControlService.BringWindowToFront(windowHandle);

            // don't override original click area or it will cause the mouse position to incrememnt every time this method is called
            var newSearchArea = PointService.GetClickAreaPositionOnWindow(windowHandle, ClickArea);

            MouseMovementService.MoveMouseToColour(newSearchArea, SearchColour);
        }

        /// <summary>
        /// returns a string which is added to the listbox to give information about the event which was added to the event list
        /// </summary>
        /// <returns></returns>
        public override string GetEventListBoxString()
        {
            return "Move mouse to random pixel matching colour " + SearchColour + " in area " + ClickArea.PrintArea() + " on " + WindowTitle + " window.";
        }
    }
}
