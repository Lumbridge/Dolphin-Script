using static RASL.Lib.Backend.GlobalVariables;
using static RASL.Lib.Backend.WindowControl;

namespace RASL.Lib.ScriptEventClasses
{
    class MoveWindowToFront : ScriptEvent
    {
        public MoveWindowToFront()
        {
            EventType = Event.Move_Window_To_Front;
        }

        public override void DoEvent()
        {
            SetWindowTopMostIfExists(WindowClass, WindowTitle);
            base.DoEvent();
        }

        public override string GetEventListBoxString()
        {
            if (GroupID == -1)
                return "Move " + WindowTitle + " window to front.";
            else
                return "[Group " + GroupID + "] Move " + WindowTitle + " window to front.";
        }
    }
}
