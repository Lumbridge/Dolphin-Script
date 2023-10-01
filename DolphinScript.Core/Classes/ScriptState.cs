﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DolphinScript.Core.Concrete;
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
        public static BindingSource AllEventsSource { get; set; }
        public static BindingList<ScriptEvent> AllEvents { get; set; } = new BindingList<ScriptEvent>();
        public static List<List<ScriptEvent>> AllGroups { get; set; } = new List<List<ScriptEvent>>();
        public static MouseMovementService.MouseMovementMode MouseMovementMode { get; set; } = MouseMovementService.MouseMovementMode.Realistic;
        public static bool FreeMouse { get; set; } = true;
    }
}