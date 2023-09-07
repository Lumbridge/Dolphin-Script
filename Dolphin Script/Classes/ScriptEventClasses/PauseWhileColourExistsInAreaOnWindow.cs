﻿using System;
using System.Threading;
using static DolphinScript.Classes.Backend.ColourEvent;
using static DolphinScript.Classes.Backend.Common;
using static DolphinScript.Classes.Backend.PointReturns;
using static DolphinScript.Classes.Backend.WindowControl;

namespace DolphinScript.Classes.ScriptEventClasses
{
    [Serializable]
    public class PauseWhileColourExistsInAreaOnWindow : ScriptEvent
    {
        /// <summary>
        /// main overriden method used to perform this script event
        /// </summary>
        public override void Invoke()
        {
            // don't override original click area or it will cause the mouse position to increment every time this method is called
            //
            var newSearchArea = GetClickAreaPositionOnWindow(WindowToClickHandle, ClickArea);

            // update the status label on the main form
            //
            Status = $"Pause while colour: {SearchColour} is found in area: {newSearchArea.PrintArea()} on window: {WindowToClickTitle}, waiting {ReSearchPause} seconds before re-searching.";

            RunWhileLoop(() =>
            {
                // bring the window associated with this event to the front
                //
                BringEventWindowToFront(this);

                Thread.Sleep(TimeSpan.FromSeconds(ReSearchPause));

                // update the search area incase the window has moved
                //
                newSearchArea = GetClickAreaPositionOnWindow(WindowToClickHandle, ClickArea);
            }, () => ColourExistsInArea(newSearchArea, SearchColour));
        }

        /// <summary>
        /// returns a string which is added to the listbox to give information about the event which was added to the event list
        /// </summary>
        /// <returns></returns>
        public override string GetEventListBoxString()
        {
            if (GroupId == -1)
                return "Pause while colour " + SearchColour + " exists in area " + ColourSearchArea.PrintArea() + " on " + WindowTitle + " window.";
            return "[Group " + GroupId + " Repeat x" + NumberOfCycles + "] Pause while colour " + SearchColour + " exists in area " + ColourSearchArea.PrintArea() + " on " + WindowTitle + " window.";

        }
    }
}
