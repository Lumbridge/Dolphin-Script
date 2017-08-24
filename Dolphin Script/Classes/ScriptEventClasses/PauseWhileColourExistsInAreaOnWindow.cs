using System;
using System.Threading;

using static DolphinScript.Lib.Backend.WinAPI;
using static DolphinScript.Lib.Backend.ColourEvent;
using static DolphinScript.Lib.Backend.PointReturns;
using static DolphinScript.Lib.Backend.WindowControl;
using static DolphinScript.Lib.Backend.Common;

namespace DolphinScript.Lib.ScriptEventClasses
{
    [Serializable]
    class PauseWhileColourExistsInAreaOnWindow : ScriptEvent
    {
        public PauseWhileColourExistsInAreaOnWindow()
        {
            EventType = Event.Pause_While_Colour_Exists_In_Area_On_Window;
        }

        public override void DoEvent()
        {
            // don't override original click area or it will cause the mouse position to incrememnt every time this method is called
            RECT NewSearchArea = GetClickAreaPositionOnWindow(WindowToClickHandle, ClickArea);

            while (ColourExistsInArea(NewSearchArea, SearchColour))
            {
                if (GetForegroundWindow() != WindowToClickHandle || GetActiveWindowTitle() != WindowToClickTitle)
                {
                    // un-minimises window
                    //
                    ShowWindowAsync(WindowToClickHandle, SW_SHOWNORMAL);

                    // sets window to front
                    //
                    SetForegroundWindow(WindowToClickHandle);

                    // small delay to prevent click area errors
                    //
                    Thread.Sleep(100);
                }

                Status = $"Search colour found in search area, idling for {ReSearchPause} seconds.";

                Thread.Sleep(TimeSpan.FromSeconds(ReSearchPause));

                // update the search area incase the window has moved
                //
                NewSearchArea = GetClickAreaPositionOnWindow(WindowToClickHandle, ClickArea);
            }
        }

        public override string GetEventListBoxString()
        {
            if (GroupID == -1)
                return "Pause while colour " + SearchColour + " exists in area " + ColourSearchArea.PrintArea() + " on " + WindowTitle + " window.";
            else
                return "[Group " + GroupID + " Repeat x" + NumberOfCycles + "] Pause while colour " + SearchColour + " exists in area " + ColourSearchArea.PrintArea() + " on " + WindowTitle + " window.";

        }
    }
}
