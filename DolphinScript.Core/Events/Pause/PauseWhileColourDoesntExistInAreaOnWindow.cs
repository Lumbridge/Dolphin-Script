using System;
using System.Threading;
using DolphinScript.Core.Classes;
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
        }

        /// <summary>
        /// main overriden method used to perform this script event
        /// </summary>
        public override void Invoke()
        {
            var windowHandle = WindowControlService.GetWindowHandle(WindowTitle);

            // don't override original click area or it will cause the mouse position to increment every time this method is called
            var newSearchArea = PointService.GetClickAreaPositionOnWindow(windowHandle, ClickArea);

            // update the status label on the main form
            ScriptState.Status = $"Pause while colour: {SearchColour} not found in area: {newSearchArea.PrintArea()} on window: {WindowTitle}, waiting {ScriptState.SearchPause} seconds before re-searching.";

            ExecuteWhileLoop(() =>
            {
                // bring the window associated with this event to the front
                WindowControlService.BringWindowToFront(windowHandle);

                Thread.Sleep(TimeSpan.FromSeconds(ScriptState.SearchPause));

                // update the search area in case the window has moved
                newSearchArea = PointService.GetClickAreaPositionOnWindow(windowHandle, ClickArea);
            }, () => !ColourService.ColourExistsInArea(newSearchArea, SearchColour));
        }

        /// <summary>
        /// returns a string which is added to the listbox to give information about the event which was added to the event list
        /// </summary>
        /// <returns></returns>
        public override string GetEventListBoxString()
        {
            var str = $"Pause while colour {SearchColour} doesn't exist in area {ColourSearchArea.PrintArea()} on {WindowTitle} window.";

            if (IsPartOfGroup)
            {
                str = GroupEventBoxString + str;
            }
                
            return str;
        }
    }
}
