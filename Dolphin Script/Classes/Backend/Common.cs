using System;
using System.Collections.Generic;

using DolphinScript.Lib.ScriptEventClasses;
using static DolphinScript.Lib.Backend.WinAPI;

namespace DolphinScript.Lib.Backend
{
    class Common
    {
        private static string status;

        public static string WindowClass { get; set; }
        public static string WindowTitle { get; set; }

        public static string LastAction { get; set; }
        public static string Status
        {
            get { return status; }
            set
            {
                // when the status property is changed then we change the last action property also
                //
                LastAction = Status;

                // then override the current status
                //
                status = value;
            }
        }

        // the amount of time the script will pause before re-searching during some pause events
        //
        public static double ReSearchPause = 0.5;

        public static int                       MouseSpeed       = 15;
        public static List<List<ScriptEvent>>   AllGroups        = new List<List<ScriptEvent>>();
        public static List<ScriptEvent>         AllEvents        = new List<ScriptEvent>();
        public static bool                      IsRegistering    = false, 
                                                IsRunning        = false;

        public static void CheckForTerminationKey()
        {
            if (GetAsyncKeyState(VirtualKeyStates.VK_F5) < 0)
            {
                IsRunning = false;
                return;
            }
        }

        // list of special sendkey codes
        //
        public static List<string> SpecialKeys = new List<string>()
        {
            "+",
            "%",
            "{LEFT}",
            "{RIGHT}",
            "{UP}",
            "{DOWN}",
            "{BACKSPACE}",
            "{BREAK}",
            "{CAPSLOCK}",
            "{DELETE}",
            "{END}",
            "{ENTER}",
            "{ESC}",
            "{HELP}",
            "{HOME}",
            "{INSERT}",
            "{NUMLOCK}",
            "{PGDN}",
            "{PGUP}",
            "{PRTSC}",
            "{SCROLLLOCK}",
            "{TAB}",
            "{F1}",
            "{F2}",
            "{F3}",
            "{F4}",
            "{F5}",
            "{F6}",
            "{F7}",
            "{F8}",
            "{F9}",
            "{F10}",
            "{F11}",
            "{F12}",
            "{F13}",
            "{F14}",
            "{F15}",
            "{F16}",
            "{ADD}",
            "{SUBTRACT}",
            "{MULTIPLY}",
            "{DIVIDE}"
        };

        /// <summary>
        /// shortcut function to write to the console
        /// </summary>
        /// <param name="text"></param>
        public static void Write(string text)
        {
            Console.WriteLine("[{0}] {1}", DateTime.Now.ToShortTimeString(), text);
        }

        /// <summary>
        /// swaps position of two elements in a collection
        /// </summary>
        /// <param name="list"></param>
        /// <param name="indexA"></param>
        /// <param name="indexB"></param>
        public static void Swap(IList<ScriptEvent> list, int indexA, int indexB)
        {
            // store temp version of element object
            //
            ScriptEvent tmp = list[indexA];
            
            // move element at index b to the location of the element we stored
            //
            list[indexA] = list[indexB];

            // move the stored element to the location of index b
            //
            list[indexB] = tmp;
        }
    }
}
