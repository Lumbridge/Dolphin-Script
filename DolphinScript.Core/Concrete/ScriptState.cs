using System.Collections.Generic;
using DolphinScript.Core.Classes;
using DolphinScript.Core.Interfaces;

namespace DolphinScript.Core.Concrete
{
    public class ScriptState : IScriptState
    {
        public bool IsRunning { get; set; }
        public string LastAction { get; set; }
        public string Status { get; set; }
        public bool IsRegistering { get; set; }
        public int MinimumMouseSpeed { get; set; }
        public int MaximumMouseSpeed { get; set; }
        public double SearchPause { get; set; } = 0.5;
        public List<ScriptEvent> AllEvents { get; set; } = new List<ScriptEvent>();
        public List<List<ScriptEvent>> AllGroups { get; set; } = new List<List<ScriptEvent>>();
    }
}