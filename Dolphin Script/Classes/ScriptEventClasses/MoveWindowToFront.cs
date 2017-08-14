using static DolphinScript.Lib.Backend.GlobalVariables;
using static DolphinScript.Lib.Backend.WindowControl;

namespace DolphinScript.Lib.ScriptEventClasses
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
