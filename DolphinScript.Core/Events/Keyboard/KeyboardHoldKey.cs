using System;
using DolphinScript.Core.Classes;
using DolphinScript.Core.Events.BaseEvents;

namespace DolphinScript.Core.Events.Keyboard
{
    [Serializable]
    public class KeyboardHoldKey : ScriptEvent
    {
        public override void Invoke()
        {
            // update the status label on the main form
            //
            ScriptState.Status = "";

            throw new NotImplementedException();
        }

        public override string GetEventListBoxString()
        {
            throw new NotImplementedException();
        }
    }
}
