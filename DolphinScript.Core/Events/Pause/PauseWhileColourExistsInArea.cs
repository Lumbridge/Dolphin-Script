using System;
using System.Threading;
using DolphinScript.Core.Classes;
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
        }

        /// <summary>
        /// main overriden method used to perform this script event
        /// </summary>
        public override void InvokeScriptEvent()
        {
            ExecuteWhileLoop(() =>
            {
                ScriptState.Status = $"Pause while colour: {SearchColour} is found in area: {ColourSearchArea.PrintArea()}, waiting {ScriptState.SearchPause} seconds before re-searching.";
                Thread.Sleep(TimeSpan.FromSeconds(ScriptState.SearchPause));
            }, () => ColourService.ColourExistsInArea(ColourSearchArea, SearchColour));
        }

        /// <summary>
        /// returns a string which is added to the listbox to give information about the event which was added to the event list
        /// </summary>
        /// <returns></returns>
        public override string GetEventListBoxString()
        {
            return "Pause while colour " + SearchColour + " exists in area " + ColourSearchArea.PrintArea() + ".";
        }
    }
}
