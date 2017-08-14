﻿using System;
using System.Threading.Tasks;

using static RASL.Lib.Backend.WinAPI;
using static RASL.Lib.Backend.ColourEvent;
using static RASL.Lib.Backend.PointReturns;
using static RASL.Lib.Backend.WindowControl;
using static RASL.Lib.Backend.GlobalVariables;

namespace RASL.Lib.ScriptEventClasses
{
    class PauseWhileColourDoesntExistInAreaOnWindow : ScriptEvent
    {
        public PauseWhileColourDoesntExistInAreaOnWindow()
        {
            EventType = Event.Pause_While_Colour_Doesnt_Exist_In_Area_On_Window;
        }

        public override void DoEvent()
        {
            // don't override original click area or it will cause the mouse position to incrememnt every time this method is called
            RECT NewSearchArea = GetClickAreaPositionOnWindow(WindowToClickHandle, ClickArea);

            while (!ColourExistsInArea(NewSearchArea, SearchColour))
            {
                if (GetAsyncKeyState(VirtualKeyStates.VK_F5) < 0)
                {
                    IsRunning = false;
                    Write("Status: Idle");
                    return;
                }

                if (GetForegroundWindow() != WindowToClickHandle || GetActiveWindowTitle() != WindowToClickTitle)
                {
                    // un-minimises window
                    ShowWindowAsync(WindowToClickHandle, SW_SHOWNORMAL);
                    // sets window to front
                    SetForegroundWindow(WindowToClickHandle);
                    // small delay to prevent click area errors
                    Task.WaitAll(Task.Delay(100));
                }

                Write("Search colour not found in search area, Idling for 1 second...");

                Task.WaitAll(Task.Delay(TimeSpan.FromSeconds(1.0)));

                NewSearchArea = GetClickAreaPositionOnWindow(WindowToClickHandle, ClickArea);
            }

            base.DoEvent();
        }

        public override string GetEventListBoxString()
        {
            if (GroupID == -1)
                return "Pause while colour " + SearchColour + " doesn't exist in area " + ColourSearchArea.PrintArea() + " on " + WindowTitle + " window.";
            else
                return "[Group " + GroupID + "] Pause while colour " + SearchColour + " doesn't exist in area " + ColourSearchArea.PrintArea() + " on " + WindowTitle + " window.";

        }
    }
}
