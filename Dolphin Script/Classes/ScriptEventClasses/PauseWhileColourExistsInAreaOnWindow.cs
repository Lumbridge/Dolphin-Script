﻿using System;
using System.Threading;

using static DolphinScript.Lib.Backend.Common;
using static DolphinScript.Lib.Backend.WinAPI;
using static DolphinScript.Lib.Backend.ColourEvent;
using static DolphinScript.Lib.Backend.PointReturns;
using static DolphinScript.Lib.Backend.WindowControl;

namespace DolphinScript.Lib.ScriptEventClasses
{
    [Serializable]
    class PauseWhileColourExistsInAreaOnWindow : ScriptEvent
    {
        /// <summary>
        /// main overriden method used to perform this script event
        /// </summary>
        public override void DoEvent()
        {
            // don't override original click area or it will cause the mouse position to incrememnt every time this method is called
            //
            RECT NewSearchArea = GetClickAreaPositionOnWindow(WindowToClickHandle, ClickArea);

            Status = $"Pause while colour: {SearchColour} is found in area: {NewSearchArea.PrintArea()} on window: {WindowToClickTitle}, waiting {ReSearchPause} seconds before re-searching.";

            while (ColourExistsInArea(NewSearchArea, SearchColour))
            {
                BringEventWindowToFront(this);

                Thread.Sleep(TimeSpan.FromSeconds(ReSearchPause));

                // update the search area incase the window has moved
                //
                NewSearchArea = GetClickAreaPositionOnWindow(WindowToClickHandle, ClickArea);
            }
        }

        public override string GetEventListBoxString()
        {
            if (GroupID == -1)
                return "Pause while colour " + SearchColour + " exists in area " + ColourSearchArea.PrintArea() + " on " + WindowTitle + " window.";
            else
                return "[Group " + GroupID + " Repeat x" + NumberOfCycles + "] Pause while colour " + SearchColour + " exists in area " + ColourSearchArea.PrintArea() + " on " + WindowTitle + " window.";

        }
    }
}
