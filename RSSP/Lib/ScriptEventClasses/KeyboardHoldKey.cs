using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RASL.Lib.ScriptEventClasses
{
    class KeyboardHoldKey : ScriptEvent
    {
        public KeyboardHoldKey()
        {
            EventType = Event.Keyboard_HoldKey;
        }
    }
}
