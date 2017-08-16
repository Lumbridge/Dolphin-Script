using System;
using System.Collections.Generic;

using static DolphinScript.Lib.Backend.WinAPI;
using static DolphinScript.Lib.Backend.GlobalVariables;

namespace DolphinScript.Lib.ScriptEventClasses
{
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
        
        /// <summary>
        /// returns the script event in a string so it can be saved to a file
        /// </summary>
        /// <returns></returns>
        public string SaveConfigString()
        {
            return ""
                + "EVENT_TYPE="                     + EventType + Environment.NewLine
                + "WINDOW_TO_CLICK_HANDLE="         + WindowToClickHandle + Environment.NewLine
                + "WINDOW_TO_CLICK_LOCATION="       + WindowToClickLocation.GetConfigString() + Environment.NewLine
                + "WINDOW_TO_CLICK_TITLE="          + WindowToClickTitle + Environment.NewLine
                + "COORDS_TO_MOVE_TO="              + CoordsToMoveTo.GetConfigString() + Environment.NewLine
                + "CLICK_AREA="                     + ClickArea.GetConfigString() + Environment.NewLine
                + "MOUSE_BUTTON="                   + MouseButton + Environment.NewLine
                + "KEYBOARD_KEYS="                  + KeyboardKeys + Environment.NewLine
                + "DELAY_DURATION="                 + DelayDuration + Environment.NewLine
                + "DELAY_MINIMUM="                  + DelayMinimum + Environment.NewLine
                + "DELAY_MAXIMUM="                  + DelayMaximum + Environment.NewLine
                + "SEARCH_COLOUR="                  + SearchColour + Environment.NewLine
                + "MULTI_SEARCH_COLOURS="           + SearchColoursListToString(SearchColours) + Environment.NewLine
                + "COLOUR_SEARCH_AREA="             + ColourSearchArea.GetConfigString() + Environment.NewLine
                + "COLOUR_WAS_FOUND="               + ColourWasFound + Environment.NewLine
                + "EVENTS_IN_GROUP="                + EventsInGroup + Environment.NewLine
                + "IS_PART_OF_GROUP="               + IsPartOfGroup + Environment.NewLine
                + "GROUP_ID="                       + GroupID + Environment.NewLine
                + "NUMBER_OF_GROUP_REPEAT_CYCLES="  + NumberOfCycles + Environment.NewLine;
        }

        /// <summary>
        /// returns a string of search colours used in the event so it can be saved to a file
        /// </summary>
        /// <param name="SearchColours"></param>
        /// <returns></returns>
        static public string SearchColoursListToString(List<int> SearchColours)
        {
            // create a string to store the search colours string
            //
            string temp = "NoColours";

            // checks that there are colours in the list
            //
            if (SearchColours.Count > 0)
            {
                temp = string.Empty;

                // add letter A to the beginning of the config string
                //
                temp += "A";

                // loops through each colour and adds a letter after it
                // this way we can access each colour by getting the string between the relevant letters
                // for example, if we want the first colour we would get the string between letters A and B
                //
                for (int i = 0; i < SearchColours.Count; i++)
                {
                    temp += SearchColours[i].ToString() + Alphabet[i + 1];
                }
            }

            // return the string
            //
            return temp;
        }

        /// <summary>
        /// gets all of the colours we saved to file and changes them back to an integer list
        /// </summary>
        /// <param name="ConfigString"></param>
        /// <returns></returns>
        static public List<int> ConfigStringToList(string ConfigString)
        {
            // create a counter variable used to count the number of letters in the string
            //
            int Counter = 0;
            
            // create a list to store all of the colours we find
            //
            List<int> searchColours = new List<int>();

            // loop through each character in the string and check if it's in the alphabet list we made
            // if it is then we increment the counter variable
            //
            foreach (char c in ConfigString)
                if (Alphabet.Contains(c.ToString()))
                    Counter++;

            Counter -= 1;

            // collects all colour values by looking between each letter pair e.g. A colour1 B colour2 C
            //
            if(Counter > 0)
                for (int i = 0; i < Counter; i++)
                    searchColours.Add(int.Parse(GetSubstringByString(Alphabet[i], Alphabet[i + 1], ConfigString)));

            // return the colour integer list
            //
            return searchColours;
        }
    }
}
