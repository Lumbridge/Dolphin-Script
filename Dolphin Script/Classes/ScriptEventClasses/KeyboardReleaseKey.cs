﻿using System;
using static DolphinScript.Classes.Backend.Common;

namespace DolphinScript.Classes.ScriptEventClasses
{
    [Serializable]
    class KeyboardReleaseKey : ScriptEvent
    {
        public override void DoEvent()
        {
            // update the status label on the main form
            //
            Status = "";

            throw new NotImplementedException();
        }

        public override string GetEventListBoxString()
        {
            throw new NotImplementedException();
        }
    }
}
