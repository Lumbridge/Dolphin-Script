using DolphinScript.Core.WindowsApi;
using System;
using System.Collections.Generic;

namespace DolphinScript.Core.Constants
{
    public class MainFormConstants
    {
        public static int ZoomPreviewPx = 10;
        public static int ZoomPreviewSize = 100;
        public static int ZoomPreviewPositionCursorRelativeX = 40;
        public static int ZoomPreviewPositionCursorRelativeY = 40;

        public static int DefaultMinimumMouseSpeed = 40;
        public static int DefaultMaximumMouseSpeed = 60;

        public static string SelectingPointToClick => $"Selecting point to click... ({DefaultStopCancelButton} to finish).";
        public static string SelectingPointsToClick => $"Selecting points to click... ({DefaultStopCancelButton} to finish).";
        public static string SelectingAreaToClick => $"Selecting area to click... ({DefaultStopCancelButton} to finish).";
        public static string SelectingAreaToSearch => $"Selecting area to search... ({DefaultStopCancelButton} to finish).";
        public static string SelectingColourToSearchForInArea => $"Selecting colour to search for in area... ({DefaultStopCancelButton} to finish).";

        public static string DefaultFileName = $"UserScript_{DateTime.Now:s}.xml".Replace(":", string.Empty);

        public const string AboutString = "Created by Ryan Sainty @ https://github.com/Lumbridge";

        public const string StartScript = "Start";

        public const string ErrorMovingEvent = "Error moving event.";
        public const string NoEventsAdded = "No events have been added.";
        public const string OneGroupMaxError = "One or more selected events are already part of a group, events can only be part of one group.";
        public const string SelectMoreThanOneItemToMakeAGroup = "Select more than 1 item to create a group.";

        public static string ScriptRunning => $"Script Running ({DefaultStopCancelButton} to stop)...";

        public static CommonTypes.VirtualKeyStates DefaultStopCancelButton = CommonTypes.VirtualKeyStates.F5;
        public static CommonTypes.VirtualKeyStates DefaultSecondaryStopCancelButton = CommonTypes.VirtualKeyStates.F6;
        public static CommonTypes.VirtualKeyStates StartScriptShortcut = CommonTypes.VirtualKeyStates.Insert;

        // list of special send key codes
        public static List<string> SpecialKeys = new List<string>
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
    }
}
