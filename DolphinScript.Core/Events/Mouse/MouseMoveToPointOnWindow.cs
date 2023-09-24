using System;
using System.Drawing;
using DolphinScript.Core.Classes;
using DolphinScript.Core.Events.BaseEvents;
using DolphinScript.Core.Interfaces;

namespace DolphinScript.Core.Events.Mouse
{
    [Serializable]
    public class MouseMoveToPointOnWindow : MouseMoveEvent
    {
        /// <summary>
        /// main overriden method used to perform this script event
        /// </summary>
        public override void Invoke()
        {
            var windowHandle = WindowControlService.GetWindowHandle(WindowTitle);

            // update the status label on the main form
            ScriptState.Status = $"Move mouse to {CoordsToMoveTo} on window: {WindowTitle}.";

            // bring the window associated with this event to the front
            WindowControlService.BringWindowToFront(windowHandle);

            var windowLocation = PointService.GetWindowPosition(windowHandle);

            var newClickPoint = new Point(windowLocation.left + CoordsToMoveTo.X, windowLocation.top + CoordsToMoveTo.Y);

            MouseMovementService.MoveMouseToPoint(newClickPoint);
        }

        /// <summary>
        /// returns a string which is added to the listbox to give information about the event which was added to the event list
        /// </summary>
        /// <returns></returns>
        public override string GetEventListBoxString()
        {
            if (!IsPartOfGroup)
                return "Move mouse to Point X: " + CoordsToMoveTo.X + " Y: " + CoordsToMoveTo.Y + " on " + WindowTitle + " window.";
            return "[Group " + GroupId + " Repeat x" + NumberOfCycles + "] Move mouse to random point in area " + ClickArea.PrintArea() + " on " + WindowTitle + " window.";
        }

        public MouseMoveToPointOnWindow(IMouseMovementService mouseMovementService, IPointService pointService, IWindowControlService windowControlService, IRandomService randomService) : base(mouseMovementService, pointService, windowControlService, randomService)
        {
        }

        public MouseMoveToPointOnWindow() { }
    }
}
