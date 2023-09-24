using System;
using DolphinScript.Core.Classes;
using DolphinScript.Core.Concrete;
using DolphinScript.Core.Events.BaseEvents;
using DolphinScript.Core.Interfaces;
using DolphinScript.Core.WindowsApi;

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
        }

        /// <summary>
        /// main overriden method used to perform this script event
        /// </summary>
        public override void Invoke()
        {
            var windowHandle = WindowControlService.GetWindowHandle(WindowTitle);

            // update the status label on the main form
            ScriptState.Status = $"Mouse move to area: {ClickArea.PrintArea()} on window: {WindowTitle}.";

            // bring the window associated with this event to the front
            WindowControlService.BringWindowToFront(windowHandle);

            // don't override original click area or it will cause the mouse position to increment every time this method is called
            var clickArea = PointService.GetClickAreaPositionOnWindow(windowHandle, ClickArea);

            CoordsToMoveTo = PointService.GetRandomPointInArea(clickArea);

            base.Invoke();
        }

        /// <summary>
        /// returns a string which is added to the listbox to give information about the event which was added to the event list
        /// </summary>
        /// <returns></returns>
        public override string GetEventListBoxString()
        {
            if (!IsPartOfGroup)
                return "Move mouse to random point in area " + ClickArea.PrintArea() + " on " + WindowTitle + " window.";
            return "[Group " + GroupId + " Repeat x" + NumberOfCycles + "] Move mouse to random point in area " + ClickArea.PrintArea() + " on " + WindowTitle + " window.";
        }
    }
}
