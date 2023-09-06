using System;
using System.Threading;
using static DolphinScript.Classes.Backend.Common;
using static DolphinScript.Classes.Backend.ColourEvent;

namespace DolphinScript.Classes.ScriptEventClasses
{
    [Serializable]
    public class PauseWhileColourExistsInArea : ScriptEvent
    {
        /// <summary>
        /// main overriden method used to perform this script event
        /// </summary>
        public override void DoEvent()
        {
            while (ColourExistsInArea(ColourSearchArea, SearchColour))
            {
                // update the status label on the main form
                //
                Status = $"Pause while colour: {SearchColour} is found in area: {ColourSearchArea.PrintArea()}, waiting {ReSearchPause} seconds before re-searching.";

                Thread.Sleep(TimeSpan.FromSeconds(ReSearchPause));
            }
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
