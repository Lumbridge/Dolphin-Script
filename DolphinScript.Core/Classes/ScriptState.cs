using System.Collections.Generic;
using DolphinScript.Core.Events.BaseEvents;

namespace DolphinScript.Core.Classes
{
    public static class ScriptState
    {
        public static bool IsRunning { get; set; }
        public static string LastAction { get; set; }
        public static string Status { get; set; }
        public static bool IsRegistering { get; set; }
        public static int MinimumMouseSpeed { get; set; } = Constants.DefaultMinimumMouseSpeed;
        public static int MaximumMouseSpeed { get; set; } = Constants.DefaultMaximumMouseSpeed;
        public static double SearchPause { get; set; } = 0.5;
        public static List<ScriptEvent> AllEvents { get; set; } = new List<ScriptEvent>();
        public static List<List<ScriptEvent>> AllGroups { get; set; } = new List<List<ScriptEvent>>();
    }
}