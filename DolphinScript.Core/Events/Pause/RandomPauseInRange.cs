using DolphinScript.Core.Constants;
using DolphinScript.Core.Events.BaseEvents;
using DolphinScript.Core.Interfaces;
using System;

namespace DolphinScript.Core.Events.Pause
{
    [Serializable]
    public class RandomPauseInRange : PauseEvent
    {
        public RandomPauseInRange() { }

        public RandomPauseInRange(IRandomService randomService, IColourService colourService, IPointService pointService, IWindowControlService windowControlService) : base(randomService, colourService, pointService, windowControlService)
        {
            EventType = ScriptEventConstants.EventType.RandomPauseInRange;
        }

        /// <summary>
        /// returns a string which is added to the listbox to give information about the event which was added to the event list
        /// </summary>
        /// <returns></returns>
        public override string EventDescription()
        {
            return "Random pause between " + DelayMinimum + " and " + DelayMaximum + " seconds";
        }
    }
}
