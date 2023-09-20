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
            var windowHandle = WindowControlService.GetWindowHandle(WindowToClickTitle);

            // update the status label on the main form
            ScriptState.Status = $"Move mouse to {CoordsToMoveTo} on window: {WindowTitle}.";

            // bring the window associated with this event to the front
            WindowControlService.BringWindowToFront(windowHandle);

            var newClickPoint = new Point(
                PointService.GetWindowPosition(windowHandle).Left + CoordsToMoveTo.X,
                PointService.GetWindowPosition(windowHandle).Top + CoordsToMoveTo.Y);

            MouseMovementService.MoveMouseToPoint(newClickPoint);
        }

        /// <summary>
        /// returns a string which is added to the listbox to give information about the event which was added to the event list
        /// </summary>
        /// <returns></returns>
        public override string GetEventListBoxString()
        {
            if (!IsPartOfGroup)
                return "Move mouse to Point X: " + CoordsToMoveTo.X + " Y: " + CoordsToMoveTo.Y + " on " + WindowToClickTitle + " window.";
            return "[Group " + GroupId + " Repeat x" + NumberOfCycles + "] Move mouse to random point in area " + ClickArea.PrintArea() + " on " + WindowToClickTitle + " window.";
        }

        public MouseMoveToPointOnWindow(IMouseMovementService mouseMovementService, IPointService pointService, IWindowControlService windowControlService, IRandomService randomService) : base(mouseMovementService, pointService, windowControlService, randomService)
        {
        }

        public MouseMoveToPointOnWindow() { }
    }
}
