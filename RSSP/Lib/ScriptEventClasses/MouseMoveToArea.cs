using static RASL.Lib.Backend.WinAPI;
using static RASL.Lib.Backend.PointReturns;

using static RASL.Lib.ScriptEventClasses.MouseMove;

namespace RASL.Lib.ScriptEventClasses
{
    class MouseMoveToArea : ScriptEvent
    {
        public MouseMoveToArea()
        {
            EventType = Event.Mouse_Move_To_Area;
        }

        static public void MoveMouseToArea(RECT ClickArea)
        {
            POINT EndPoint = GetRandomPointInArea(ClickArea);

            MoveMouse(EndPoint); // move mouse to picked pixel
        }

        public override void DoEvent()
        {
            MoveMouseToArea(ClickArea);

            base.DoEvent();
        }

        public override string GetEventListBoxString()
        {
            if (GroupID == -1)
                return "Move mouse to random point in area " + ClickArea.PrintArea() + ".";
            else
                return "[Group " + GroupID + "] Move mouse to random point in area " + ClickArea.PrintArea() + ".";
        }
    }
}
