using System;

using static DolphinScript.Lib.Backend.Common;
using static DolphinScript.Lib.Backend.WinAPI;
using static DolphinScript.Lib.Backend.PointReturns;
using static DolphinScript.Lib.Backend.RandomNumber;
using static DolphinScript.Lib.Backend.WindowControl;
using static DolphinScript.Lib.ScriptEventClasses.MouseMoveToColour;

namespace DolphinScript.Lib.ScriptEventClasses
{
    [Serializable]
    class MouseMoveToMultiColourOnWindow : ScriptEvent
    {
        /// <summary>
        /// main overriden method used to perform this script event
        /// </summary>
        public override void DoEvent()
        {
            Status = $"Mouse move to any colour in list of selected colours on window: {WindowToClickTitle}.";

            BringEventWindowToFront(this);

            // don't override original click area or it will cause the mouse position to incrememnt every time this method is called
            //
            int newSearchColour = SearchColours[GetRandomNumber(0, SearchColours.Count - 1)];
            RECT newClickArea = GetClickAreaPositionOnWindow(WindowToClickHandle, ClickArea);
            
            MoveMouseToColour(newClickArea, newSearchColour);
        }

        public override string GetEventListBoxString()
        {
            if (GroupID == -1)
                return "Move mouse to random pixel matching colour " + SearchColours.ToString() + " in area " + ClickArea.PrintArea() + " on " + WindowToClickTitle + " window.";
            else
                return "[Group " + GroupID + " Repeat x" + NumberOfCycles + "] Move mouse to random pixel matching colour " + SearchColour + " in area " + ClickArea.PrintArea() + " on " + WindowToClickTitle + " window.";
        }
    }
}
