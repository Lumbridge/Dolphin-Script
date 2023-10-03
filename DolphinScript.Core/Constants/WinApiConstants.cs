using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinScript.Core.Constants
{
    public class WinApiConstants
    {
        // normal/minimise/maximise window flags
        public static int SW_SHOWNORMAL = 1;
        public static int SW_SHOWMINIMIZED = 2;
        public static int SW_SHOWMAXIMIZED = 3;
        public static int SW_SHOW = 5;
        public static int SW_RESTORE = 9;

        // key pressed state
        public const int KeyPressed = 0x8000;
    }
}
