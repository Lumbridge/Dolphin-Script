﻿using System;
using DolphinScript.Core.Interfaces;
using DolphinScript.Event.BaseEvents;
using DolphinScript.Event.Interfaces;

namespace DolphinScript.Event.Pause
{
    [Serializable]
    public class RandomPauseInRange : PauseEvent
    {
        public RandomPauseInRange() { }

        public RandomPauseInRange(IScriptState scriptState, IRandomService randomService,
            IColourService colourService, IPointService pointService, IWindowControlService windowControlService)
            : base(scriptState, randomService, colourService, pointService, windowControlService)
        {
        }

        /// <summary>
        /// main overriden method used to perform this script event
        /// </summary>
        public override void Invoke()
        {
            // update the status label on the main form
            //
            _scriptState.Status = $"Random pause Between {DelayMinimum} & {DelayMaximum} secs.";

            base.Invoke();
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
