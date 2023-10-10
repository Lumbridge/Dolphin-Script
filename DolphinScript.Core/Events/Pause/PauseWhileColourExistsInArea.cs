using System;
using System.Threading;
using DolphinScript.Core.Classes;
using DolphinScript.Core.Constants;
using DolphinScript.Core.Events.BaseEvents;
using DolphinScript.Core.Interfaces;

namespace DolphinScript.Core.Events.Pause
{
    [Serializable]
    public class PauseWhileColourExistsInArea : PauseEvent
    {
        public PauseWhileColourExistsInArea() { }

        public PauseWhileColourExistsInArea(IRandomService randomService, IColourService colourService, IPointService pointService, IWindowControlService windowControlService) : base(randomService, colourService, pointService, windowControlService)
        {
            EventType = ScriptEventConstants.EventType.PauseWhileColourExistsInArea;
        }

        /// <summary>
        /// main overriden method used to perform this script event
        /// </summary>
        public override void Execute()
        {
            ExecuteWhileLoop(() =>
            {
                ScriptState.CurrentAction = $"Pause while colour: {SearchColour} is found in area: {ColourSearchArea}, waiting {ScriptState.SearchPause} seconds before re-searching";
                ScriptState.AllEvents.ResetBindings();
                Thread.Sleep(TimeSpan.FromSeconds(ScriptState.SearchPause));
            }, () => ColourService.ColourExistsInArea(ColourSearchArea, SearchColour));
        }

        /// <summary>
        /// returns a string which is added to the listbox to give information about the event which was added to the event list
        /// </summary>
        /// <returns></returns>
        public override string EventDescription()
        {
            return $"Pause while colour {SearchColour} exists in area {ColourSearchArea}";
        }
    }
}
