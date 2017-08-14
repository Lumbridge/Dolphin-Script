using System.Collections.Generic;

using static DolphinScript.Lib.Backend.WinAPI;
using static DolphinScript.Lib.Backend.ColourEvent;
using static DolphinScript.Lib.Backend.RandomNumber;
using static DolphinScript.Lib.Backend.PointReturns;
using static DolphinScript.Lib.ScriptEventClasses.MouseMove;

namespace DolphinScript.Lib.ScriptEventClasses
{
    class MouseMoveToColour : ScriptEvent
    {
        public MouseMoveToColour()
        {
            EventType = Event.Mouse_Move_To_Colour;
        }

        /// <summary>
        /// Moves mouse to a colour in a given search area
        /// </summary>
        /// <param name="SearchArea"></param>
        /// <param name="SearchColour"></param>
        /// <returns></returns>
        static public void MoveMouseToColour(RECT SearchArea, int SearchColour)
        {
            List<POINT> temp = GetMatchingPixelList(SearchArea, SearchColour); // add matching colour pixels to list
            POINT EndPoint;
            POINT pos = GetCursorPosition();

            while (GetColorAt(pos.X, pos.Y).ToArgb() != SearchColour)
            {
                if (temp.Count > 0)
                {
                    temp = GetMatchingPixelList(SearchArea, SearchColour); // add matching colour pixels to list

                    EndPoint = temp[GetRandomNumber(0, temp.Count - 1)]; // pick random matching pixel to click

                    MoveMouse(EndPoint); // move mouse to picked pixel

                    GetCursorPos(out pos);
                }
                else
                    break;
            }
        }

        public override void DoEvent()
        {
            MoveMouseToColour(ColourSearchArea, SearchColour);

            base.DoEvent();
        }

        public override string GetEventListBoxString()
        {
            if (GroupID == -1)
                return "Move mouse to random pixel of colour " + SearchColour + " in area " + ColourSearchArea.PrintArea() + ".";
            else
                return "[Group " + GroupID + "] Move mouse to random pixel of colour " + SearchColour + " in area " + ColourSearchArea.PrintArea() + ".";
        }
    }
}
