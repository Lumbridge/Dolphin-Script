using DolphinScript.Core.Classes;
using DolphinScript.Core.Constants;
using DolphinScript.Core.Events.BaseEvents;
using DolphinScript.Core.Interfaces;
using System;

namespace DolphinScript.Core.Events.Mouse
{
    [Serializable]
    public class MouseMoveToColour : MouseMoveEvent
    {
        public MouseMoveToColour() { }

        public MouseMoveToColour(IMouseMovementService mouseMovementService, IPointService pointService, IWindowControlService windowControlService, IRandomService randomService, IColourService colourService) : base(mouseMovementService, pointService, windowControlService, randomService, colourService)
        {
            EventType = ScriptEventConstants.EventType.MouseMoveToColour;
        }

        public override void Setup()
        {
            SearchColour = SearchColours[RandomService.GetRandomNumber(0, SearchColours.Count - 1)];
            var temp = ColourService.GetMatchingPixelList(ColourSearchArea, SearchColour);
            CoordsToMoveTo = temp[RandomService.GetRandomNumber(0, temp.Count - 1)];
            ScriptState.CurrentAction = $"Mouse move to colour {SearchColourHex} at coordinate: {CoordsToMoveTo}";
        }

        /// <summary>
        /// returns a string which is added to the listbox to give information about the event which was added to the event list
        /// </summary>
        /// <returns></returns>
        public override string EventDescription()
        {
            return $"Mouse move to any colour in list of selected colours in area: {ColourSearchArea}";
        }
    }
}
