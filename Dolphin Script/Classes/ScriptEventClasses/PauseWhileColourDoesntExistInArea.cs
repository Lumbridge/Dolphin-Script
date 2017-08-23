using System;
using System.Threading;

using static DolphinScript.Lib.Backend.ColourEvent;
using static DolphinScript.Lib.Backend.GlobalVariables;
using static DolphinScript.Lib.Backend.WinAPI;

namespace DolphinScript.Lib.ScriptEventClasses
{
    [Serializable]
    class PauseWhileColourDoesntExistInArea : ScriptEvent
    {
        public PauseWhileColourDoesntExistInArea()
        {
            EventType = Event.Pause_While_Colour_Doesnt_Exist_In_Area;
        }

        public override void DoEvent()
        {
            while (!ColourExistsInArea(ColourSearchArea, SearchColour))
            {
                if (GetAsyncKeyState(VirtualKeyStates.VK_F5) < 0)
                {
                    IsRunning = false;
                    return;
                }

                Thread.Sleep(TimeSpan.FromSeconds(1.0));
            }
        }

        public override string GetEventListBoxString()
        {
            if (GroupID == -1)
                return "Pause while colour " + SearchColour + " doesn't exist in area " + ColourSearchArea.PrintArea() + ".";
            else
                return "[Group " + GroupID + " Repeat x" + NumberOfCycles + "] Pause while colour " + SearchColour + " doesn't exist in area " + ColourSearchArea.PrintArea() + ".";
        }
    }
}
