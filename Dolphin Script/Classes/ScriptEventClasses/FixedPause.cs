﻿using System;
using System.Threading;
using static DolphinScript.Classes.Backend.Common;

namespace DolphinScript.Classes.ScriptEventClasses
{
    /// <summary>
    /// This event will pause the script for a fixed period of time .
    /// </summary>
    [Serializable]
    public class FixedPause : ScriptEvent
    {
        /// <summary>
        /// main overriden method used to perform this script event
        /// </summary>
        public override void DoEvent()
        {
            // update the status label on the main form
            //
            Status = $"Fixed pause for {DelayDuration} seconds.";

            // sleep this thread for the specified time
            //
            Thread.Sleep(TimeSpan.FromSeconds(DelayDuration));
        }

        /// <summary>
        /// returns a string which is added to the listbox to give information about the event which was added to the event list
        /// </summary>
        /// <returns></returns>
        public override string GetEventListBoxString()
        {
            //
            // check if the event is part of a group to add a group tag to the list box string
            //

            if (GroupId == -1)
                return "Fixed Pause for " + DelayDuration + " seconds.";
            
            return "[Group " + GroupId + " Repeat x" + NumberOfCycles + "] Fixed Pause for " + DelayDuration + " seconds.";
        }
    }
}
