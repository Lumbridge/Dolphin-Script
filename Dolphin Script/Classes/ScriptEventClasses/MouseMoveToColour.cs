using System;
using static DolphinScript.Classes.Backend.Common;
using static DolphinScript.Classes.Backend.WinApi;
using static DolphinScript.Classes.Backend.ColourEvent;
using static DolphinScript.Classes.Backend.RandomNumber;
using static DolphinScript.Classes.Backend.PointReturns;
using static DolphinScript.Classes.ScriptEventClasses.MouseMove;

namespace DolphinScript.Classes.ScriptEventClasses
{
    [Serializable]
    public class MouseMoveToColour : ScriptEvent
    {
        /// <summary>
        /// Moves mouse to a colour in a given search area
        /// </summary>
        /// <param name="searchArea"></param>
        /// <param name="searchColour"></param>
        /// <returns></returns>
        public static void MoveMouseToColour(Rect searchArea, int searchColour)
        {
            var temp = GetMatchingPixelList(searchArea, searchColour); // add matching colour pixels to list
            var pos = GetCursorPosition();

            while (GetColorAt(pos).ToArgb() != searchColour)
            {
                if (temp.Count > 0)
                {
                    temp = GetMatchingPixelList(searchArea, searchColour); // add matching colour pixels to list

                    var endPoint = temp[GetRandomNumber(0, temp.Count - 1)];

                    MoveMouse(endPoint); // move mouse to picked pixel

                    GetCursorPos(out pos);
                }
                else
                    break;
            }
        }

        /// <summary>
        /// main overriden method used to perform this script event
        /// </summary>
        public override void Invoke()
        {
            // update the status label on the main form
            //
            Status = $"Mouse move to colour: {SearchColour} in area: {ColourSearchArea.PrintArea()}.";

            // perform the mouse move method
            //
            MoveMouseToColour(ColourSearchArea, SearchColour);
        }

        /// <summary>
        /// returns a string which is added to the listbox to give information about the event which was added to the event list
        /// </summary>
        /// <returns></returns>
        public override string GetEventListBoxString()
        {
            if (GroupId == -1)
                return "Move mouse to random pixel of colour " + SearchColour + " in area " + ColourSearchArea.PrintArea() + ".";
            return "[Group " + GroupId + " Repeat x" + NumberOfCycles + "] Move mouse to random pixel of colour " + SearchColour + " in area " + ColourSearchArea.PrintArea() + ".";
        }
    }
}
