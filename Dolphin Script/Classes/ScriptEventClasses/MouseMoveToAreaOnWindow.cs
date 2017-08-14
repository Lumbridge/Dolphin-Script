using System.Threading.Tasks;

using static DolphinScript.Lib.Backend.WinAPI;
using static DolphinScript.Lib.Backend.PointReturns;
using static DolphinScript.Lib.Backend.WindowControl;

using static DolphinScript.Lib.ScriptEventClasses.MouseMove;

namespace DolphinScript.Lib.ScriptEventClasses
{
    class MouseMoveToAreaOnWindow : ScriptEvent
    {
        public MouseMoveToAreaOnWindow()
        {
            EventType = Event.Mouse_Move_To_Area_On_Window;
        }

        static public void MoveMouseToAreaOnWindow(RECT ClickArea)
        {
            POINT EndPoint = GetRandomPointInArea(ClickArea);

            MoveMouse(EndPoint); // move mouse to picked pixel
        }

        public override void DoEvent()
        {
            // make sure window is not minimised
            while (GetForegroundWindow() != WindowToClickHandle)
            {
                // un-minimises window
                ShowWindowAsync(WindowToClickHandle, SW_SHOWNORMAL);
                // sets window to front
                SetForegroundWindow(WindowToClickHandle);
                // small delay to prevent click area errors
                Task.WaitAll(Task.Delay(1));
            }

            // don't override original click area or it will cause the mouse position to incrememnt every time this method is called
            RECT NewClickArea = GetClickAreaPositionOnWindow(WindowToClickHandle, ClickArea);

            MoveMouseToAreaOnWindow(NewClickArea);

            base.DoEvent();
        }

        public override string GetEventListBoxString()
        {
            if (GroupID == -1)
                return "Move mouse to random point in area " + ClickArea.PrintArea() + " on " + GetWindowTitle(WindowToClickHandle) + " window.";
            else
                return "[Group " + GroupID + "] Move mouse to random point in area " + ClickArea.PrintArea() + " on " + GetWindowTitle(WindowToClickHandle) + " window.";
        }
    }
}
