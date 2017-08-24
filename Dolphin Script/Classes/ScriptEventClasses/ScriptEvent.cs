using System;
using System.Collections.Generic;

using static DolphinScript.Lib.Backend.WinAPI;
using static DolphinScript.Lib.Backend.WindowControl;

namespace DolphinScript.Lib.ScriptEventClasses
{
    [Serializable]
    abstract class ScriptEvent
    {
        // private window variables
        //
        private RECT windowRECT;
        private IntPtr windowHandle;

        // mandatory override methods
        //
        public abstract void DoEvent();
        public abstract string GetEventListBoxString();

        // gets the window handle by title (because handle ID can change often)
        //
        public IntPtr WindowToClickHandle
        {
            get
            {
                windowHandle = FindWindow(null, WindowToClickTitle);
                return windowHandle;
            }
            set { windowHandle = value; }
        }

        // gets the window location (because the window can move/scale often)
        //
        public RECT WindowToClickLocation
        {
            get
            {
                GetWindowRect(WindowToClickHandle, ref windowRECT);
                return windowRECT;
            }
            set { windowRECT = value; }
        }

        // gets/sets the title of the window, if the name of the window changes then the event will break
        // window title can change but it's the most consistent window feature we can use to get the handle and location of the window
        //
        public string WindowToClickTitle { get; set; }

        // fixed click position
        public POINT CoordsToMoveTo { get; set; }

        // area cick
        public RECT ClickArea { get; set; }
        
        // click event
        public VirtualMouseStates MouseButton { get; set; }

        // keyboard event
        public string KeyboardKeys { get; set; }

        // delay event
        public double DelayDuration { get; set; }
        public double DelayMinimum { get; set; }
        public double DelayMaximum { get; set; }

        // area colour search
        public int SearchColour { get; set; }
        public List<int> SearchColours { get; set; }
        public RECT ColourSearchArea { get; set; }
        public bool ColourWasFound { get; set; }

        // event group
        public List<ScriptEvent> EventsInGroup { get; set; }
        public bool IsPartOfGroup { get; set; }
        public int GroupID { get; set; }
        public int NumberOfCycles { get; set; }

        /// <summary>
        /// Script Event Constructor - sets default values for properties
        /// </summary>
        public ScriptEvent()
        {
            WindowToClickHandle = IntPtr.Zero;
            WindowToClickLocation = new RECT();
            WindowToClickTitle = "NoWindow";
            CoordsToMoveTo = new POINT();
            ClickArea = new RECT();
            MouseButton = VirtualMouseStates.None;
            KeyboardKeys = "NoKey";
            DelayDuration = -1.0;
            DelayMinimum = -1.0;
            DelayMaximum = -1.0;
            SearchColour = -1;
            SearchColours = new List<int>();
            ColourSearchArea = new RECT();
            ColourWasFound = false;
            EventsInGroup = new List<ScriptEvent>();
            IsPartOfGroup = false;
            GroupID = -1;
            NumberOfCycles = -1;
        }
    }
}
