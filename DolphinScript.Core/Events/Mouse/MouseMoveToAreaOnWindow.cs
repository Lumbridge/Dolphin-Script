using DolphinScript.Core.Classes;
using DolphinScript.Core.Events.BaseEvents;
using DolphinScript.Core.Interfaces;
using System;

namespace DolphinScript.Core.Events.Mouse
{
    /// <summary>
    /// This event moves the mouse cursor to a random point in a given area on a specific window.
    /// </summary>
    [Serializable]
    public class MouseMoveToAreaOnWindow : MouseMoveEvent
    {
        public MouseMoveToAreaOnWindow() { }

        public MouseMoveToAreaOnWindow(IMouseMovementService mouseMovementService, IPointService pointService, IWindowControlService windowControlService, IRandomService randomService) : base(mouseMovementService, pointService, windowControlService, randomService)
        {
            EventType = Constants.EventType.MouseMoveToAreaOnWindow;
        }

        /// <summary>
        /// main overriden method used to perform this script event
        /// </summary>
        public override void Execute()
        {
            ScriptState.CurrentAction = $"Mouse move to area: {ClickArea.PrintArea()} on window: {EventProcess.WindowTitle}.";

            // bring the window associated with this event to the front
            WindowControlService.BringWindowToFront(EventProcess.WindowHandle);

            // don't override original click area or it will cause the mouse position to increment every time this method is called
            var clickArea = PointService.GetClickAreaPositionOnWindow(EventProcess.WindowHandle, ClickArea);

            CoordsToMoveTo = PointService.GetRandomPointInArea(clickArea);

            base.Execute();
        }

        /// <summary>
        /// returns a string which is added to the listbox to give information about the event which was added to the event list
        /// </summary>
        /// <returns></returns>
        public override string EventDescription()
        {
            return "Move mouse to random point in area " + ClickArea.PrintArea() + " on " + EventProcess.WindowTitle + " window.";
        }
    }
}
