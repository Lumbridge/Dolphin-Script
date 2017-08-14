using System.Threading.Tasks;

using static RASL.Lib.Backend.WinAPI;
using static RASL.Lib.Backend.PointReturns;
using static RASL.Lib.Backend.RandomNumber;
using static RASL.Lib.Backend.WindowControl;
using static RASL.Lib.ScriptEventClasses.MouseMoveToColour;

namespace RASL.Lib.ScriptEventClasses
{
    class MouseMoveToMultiColourOnWindow : ScriptEvent
    {
        public MouseMoveToMultiColourOnWindow()
        {
            EventType = Event.Mouse_Move_To_Multi_Colour_On_Window;
        }

        public override void DoEvent()
        {
            // make sure window is not minimised
            while (GetForegroundWindow() != WindowToClickHandle || GetActiveWindowTitle() != WindowToClickTitle)
            {
                // un-minimises window
                ShowWindowAsync(WindowToClickHandle, SW_SHOWNORMAL);
                // sets window to front
                SetForegroundWindow(WindowToClickHandle);
                // small delay to prevent click area errors
                Task.WaitAll(Task.Delay(1));
            }

            // don't override original click area or it will cause the mouse position to incrememnt every time this method is called
            int newSearchColour = SearchColours[GetRandomNumber(0, SearchColours.Count - 1)];
            RECT newClickArea = GetClickAreaPositionOnWindow(WindowToClickHandle, ClickArea);
            
            MoveMouseToColour(newClickArea, newSearchColour);

            base.DoEvent();
        }

        public override string GetEventListBoxString()
        {
            if (GroupID == -1)
                return "Move mouse to random pixel matching colour " + SearchColours.ToString() + " in area " + ClickArea.PrintArea() + " on " + WindowToClickTitle + " window.";
            else
                return "[Group " + GroupID + "] Move mouse to random pixel matching colour " + SearchColour + " in area " + ClickArea.PrintArea() + " on " + WindowToClickTitle + " window.";
        }
    }
}
