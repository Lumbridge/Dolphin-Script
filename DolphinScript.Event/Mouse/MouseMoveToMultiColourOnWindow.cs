using System;
using DolphinScript.Core.Classes;
using static DolphinScript.Event.Mouse.MouseMoveToColour;

namespace DolphinScript.Event.Mouse
{
    [Serializable]
    public class MouseMoveToMultiColourOnWindow : ScriptEvent
    {
        /// <summary>
        /// main overriden method used to perform this script event
        /// </summary>
        public override void Invoke()
        {
            // update the status label on the main form
            //
            _scriptState.Status = $"Mouse move to any colour in list of selected colours on window: {WindowToClickTitle}.";

            // bring the window associated with this event to the front
            //
            _windowControlService.BringEventWindowToFront(this);

            // don't override original click area or it will cause the mouse position to incrememnt every time this method is called
            //
            var newSearchColour = SearchColours[_randomService.GetRandomNumber(0, SearchColours.Count - 1)];
            var newClickArea = _pointService.GetClickAreaPositionOnWindow(WindowToClickHandle, ClickArea);
            
            _mouseMovementService.MoveMouseToColour(newClickArea, newSearchColour);
        }

        /// <summary>
        /// returns a string which is added to the listbox to give information about the event which was added to the event list
        /// </summary>
        /// <returns></returns>
        public override string GetEventListBoxString()
        {
            if (GroupId == -1)
                return "Move mouse to random pixel matching colour " + SearchColours.ToString() + " in area " + ClickArea.PrintArea() + " on " + WindowToClickTitle + " window.";
            return "[Group " + GroupId + " Repeat x" + NumberOfCycles + "] Move mouse to random pixel matching colour " + SearchColour + " in area " + ClickArea.PrintArea() + " on " + WindowToClickTitle + " window.";
        }
    }
}
