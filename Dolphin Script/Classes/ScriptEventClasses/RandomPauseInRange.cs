using System;
using System.Threading;

using static DolphinScript.Lib.Backend.Common;
using static DolphinScript.Lib.Backend.RandomNumber;

namespace DolphinScript.Lib.ScriptEventClasses
{
    [Serializable]
    class RandomPauseInRange : ScriptEvent
    {
        /// <summary>
        /// main overriden method used to perform this script event
        /// </summary>
        public override void DoEvent()
        {
            double delay = GetRandomDouble(DelayMinimum, DelayMaximum);

            Write("Waiting for " + delay + " seconds.");

            Thread.Sleep(TimeSpan.FromSeconds(delay));
        }

        public override string GetEventListBoxString()
        {
            if (GroupID == -1)
                return "Random pause between " + DelayMinimum + " and " + DelayMaximum + " seconds.";
            else
                return "[Group " + GroupID + " Repeat x" + NumberOfCycles + "] Random pause between " + DelayMinimum + " and " + DelayMaximum + " seconds.";
        }
    }
}
