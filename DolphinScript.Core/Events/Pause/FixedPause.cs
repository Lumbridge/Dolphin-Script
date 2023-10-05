using DolphinScript.Core.Constants;
using DolphinScript.Core.Events.BaseEvents;
using DolphinScript.Core.Interfaces;
using System;

namespace DolphinScript.Core.Events.Pause
{
    /// <summary>
    /// This event will pause the script for a fixed period of time .
    /// </summary>
    [Serializable]
    public class FixedPause : PauseEvent
    {
        public FixedPause() { }

        public FixedPause(IRandomService randomService,
            IColourService colourService, IPointService pointService, IWindowControlService windowControlService)
            : base(randomService, colourService, pointService, windowControlService)
        {
            EventType = ScriptEventConstants.EventType.FixedPause;
        }
        
        /// <summary>
        /// returns a string which is added to the listbox to give information about the event which was added to the event list
        /// </summary>
        /// <returns></returns>
        public override string EventDescription()
        {
            return "Fixed Pause for " + DelayDuration + " seconds";
        }
    }
}
