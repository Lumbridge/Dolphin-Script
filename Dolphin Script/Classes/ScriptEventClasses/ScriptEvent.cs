using System;
using System.Collections.Generic;

using static DolphinScript.Lib.Backend.WinAPI;
using static DolphinScript.Lib.Backend.GlobalVariables;

namespace DolphinScript.Lib.ScriptEventClasses
{
    class ScriptEvent
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

        public virtual void DoEvent()
        {
            Write("Completed " + EventType + " event.");
        }

        public virtual string GetEventListBoxString()
        {
            return string.Empty;
        }

        // generic event variable
        public Event EventType { get; set; }

        // For clicking a point/area on a specific window
        public IntPtr WindowToClickHandle { get; set; }
        public RECT WindowToClickLocation { get; set; }
        public string WindowToClickTitle { get; set; }

        // fixed click position
        public POINT DestinationPoint { get; set; }

        // area cick
        public RECT ClickArea { get; set; }
        
        // click event
        public VirtualMouseStates MouseButton { get; set; }

        // keyboard event
        public string KeyboardKey { get; set; }

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
            DestinationPoint = new POINT();
            ClickArea = new RECT();
            MouseButton = VirtualMouseStates.None;
            KeyboardKey = "NoKey";
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
        
        public string SaveConfigString()
        {
            return ""
                + EventType + Environment.NewLine
                + WindowToClickHandle + Environment.NewLine
                + WindowToClickLocation.GetConfigString() + Environment.NewLine
                + WindowToClickTitle + Environment.NewLine
                + DestinationPoint.GetConfigString() + Environment.NewLine
                + ClickArea.GetConfigString() + Environment.NewLine
                + MouseButton + Environment.NewLine
                + KeyboardKey + Environment.NewLine
                + DelayDuration + Environment.NewLine
                + DelayMinimum + Environment.NewLine
                + DelayMaximum + Environment.NewLine
                + SearchColour + Environment.NewLine
                + ColourSearchArea.GetConfigString() + Environment.NewLine
                + ColourWasFound + Environment.NewLine
                + EventsInGroup + Environment.NewLine
                + IsPartOfGroup + Environment.NewLine
                + GroupID + Environment.NewLine
                + NumberOfCycles + Environment.NewLine;
        }

        //public string SaveConfigString()
        //{
        //    return ""
        //        + EventType + Environment.NewLine
        //        + WindowToClickHandle + Environment.NewLine
        //        + WindowToClickLocation.GetConfigString() + Environment.NewLine
        //        + WindowToClickTitle + Environment.NewLine
        //        + DestinationPoint.GetConfigString() + Environment.NewLine
        //        + ClickArea.GetConfigString() + Environment.NewLine
        //        + MouseButton + Environment.NewLine
        //        + KeyboardKey + Environment.NewLine
        //        + DelayDuration + Environment.NewLine
        //        + DelayMinimum + Environment.NewLine
        //        + DelayMaximum + Environment.NewLine
        //        + SearchColour + Environment.NewLine
        //        + SearchColours + Environment.NewLine
        //        + SearchColoursListToString(SearchColours) + Environment.NewLine
        //        + ColourWasFound + Environment.NewLine
        //        + EventsInGroup + Environment.NewLine
        //        + IsPartOfGroup + Environment.NewLine
        //        + GroupID + Environment.NewLine
        //        + NumberOfCycles + Environment.NewLine;
        //}

        static public string SearchColoursListToString(List<int> SearchColours)
        {
            string temp = string.Empty;
            
            for(int i = 0; i < SearchColours.Count; i++)
                temp += SearchColours[i].ToString() + Alphabet[i];

            return temp;
        }

        static public List<int> ConfigStringToList(string ConfigString)
        {
            int Counter = 0;
            List<int> searchColours = new List<int>();

            foreach (var c in ConfigString)
                if (Alphabet.Contains(c.ToString()))
                    Counter++;

            for (int i = 0; i < Counter; i++)
                searchColours.Add(int.Parse(GetSubstringByString(Alphabet[i], Alphabet[i + 1], ConfigString)));

            return searchColours;
        }
    }
}
