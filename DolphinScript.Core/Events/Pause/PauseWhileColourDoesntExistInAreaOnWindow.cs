using System;
using System.Threading;
using DolphinScript.Core.Classes;
using DolphinScript.Core.Constants;
using DolphinScript.Core.Events.BaseEvents;
using DolphinScript.Core.Interfaces;

namespace DolphinScript.Core.Events.Pause
{
    [Serializable]
    public class PauseWhileColourDoesntExistInAreaOnWindow : PauseEvent
    {
        public PauseWhileColourDoesntExistInAreaOnWindow() { }

        public PauseWhileColourDoesntExistInAreaOnWindow(IRandomService randomService, IColourService colourService, IPointService pointService, IWindowControlService windowControlService) : base(randomService, colourService, pointService, windowControlService)
        {
            EventType = ScriptEventConstants.EventType.PauseWhileColourDoesntExistInAreaOnWindow;
        }

        /// <summary>
        /// main overriden method used to perform this script event
        /// </summary>
        public override void Execute()
        {
            // don't override original click area or it will cause the mouse position to increment every time this method is called
            var newSearchArea = PointService.GetClickAreaPositionOnWindow(EventProcess.WindowHandle, ClickArea);

            ExecuteWhileLoop(() =>
            {
                WindowControlService.BringWindowToFront(EventProcess.WindowHandle);
                ScriptState.CurrentAction = $"Pause while colour: {SearchColour} not found in area: {newSearchArea.PrintArea()} on window: {EventProcess.WindowTitle}, waiting {ScriptState.SearchPause} seconds before re-searching.";
                ScriptState.AllEvents.ResetBindings();
                Thread.Sleep(TimeSpan.FromSeconds(ScriptState.SearchPause));
                // update the search area in case the window has moved
                newSearchArea = PointService.GetClickAreaPositionOnWindow(EventProcess.WindowHandle, ClickArea);
            }, () => !ColourService.ColourExistsInArea(newSearchArea, SearchColour));
        }

        /// <summary>
        /// returns a string which is added to the listbox to give information about the event which was added to the event list
        /// </summary>
        /// <returns></returns>
        public override string EventDescription()
        {
            return $"Pause while colour {SearchColour} doesn't exist in area {ColourSearchArea.PrintArea()} on {EventProcess.WindowTitle} window";
        }
    }
}
