using System;
using System.Threading;
using DolphinScript.Core.Classes;
using DolphinScript.Core.Constants;
using DolphinScript.Core.Events.BaseEvents;
using DolphinScript.Core.Interfaces;

namespace DolphinScript.Core.Events.Pause
{
    [Serializable]
    public class PauseWhileColourExistsInAreaOnWindow : PauseEvent
    {
        public PauseWhileColourExistsInAreaOnWindow() { }

        public PauseWhileColourExistsInAreaOnWindow(IRandomService randomService, IColourService colourService, IPointService pointService, IWindowControlService windowControlService) : base(randomService, colourService, pointService, windowControlService)
        {
            EventType = ScriptEventConstants.EventType.PauseWhileColourExistsInAreaOnWindow;
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
                ScriptState.CurrentAction = $"Pause while colour: {SearchColour} is found in area: {newSearchArea} on window: {EventProcess.WindowTitle}, waiting {ScriptState.SearchPause} seconds before re-searching";
                WindowControlService.BringWindowToFront(EventProcess.WindowHandle);
                ScriptState.AllEvents.ResetBindings();
                Thread.Sleep(TimeSpan.FromSeconds(ScriptState.SearchPause));
                // update the search area in case the window has moved
                newSearchArea = PointService.GetClickAreaPositionOnWindow(EventProcess.WindowHandle, ClickArea);
            }, () => ColourService.ColourExistsInArea(newSearchArea, SearchColour));
        }

        /// <summary>
        /// returns a string which is added to the listbox to give information about the event which was added to the event list
        /// </summary>
        /// <returns></returns>
        public override string EventDescription()
        {
            return $"Pause while colour {SearchColour} exists in area {ColourSearchArea} on {EventProcess.WindowTitle} window";
        }
    }
}
