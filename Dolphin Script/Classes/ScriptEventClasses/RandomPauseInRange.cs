using System;
using System.Threading;
using static DolphinScript.Classes.Backend.Common;
using static DolphinScript.Classes.Backend.RandomNumber;

namespace DolphinScript.Classes.ScriptEventClasses
{
    [Serializable]
    class RandomPauseInRange : ScriptEvent
    {
        /// <summary>
        /// main overriden method used to perform this script event
        /// </summary>
        public override void DoEvent()
        {
            var delay = GetRandomDouble(DelayMinimum, DelayMaximum);

            // update the status label on the main form
            //
            Status = $"Random pause for {delay} seconds (Between {DelayMinimum} & {DelayMaximum} secs).";

            // wait for the randomly generated time before continuing
            //
            Thread.Sleep(TimeSpan.FromSeconds(delay));
        }

        /// <summary>
        /// returns a string which is added to the listbox to give information about the event which was added to the event list
        /// </summary>
        /// <returns></returns>
        public override string GetEventListBoxString()
        {
            if (GroupId == -1)
                return "Random pause between " + DelayMinimum + " and " + DelayMaximum + " seconds.";
            return "[Group " + GroupId + " Repeat x" + NumberOfCycles + "] Random pause between " + DelayMinimum + " and " + DelayMaximum + " seconds.";
        }
    }
}
