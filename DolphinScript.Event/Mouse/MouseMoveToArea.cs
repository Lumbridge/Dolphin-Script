using System;
using DolphinScript.Core.Classes;
using DolphinScript.Core.WindowsApi;

namespace DolphinScript.Event.Mouse
{
    /// <summary>
    /// This event moves the mouse cursor to a random point in a given area.
    /// </summary>
    [Serializable]
    public class MouseMoveToArea : ScriptEvent
    {
        /// <summary>
        /// wrote this function in here for clarity in the doevent method
        /// </summary>
        /// <param name="clickArea"></param>
        public void MoveMouseToArea(CommonTypes.Rect clickArea)
        {
            var endPoint = _pointService.GetRandomPointInArea(clickArea);
            _mouseMovementService.MoveMouseToPoint(endPoint); // move mouse to picked pixel
        }

        /// <summary>
        /// main overriden method used to perform this script event
        /// </summary>
        public override void Invoke()
        {
            // update the status label on the main form
            _scriptState.Status = $"Mouse move to random point in area: {ClickArea.PrintArea()}.";
            MoveMouseToArea(ClickArea);
        }

        /// <summary>
        /// returns a string which is added to the listbox to give information about the event which was added to the event list
        /// </summary>
        /// <returns></returns>
        public override string GetEventListBoxString()
        {
            if (GroupId == -1)
                return "Move mouse to random point in area " + ClickArea.PrintArea() + ".";
            return "[Group " + GroupId + " Repeat x" + NumberOfCycles + "] Move mouse to random point in area " + ClickArea.PrintArea() + ".";
        }
    }
}
