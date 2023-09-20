using System;
using System.Threading;
using DolphinScript.Core.Classes;
using DolphinScript.Core.Events.BaseEvents;
using DolphinScript.Core.Interfaces;

namespace DolphinScript.Core.Events.Pause
{
    [Serializable]
    public class PauseWhileColourExistsInAreaOnWindow : PauseEvent
    {
        public PauseWhileColourExistsInAreaOnWindow(IRandomService randomService, IColourService colourService, IPointService pointService, IWindowControlService windowControlService) : base(randomService, colourService, pointService, windowControlService)
        {
        }

        /// <summary>
        /// main overriden method used to perform this script event
        /// </summary>
        public override void Invoke()
        {
            // don't override original click area or it will cause the mouse position to increment every time this method is called
            //
            var newSearchArea = PointService.GetClickAreaPositionOnWindow(WindowToClickHandle, ClickArea);

            // update the status label on the main form
            //
            ScriptState.Status = $"Pause while colour: {SearchColour} is found in area: {newSearchArea.PrintArea()} on window: {WindowToClickTitle}, waiting {ScriptState.SearchPause} seconds before re-searching.";

            ExecuteWhileLoop(() =>
            {
                // bring the window associated with this event to the front
                //
                WindowControlService.BringWindowToFront(WindowToClickHandle);

                Thread.Sleep(TimeSpan.FromSeconds(ScriptState.SearchPause));

                // update the search area incase the window has moved
                //
                newSearchArea = PointService.GetClickAreaPositionOnWindow(WindowToClickHandle, ClickArea);
            }, () => ColourService.ColourExistsInArea(newSearchArea, SearchColour));
        }

        /// <summary>
        /// returns a string which is added to the listbox to give information about the event which was added to the event list
        /// </summary>
        /// <returns></returns>
        public override string GetEventListBoxString()
        {
            if (!IsPartOfGroup)
                return "Pause while colour " + SearchColour + " exists in area " + ColourSearchArea.PrintArea() + " on " + WindowTitle + " window.";
            return "[Group " + GroupId + " Repeat x" + NumberOfCycles + "] Pause while colour " + SearchColour + " exists in area " + ColourSearchArea.PrintArea() + " on " + WindowTitle + " window.";

        }
    }
}
