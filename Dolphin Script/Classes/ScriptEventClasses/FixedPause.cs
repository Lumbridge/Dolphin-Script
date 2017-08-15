using System;
using System.Threading;

namespace DolphinScript.Lib.ScriptEventClasses
{
    class FixedPause : ScriptEvent
    {
        public FixedPause()
        {
            EventType = Event.Fixed_Pause;
        }

        public override void DoEvent()
        {
            Thread.Sleep(TimeSpan.FromSeconds(DelayDuration));
        }

        public override string GetEventListBoxString()
        {
            if (GroupID == -1)
                return "Fixed Pause for " + DelayDuration + " seconds.";
            else
                return "[Group " + GroupID + "] Fixed Pause for " + DelayDuration + " seconds.";
        }
    }
}
