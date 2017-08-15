using System;
using System.Threading;

using static DolphinScript.Lib.Backend.RandomNumber;
using static DolphinScript.Lib.Backend.GlobalVariables;

namespace DolphinScript.Lib.ScriptEventClasses
{
    class RandomPauseInRange : ScriptEvent
    {
        public RandomPauseInRange()
        {
            EventType = Event.Random_Pause_In_Range;
        }

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
                return "[Group " + GroupID + "] Random pause between " + DelayMinimum + " and " + DelayMaximum + " seconds.";
        }
    }
}
