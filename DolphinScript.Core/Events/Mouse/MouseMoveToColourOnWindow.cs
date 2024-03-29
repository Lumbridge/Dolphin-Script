﻿using System;
using System.Linq;
using DolphinScript.Core.Classes;
using DolphinScript.Core.Constants;
using DolphinScript.Core.Events.BaseEvents;
using DolphinScript.Core.Interfaces;

namespace DolphinScript.Core.Events.Mouse
{
    [Serializable]
    public class MouseMoveToColourOnWindow : MouseMoveEvent
    {
        public MouseMoveToColourOnWindow() { }

        public MouseMoveToColourOnWindow(IMouseMovementService mouseMovementService, IPointService pointService, IWindowControlService windowControlService, IRandomService randomService, IColourService colourService) : base(mouseMovementService, pointService, windowControlService, randomService, colourService)
        {
            EventType = ScriptEventConstants.EventType.MouseMoveToColourOnWindow;
        }

        public override void Setup()
        {
            // don't override original click area or it will cause the mouse position to increment every time this method is called
            var newSearchArea = PointService.GetClickAreaPositionOnWindow(EventProcess.WindowHandle, ClickArea);

            // bring the window associated with this event to the front
            WindowControlService.BringWindowToFront(EventProcess.WindowHandle);

            SearchColour = SearchColours[RandomService.GetRandomNumber(0, SearchColours.Count - 1)];

            var matchingPixelList = ColourService.GetMatchingPixelList(ColourSearchArea, SearchColour);

            if (!matchingPixelList.Any())
            {
                ScriptState.CurrentAction = $"No pixels matching colour: {SearchColourHex} found in area {newSearchArea} on window: {EventProcess.WindowTitle}.";
                return;
            }

            CoordsToMoveTo = matchingPixelList[RandomService.GetRandomNumber(0, matchingPixelList.Count - 1)];

            ScriptState.CurrentAction = $"Mouse move to colour: {SearchColourHex} on window: {EventProcess.WindowTitle} in area {newSearchArea}.";
        }

        /// <summary>
        /// returns a string which is added to the listbox to give information about the event which was added to the event list
        /// </summary>
        /// <returns></returns>
        public override string EventDescription()
        {
            return $"Move mouse to a pixel matching colour {SearchColourHex} in area {ColourSearchArea} on {EventProcess.WindowTitle} window.";
        }
    }
}
