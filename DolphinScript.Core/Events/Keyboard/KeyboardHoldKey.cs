using System;
using DolphinScript.Core.Classes;
using DolphinScript.Core.Events.BaseEvents;

namespace DolphinScript.Core.Events.Keyboard
{
    [Serializable]
    public class KeyboardHoldKey : ScriptEvent
    {
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
