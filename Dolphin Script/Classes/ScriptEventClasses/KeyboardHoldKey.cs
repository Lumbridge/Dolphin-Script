using System;

namespace DolphinScript.Lib.ScriptEventClasses
{
    class KeyboardHoldKey : ScriptEvent
    {
        public KeyboardHoldKey()
        {
            EventType = Event.Keyboard_HoldKey;
        }

        public override void DoEvent()
        {
            throw new NotImplementedException();
        }

        public override string GetEventListBoxString()
        {
            throw new NotImplementedException();
        }
    }
}
