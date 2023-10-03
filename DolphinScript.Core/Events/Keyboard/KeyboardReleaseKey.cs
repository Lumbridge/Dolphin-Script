using System;
using DolphinScript.Core.Classes;
using DolphinScript.Core.Constants;
using DolphinScript.Core.Events.BaseEvents;

namespace DolphinScript.Core.Events.Keyboard
{
    [Serializable]
    public class KeyboardReleaseKey : ScriptEvent
    {
        public KeyboardReleaseKey()
        {
            EventType = ScriptEventConstants.EventType.KeyboardReleaseKey;
        }

        public override void Setup()
        {
            ScriptState.CurrentAction = "";
        }

        public override void Execute()
        {
            throw new NotImplementedException();
        }

        public override string EventDescription()
        {
            throw new NotImplementedException();
        }
    }
}
