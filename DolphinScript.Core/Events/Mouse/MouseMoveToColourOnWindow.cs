using System;
using DolphinScript.Core.Classes;
using DolphinScript.Core.Constants;
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
            EventType = ScriptEventConstants.EventType.MouseMoveToColourOnWindow;
        }

        /// <summary>
        /// main overriden method used to perform this script event
        /// </summary>
        public override void Execute()
        {
            ScriptState.CurrentAction = $"Mouse move to colour: {SearchColour} on window: {EventProcess.WindowTitle}.";

            // bring the window associated with this event to the front
            WindowControlService.BringWindowToFront(EventProcess.WindowHandle);

            // don't override original click area or it will cause the mouse position to increment every time this method is called
            var newSearchArea = PointService.GetClickAreaPositionOnWindow(EventProcess.WindowHandle, ClickArea);

            MouseMovementService.MoveMouseToColour(newSearchArea, SearchColour);
        }

        /// <summary>
        /// returns a string which is added to the listbox to give information about the event which was added to the event list
        /// </summary>
        /// <returns></returns>
        public override string EventDescription()
        {
            return "Move mouse to random pixel matching colour " + SearchColour + " in area " + ClickArea.PrintArea() + " on " + EventProcess.WindowTitle + " window.";
        }
    }
}
