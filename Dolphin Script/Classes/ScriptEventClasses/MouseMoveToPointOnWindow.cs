using System.Threading.Tasks;

using static DolphinScript.Lib.Backend.WinAPI;
using static DolphinScript.Lib.Backend.PointReturns;
using static DolphinScript.Lib.Backend.WindowControl;

using static DolphinScript.Lib.ScriptEventClasses.MouseMove;

namespace DolphinScript.Lib.ScriptEventClasses
{
    class MouseMoveToPointOnWindow : ScriptEvent
    {
        public MouseMoveToPointOnWindow()
        {
            EventType = Event.Mouse_Move_To_Point_On_Window;
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

            POINT NewClickPoint = new POINT(
                GetWindowPosition(WindowToClickHandle).Left + PositionToMoveTo.X,
                GetWindowPosition(WindowToClickHandle).Top + PositionToMoveTo.Y);

            MoveMouse(NewClickPoint);
        }

        public override string GetEventListBoxString()
        {
            if (GroupID == -1)
                return "Move mouse to Point X: " + PositionToMoveTo.X + " Y: " + PositionToMoveTo.Y + " on " + WindowToClickTitle + " window.";
            else
                return "[Group " + GroupID + "] Move mouse to random point in area " + ClickArea.PrintArea() + " on " + WindowToClickTitle + " window.";
        }
    }
}
