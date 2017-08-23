using System;

namespace DolphinScript.Lib.ScriptEventClasses
{
    [Serializable]
    class KeyboardReleaseKey : ScriptEvent
    {
        public KeyboardReleaseKey()
        {
            EventType = Event.Keyboard_ReleaseKey;
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
