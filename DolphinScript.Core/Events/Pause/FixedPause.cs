using System;
using DolphinScript.Core.Classes;
using DolphinScript.Core.Events.BaseEvents;
using DolphinScript.Core.Interfaces;

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
        }

        /// <summary>
        /// main overriden method used to perform this script event
        /// </summary>
        public override void InvokeScriptEvent()
        {
            ScriptState.Status = $"Fixed pause for {DelayDuration} seconds.";
            base.InvokeScriptEvent();
        }

        /// <summary>
        /// returns a string which is added to the listbox to give information about the event which was added to the event list
        /// </summary>
        /// <returns></returns>
        public override string GetEventListBoxString()
        {
            return "Fixed Pause for " + DelayDuration + " seconds.";
        }
    }
}
