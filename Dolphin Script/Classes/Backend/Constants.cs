using System;

namespace DolphinScript.Classes.Backend
{
    public class Constants
    {
        public static string SelectingPointToClick => $"Selecting point to click... ({DefaultStopCancelButton} to finish).";
        public static string SelectingPointsToClick => $"Selecting points to click... ({DefaultStopCancelButton} to finish).";
        public static string SelectingAreaToClick => $"Selecting area to click... ({DefaultStopCancelButton} to finish).";
        public static string SelectingAreaToSearch => $"Selecting area to search... ({DefaultStopCancelButton} to finish).";
        public static string SelectingColourToSearchForInArea => $"Selecting colour to search for in area... ({DefaultStopCancelButton} to finish).";

        public static string DefaultFileName = $"MyScript-{Guid.NewGuid()}.xml";

        public const string AboutString = "Created by Ryan Sainty @ https://github.com/Lumbridge";

        public const string IdleStatus = "Status: idle";
        public const string NoLastAction = "Last action: None";
        public const string StartScript = "Start script";

        public const string ErrorMovingEvent = "Error moving event.";
        public const string NoEventsAdded = "No events have been added.";
        public const string OneGroupMaxError = "One or more selected events are already part of a group, events can only be part of one group.";
        public const string SelectMoreThanOneItemToMakeAGroup = "Select more than 1 item to create a group.";

        public static string ScriptRunning => $"Script Running ({DefaultStopCancelButton} to stop)...";

        public static WinApi.VirtualKeyStates DefaultStopCancelButton = WinApi.VirtualKeyStates.F5;
        public static WinApi.VirtualKeyStates DefaultSecondaryStopCancelButton = WinApi.VirtualKeyStates.F6;
        public static WinApi.VirtualKeyStates StartScriptShortcut = WinApi.VirtualKeyStates.Insert;
    }
}
