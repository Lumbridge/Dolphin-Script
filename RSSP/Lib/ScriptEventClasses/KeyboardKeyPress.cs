using System.Windows.Forms;

namespace RASL.Lib.ScriptEventClasses
{
    class KeyboardKeyPress : ScriptEvent
    {
        public KeyboardKeyPress()
        {
            EventType = Event.Keyboard_Keypress;
        }

        public override void DoEvent()
        {
            SendKeys.SendWait(KeyboardKey);
            base.DoEvent();
        }

        public override string GetEventListBoxString()
        {
            if (GroupID == -1)
                return "Keypress (Key(s): " + KeyboardKey + ")";
            else
                return "[Group " + GroupID + "] Keypress (Key: " + KeyboardKey + ").";
        }
    }
}