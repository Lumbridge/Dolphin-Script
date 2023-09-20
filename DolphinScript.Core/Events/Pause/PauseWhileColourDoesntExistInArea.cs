using System;
using System.Threading;
using DolphinScript.Core.Classes;
using DolphinScript.Core.Events.BaseEvents;
using DolphinScript.Core.Interfaces;

namespace DolphinScript.Core.Events.Pause
{
    [Serializable]
    public class PauseWhileColourDoesntExistInArea : PauseEvent
    {
        public PauseWhileColourDoesntExistInArea(IRandomService randomService, IColourService colourService, 
            IPointService pointService, IWindowControlService windowControlService) 
            : base(randomService, colourService, pointService, windowControlService)
        {
        }

        /// <summary>
        /// main overriden method used to perform this script event
        /// </summary>
        public override void Invoke()
        {
            ExecuteWhileLoop(() =>
            {
                // update the status label on the main form
                //
                ScriptState.Status = $"Pause while colour: {SearchColour} not found in area: {ColourSearchArea.PrintArea()}, waiting {ScriptState.SearchPause} seconds before re-searching.";

                Thread.Sleep(TimeSpan.FromSeconds(ScriptState.SearchPause));

            }, () => !ColourService.ColourExistsInArea(ColourSearchArea, SearchColour));
        }

        /// <summary>
        /// returns a string which is added to the listbox to give information about the event which was added to the event list
        /// </summary>
        /// <returns></returns>
        public override string GetEventListBoxString()
        {
            if (!IsPartOfGroup)
                return "Pause while colour " + SearchColour + " doesn't exist in area " + ColourSearchArea.PrintArea() + ".";
            return "[Group " + GroupId + " Repeat x" + NumberOfCycles + "] Pause while colour " + SearchColour + " doesn't exist in area " + ColourSearchArea.PrintArea() + ".";
        }
    }
}
