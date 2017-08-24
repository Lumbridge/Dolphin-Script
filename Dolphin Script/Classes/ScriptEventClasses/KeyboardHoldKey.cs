using System;

using static DolphinScript.Lib.Backend.Common;

namespace DolphinScript.Lib.ScriptEventClasses
{
    [Serializable]
    class KeyboardHoldKey : ScriptEvent
    {
        public override void DoEvent()
        {
            Status = $"";

            throw new NotImplementedException();
        }

        public override string GetEventListBoxString()
        {
            throw new NotImplementedException();
        }
    }
}
