using DolphinScript.Core.Classes;
using System.Collections.Generic;

namespace DolphinScript.Core.Interfaces
{
    public interface IScriptState
    { 
        bool IsRunning { get; set; }
        string LastAction { get; set; }
        string Status { get; set; }
        bool IsRegistering { get; set; }
        int MinimumMouseSpeed { get; set; }
        int MaximumMouseSpeed { get; set; }
        double SearchPause { get; set; }
        List<ScriptEvent> AllEvents { get; set; }
        List<List<ScriptEvent>> AllGroups { get; set; }
    }
}