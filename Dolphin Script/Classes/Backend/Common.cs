using System;
using System.Collections;
using System.Collections.Generic;
using DolphinScript.Classes.ScriptEventClasses;
using static DolphinScript.Classes.Backend.WinApi;

namespace DolphinScript.Classes.Backend
{
    /// <summary>
    /// This is a general class to keep methods and variables which are required in multiple different classes.
    /// </summary>
    class Common
    {
        private static string _status;
        
        // public window properties used for 
        //
        public static string WindowClass { get; set; }
        public static string WindowTitle { get; set; }

        // this is a public string string which is used to update the last action label on the main form
        // it is here in the common class so all the sub events can update it
        //
        public static string LastAction { get; set; }

        // this is a public string which is used to update the status label on the main form
        // it is here in the common class so all sub events can update it
        //
        public static string Status
        {
            get { return _status; }
            set
            {
                // when the status property is changed then we change the last action property also
                //
                LastAction = Status;

                // then override the current status
                //
                _status = $"Status: {value}";
            }
        }

        // the amount of time the script will pause before re-searching during some pause events
        //
        public static double ReSearchPause = 0.5;

        // this is the speed the mouse will move at during mouse move events
        //
        public static int MinMouseSpeed = 10;
        public static int MaxMouseSpeed = 15;

        // this is a list of lists of scripts events which stores the grouped events
        //
        public static List<List<ScriptEvent>> AllGroups = new List<List<ScriptEvent>>();

        // this is a list of all events in the event listbox
        //
        public static List<ScriptEvent> AllEvents = new List<ScriptEvent>();

        // public booleans which will be used to flag when the user is registering an event
        // or running the script
        //
        public static bool IsRegistering, IsRunning;

        // list of special send key codes
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
        /// this method is used to determine if the user is pressing the DefaultStopCancelButton key to stop the script
        /// </summary>
        public static void CheckForTerminationKey()
        {
            // listen for the equals key
            //
            if (GetAsyncKeyState(Constants.DefaultStopCancelButton) < 0)
            {
                // set is running flag to false
                IsRunning = false;
            }
        }

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
        public static void Swap(IList list, int indexA, int indexB)
        {
            (list[indexA], list[indexB]) = (list[indexB], list[indexA]);
        }

        /// <summary>
        /// moves a list item to another position in the collection
        /// </summary>
        /// <param name="list"></param>
        /// <param name="startIndex"></param>
        /// <param name="shiftAmount"></param>
        public static void ShiftItem(IList list, int startIndex, int shiftAmount)
        {
            for(var i = startIndex; i < startIndex + shiftAmount; i++)
            {
                Swap(list, i, i + 1);
            }
        }

        /// <summary>
        /// moves a range of elements down a list
        /// </summary>
        /// <param name="list"></param>
        /// <param name="startIndex"></param>
        /// <param name="groupSize"></param>
        /// <param name="shiftAmount"></param>
        public static void ShiftRange(IList list, int startIndex, int groupSize, int shiftAmount)
        {
            for(var i = startIndex; i < shiftAmount; i++)
            {
                Swap(list, i, i + groupSize);
            }
        }
    }
}
