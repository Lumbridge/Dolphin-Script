using System;
using System.Threading;

using static DolphinScript.Lib.Backend.ColourEvent;
using static DolphinScript.Lib.Backend.Common;
using static DolphinScript.Lib.Backend.WinAPI;

namespace DolphinScript.Lib.ScriptEventClasses
{
    [Serializable]
    class PauseWhileColourExistsInArea : ScriptEvent
    {
        public PauseWhileColourExistsInArea()
        {
            EventType = Event.Pause_While_Colour_Exists_In_Area;
        }

        public override void DoEvent()
        {
            while (ColourExistsInArea(ColourSearchArea, SearchColour))
            {
                Status = $"Colour found in area, idling for {ReSearchPause} seconds.";

                Thread.Sleep(TimeSpan.FromSeconds(ReSearchPause));
            }
        }

        public override string GetEventListBoxString()
        {
            if (GroupID == -1)
                return "Pause while colour " + SearchColour + " exists in area " + ColourSearchArea.PrintArea() + ".";
            else
                return "[Group " + GroupID + " Repeat x" + NumberOfCycles + "] Pause while colour " + SearchColour + " exists in area " + ColourSearchArea.PrintArea() + ".";
        }
    }
}
