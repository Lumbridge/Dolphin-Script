using System;
using System.Collections.Generic;

using static DolphinScript.Lib.Backend.WinAPI;
using static DolphinScript.Lib.Backend.GlobalVariables;

namespace DolphinScript.Lib.ScriptEventClasses
{
    [Serializable]
    abstract class ScriptEvent
    {
        public enum Event
        {
            // movement
            Mouse_Move,
            Mouse_Move_To_Area,
            Mouse_Move_To_Point_On_Window,
            Mouse_Move_To_Area_On_Window,
            Mouse_Move_To_Colour,
            Mouse_Move_To_Colour_On_Window,
            Mouse_Move_To_Multi_Colour_On_Window,
            // left mouse clicks
            Mouse_Left_Click,
            Mouse_Left_Down,
            Mouse_Left_UP,
            // right mouse clicks
            Mouse_Right_Click,
            Mouse_Right_Down,
            Mouse_Right_Up,
            // middle mouse clicks
            Mouse_Middle_Click,
            Mouse_Middle_Down,
            Mouse_Middle_Up,
            // pauses/delays/idles
            Random_Pause_In_Range,
            Fixed_Pause,
            Pause_While_Colour_Exists_In_Area,
            Pause_While_Colour_Exists_In_Area_On_Window,
            Pause_While_Colour_Doesnt_Exist_In_Area,
            Pause_While_Colour_Doesnt_Exist_In_Area_On_Window,
            Pause_While_Window_Not_Found,
            // keyboard event
            Keyboard_Keypress,
            Keyboard_HoldKey,
            Keyboard_ReleaseKey,
            // antiban event
            Anti_ban,
            // window event
            Move_Window_To_Front,
            // default assignment state
            None
        }

        // mandatory override methods
        //
        public abstract void DoEvent();
        public abstract string GetEventListBoxString();

        // generic event variable
        public Event EventType { get; set; }

        // For clicking a point/area on a specific window
        public IntPtr WindowToClickHandle { get; set; }
        public RECT WindowToClickLocation { get; set; }
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

        public ScriptEvent()
        {
            EventType = Event.None;
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
