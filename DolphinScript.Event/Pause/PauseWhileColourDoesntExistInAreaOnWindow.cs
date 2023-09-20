using System;
using System.Threading;
using DolphinScript.Core.Classes;

namespace DolphinScript.Event.Pause
{
    [Serializable]
    public class PauseWhileColourDoesntExistInAreaOnWindow : ScriptEvent
    {
        /// <summary>
        /// main overriden method used to perform this script event
        /// </summary>
        public override void Invoke()
        {
            // don't override original click area or it will cause the mouse position to incrememnt every time this method is called
            //
            var newSearchArea = _pointService.GetClickAreaPositionOnWindow(WindowToClickHandle, ClickArea);

            // update the status label on the main form
            //
            _scriptState.Status = $"Pause while colour: {SearchColour} not found in area: {newSearchArea.PrintArea()} on window: {WindowToClickTitle}, waiting {_scriptState.SearchPause} seconds before re-searching.";

            ExecuteWhileLoop(() =>
            {
                // bring the window associated with this event to the front
                //
                _windowControlService.BringEventWindowToFront(this);

                Thread.Sleep(TimeSpan.FromSeconds(_scriptState.SearchPause));

                // update the search area incase the window has moved
                //
                newSearchArea = _pointService.GetClickAreaPositionOnWindow(WindowToClickHandle, ClickArea);
            }, () => !_colourService.ColourExistsInArea(newSearchArea, SearchColour));
        }

        /// <summary>
        /// returns a string which is added to the listbox to give information about the event which was added to the event list
        /// </summary>
        /// <returns></returns>
        public override string GetEventListBoxString()
        {
            if (GroupId == -1)
                return "Pause while colour " + SearchColour + " doesn't exist in area " + ColourSearchArea.PrintArea() + " on " + WindowTitle + " window.";
            return "[Group " + GroupId + " Repeat x" + NumberOfCycles + "] Pause while colour " + SearchColour + " doesn't exist in area " + ColourSearchArea.PrintArea() + " on " + WindowTitle + " window.";

        }
    }
}
