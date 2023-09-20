using DolphinScript.Core.Classes;
using DolphinScript.Core.WindowsApi;
using System;
using System.Drawing;
using System.Threading.Tasks;

namespace DolphinScript.Event.Mouse
{
    /// <summary>
    /// This class provides the core mouse moving functionality.
    /// </summary>
    [Serializable]
    public class MouseMove : ScriptEvent
    {
        /// <summary>
        /// main overriden method used to perform this script event
        /// </summary>
        public override void Invoke()
        {
            // update the status label on the main form
            //
            _scriptState.Status = $"Mouse move: {CoordsToMoveTo}.";
            _mouseMovementService.MoveMouseToPoint(CoordsToMoveTo);
        }

        /// <summary>
        /// returns a string which is added to the listbox to give information about the event which was added to the event list
        /// </summary>
        /// <returns></returns>
        public override string GetEventListBoxString()
        {
            if (GroupId == -1)
                return "Move mouse to Point X: " + CoordsToMoveTo.X + " Y: " + CoordsToMoveTo.Y + ".";
            return "[Group " + GroupId + " Repeat x" + NumberOfCycles + "] Move mouse to Point X: " + CoordsToMoveTo.X + " Y: " + CoordsToMoveTo.Y + ".";
        }
    }
}