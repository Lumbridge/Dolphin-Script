using System;
using System.Threading.Tasks;

using static DolphinScript.Lib.Backend.ColourEvent;
using static DolphinScript.Lib.Backend.GlobalVariables;
using static DolphinScript.Lib.Backend.WinAPI;

namespace DolphinScript.Lib.ScriptEventClasses
{
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
                    Write("Status: Idle");
                    return;
                }

                Write("Search colour not found in search area, Idling for 1 second...");

                Task.WaitAll(Task.Delay(TimeSpan.FromSeconds(1.0)));
            }

            base.DoEvent();
        }

        public override string GetEventListBoxString()
        {
            if (GroupID == -1)
                return "Pause while colour " + SearchColour + " doesn't exist in area " + ColourSearchArea.PrintArea() + ".";
            else
                return "[Group " + GroupID + "] Pause while colour " + SearchColour + " doesn't exist in area " + ColourSearchArea.PrintArea() + ".";
        }
    }
}
