using System;
using static DolphinScript.Lib.Backend.Common;
using static DolphinScript.Lib.Backend.WinAPI;
using static DolphinScript.Lib.Backend.PointReturns;

using static DolphinScript.Lib.ScriptEventClasses.MouseMove;

namespace DolphinScript.Lib.ScriptEventClasses
{
    [Serializable]
    class MouseMoveToArea : ScriptEvent
    {
        static public void MoveMouseToArea(RECT ClickArea)
        {
            POINT EndPoint = GetRandomPointInArea(ClickArea);

            MoveMouse(EndPoint); // move mouse to picked pixel
        }

        /// <summary>
        /// main overriden method used to perform this script event
        /// </summary>
        public override void DoEvent()
        {
            Status = $"Mouse move to random point in area: {ClickArea.PrintArea()}.";

            MoveMouseToArea(ClickArea);
        }

        public override string GetEventListBoxString()
        {
            if (GroupID == -1)
                return "Move mouse to random point in area " + ClickArea.PrintArea() + ".";
            else
                return "[Group " + GroupID + " Repeat x" + NumberOfCycles + "] Move mouse to random point in area " + ClickArea.PrintArea() + ".";
        }
    }
}
