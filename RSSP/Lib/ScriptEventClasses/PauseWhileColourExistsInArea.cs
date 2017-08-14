﻿using System;
using System.Threading;

using static RASL.Lib.Backend.ColourEvent;
using static RASL.Lib.Backend.GlobalVariables;
using static RASL.Lib.Backend.WinAPI;

namespace RASL.Lib.ScriptEventClasses
{
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
                if (GetAsyncKeyState(VirtualKeyStates.VK_F5) < 0)
                {
                    IsRunning = false;
                    Write("Status: Idle");
                    return;
                }

                Write("Colour found in search area, Idling for 1 second...");

                Thread.Sleep(TimeSpan.FromSeconds(1.0));
            }

            base.DoEvent();
        }

        public override string GetEventListBoxString()
        {
            if (GroupID == -1)
                return "Pause while colour " + SearchColour + " exists in area " + ColourSearchArea.PrintArea() + ".";
            else
                return "[Group " + GroupID + "] Pause while colour " + SearchColour + " exists in area " + ColourSearchArea.PrintArea() + ".";
        }
    }
}