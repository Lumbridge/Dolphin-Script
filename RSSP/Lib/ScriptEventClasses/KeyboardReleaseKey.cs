using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RASL.Lib.ScriptEventClasses
{
    class KeyboardReleaseKey : ScriptEvent
    {
        public KeyboardReleaseKey()
        {
            EventType = Event.Keyboard_ReleaseKey;
        }
    }
}
