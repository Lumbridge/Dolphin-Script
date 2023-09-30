using System;
using DolphinScript.Core.Classes;
using DolphinScript.Core.Events.BaseEvents;
using DolphinScript.Core.Interfaces;

namespace DolphinScript.Core.Events.Pause
{
    [Serializable]
    public class RandomPauseInRange : PauseEvent
    {
        public RandomPauseInRange() { }

        public RandomPauseInRange(IRandomService randomService, IColourService colourService, IPointService pointService, IWindowControlService windowControlService) : base(randomService, colourService, pointService, windowControlService)
        {
        }

        /// <summary>
        /// main overriden method used to perform this script event
        /// </summary>
        public override void InvokeScriptEvent()
        {
            // update the status label on the main form
            ScriptState.Status = $"Random pause Between {DelayMinimum} and {DelayMaximum} seconds.";
            base.InvokeScriptEvent();
        }

        /// <summary>
        /// returns a string which is added to the listbox to give information about the event which was added to the event list
        /// </summary>
        /// <returns></returns>
        public override string GetEventListBoxString()
        {
            return "Random pause between " + DelayMinimum + " and " + DelayMaximum + " seconds.";
        }
    }
}
