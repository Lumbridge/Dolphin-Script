using System;
using System.Threading;
using DolphinScript.Core.Classes;

namespace DolphinScript.Event.Pause
{
    [Serializable]
    public class PauseWhileColourExistsInArea : ScriptEvent
    {
        /// <summary>
        /// main overriden method used to perform this script event
        /// </summary>
        public override void Invoke()
        {
            ExecuteWhileLoop(() =>
            {
                // update the status label on the main form
                //
                _scriptState.Status = $"Pause while colour: {SearchColour} is found in area: {ColourSearchArea.PrintArea()}, waiting {_scriptState.SearchPause} seconds before re-searching.";

                Thread.Sleep(TimeSpan.FromSeconds(_scriptState.SearchPause));
            }, () => _colourService.ColourExistsInArea(ColourSearchArea, SearchColour));
        }

        /// <summary>
        /// returns a string which is added to the listbox to give information about the event which was added to the event list
        /// </summary>
        /// <returns></returns>
        public override string GetEventListBoxString()
        {
            if (GroupId == -1)
                return "Pause while colour " + SearchColour + " exists in area " + ColourSearchArea.PrintArea() + ".";
            return "[Group " + GroupId + " Repeat x" + NumberOfCycles + "] Pause while colour " + SearchColour + " exists in area " + ColourSearchArea.PrintArea() + ".";
        }
    }
}
