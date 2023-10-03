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
        public MouseMoveToPointOnWindow() { }

        public MouseMoveToPointOnWindow(IMouseMovementService mouseMovementService, IPointService pointService, IWindowControlService windowControlService, IRandomService randomService) : base(mouseMovementService, pointService, windowControlService, randomService)
        {
            EventType = Constants.EventType.MouseMoveToPointOnWindow;
        }

        /// <summary>
        /// main overriden method used to perform this script event
        /// </summary>
        public override void Execute()
        {
            ScriptState.CurrentAction = $"Move mouse to {CoordsToMoveTo} on window: {EventProcess.WindowTitle}.";

            // bring the window associated with this event to the front
            WindowControlService.BringWindowToFront(EventProcess.WindowHandle);

            var windowLocation = PointService.GetWindowPosition(EventProcess.WindowHandle);

            var newClickPoint = new Point(windowLocation.left + CoordsToMoveTo.X, windowLocation.top + CoordsToMoveTo.Y);

            MouseMovementService.MoveMouseToPoint(newClickPoint);
        }

        /// <summary>
        /// returns a string which is added to the listbox to give information about the event which was added to the event list
        /// </summary>
        /// <returns></returns>
        public override string EventDescription()
        {
            return "Move mouse to Point X: " + CoordsToMoveTo.X + " Y: " + CoordsToMoveTo.Y + " on " + EventProcess.WindowTitle + " window.";
        }
    }
}
