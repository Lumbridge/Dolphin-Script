using System;
using System.Drawing;
using DolphinScript.Core.Classes;
using static DolphinScript.Event.Mouse.MouseMove;

namespace DolphinScript.Event.Mouse
{
    [Serializable]
    public class MouseMoveToPointOnWindow : ScriptEvent
    {
        /// <summary>
        /// main overriden method used to perform this script event
        /// </summary>
        public override void Invoke()
        {
            // update the status label on the main form
            //
            _scriptState.Status = $"Move mouse to {CoordsToMoveTo} on window: {WindowTitle}.";

            // bring the window associated with this event to the front
            //
            _windowControlService.BringEventWindowToFront(this);

            var newClickPoint = new Point(
                _pointService.GetWindowPosition(WindowToClickHandle).Left + CoordsToMoveTo.X,
                _pointService.GetWindowPosition(WindowToClickHandle).Top + CoordsToMoveTo.Y);

            _mouseMovementService.MoveMouseToPoint(newClickPoint);
        }

        /// <summary>
        /// returns a string which is added to the listbox to give information about the event which was added to the event list
        /// </summary>
        /// <returns></returns>
        public override string GetEventListBoxString()
        {
            if (GroupId == -1)
                return "Move mouse to Point X: " + CoordsToMoveTo.X + " Y: " + CoordsToMoveTo.Y + " on " + WindowToClickTitle + " window.";
            return "[Group " + GroupId + " Repeat x" + NumberOfCycles + "] Move mouse to random point in area " + ClickArea.PrintArea() + " on " + WindowToClickTitle + " window.";
        }
    }
}
