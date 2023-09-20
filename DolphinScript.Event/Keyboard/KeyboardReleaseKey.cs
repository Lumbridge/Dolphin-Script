using System;
using DolphinScript.Core.Classes;

namespace DolphinScript.Event.Keyboard
{
    [Serializable]
    public class KeyboardReleaseKey : ScriptEvent
    {
        public override void Invoke()
        {
            // update the status label on the main form
            //
            _scriptState.Status = "";

            throw new NotImplementedException();
        }

        public override string GetEventListBoxString()
        {
            throw new NotImplementedException();
        }
    }
}
