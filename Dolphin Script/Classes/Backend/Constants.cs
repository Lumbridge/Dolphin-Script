using System;

namespace DolphinScript.Classes.Backend
{
    public class Constants
    {
        public const string SelectingPointToClick = "Selecting point to click... (F5 to finish).";
        public const string SelectingPointsToClick = "Selecting points to click... (F5 to finish).";
        public const string SelectingAreaToClick = "Selecting area to click... (F5 to finish).";
        public const string SelectingAreaToSearch = "Selecting area to search... (F5 to finish).";
        public const string SelectingColourToSearchForInArea = "Selecting colour to search for in area... (F5 to finish).";

        public static string DefaultFileName = $"MyScript-{Guid.NewGuid()}.xml";

        public const string AboutString = "Created by Ryan Sainty @ https://github.com/Lumbridge";

        public const string IdleStatus = "Status: idle";
        public const string NoLastAction = "Last action: None";
        public const string StartScript = "Start script";

        public const string ErrorMovingEvent = "Error moving event.";
        public const string NoEventsAdded = "No events have been added.";
        public const string OneGroupMaxError = "One or more selected events are already part of a group, events can only be part of one group.";
        public const string SelectMoreThanOneItemToMakeAGroup = "Select more than 1 item to create a group.";

        public static string ScriptRunning { get; set; } = "Script Running (F5 to stop)...";
    }
}
