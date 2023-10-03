using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DolphinScript.Core.Concrete;
using DolphinScript.Core.Events.BaseEvents;
using DolphinScript.Core.WindowsApi;

namespace DolphinScript.Core.Classes
{
    public static class ScriptState
    {
        public static bool IsRunning { get; set; }
        public static bool IsRegistering { get; set; }
        public static int MinimumMouseSpeed { get; set; } = Constants.MainFormConstants.DefaultMinimumMouseSpeed;
        public static int MaximumMouseSpeed { get; set; } = Constants.MainFormConstants.DefaultMaximumMouseSpeed;
        public static double SearchPause { get; set; } = 0.5;
        public static BindingSource AllEventsSource { get; set; }
        public static BindingList<ScriptEvent> AllEvents { get; set; } = new BindingList<ScriptEvent>();
        public static List<List<ScriptEvent>> AllGroups { get; set; } = new List<List<ScriptEvent>>();
        public static MouseMovementService.MouseMovementMode MouseMovementMode { get; set; } = MouseMovementService.MouseMovementMode.Realistic;
        public static bool FreeMouse { get; set; } = false;
        public static string CurrentAction { get; set; }
        public static CommonTypes.Rect LastSavedArea { get; set; }
    }
}