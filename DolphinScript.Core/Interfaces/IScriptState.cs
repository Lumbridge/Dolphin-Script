using DolphinScript.Core.Concrete;
using DolphinScript.Core.Events.BaseEvents;
using DolphinScript.Core.Models;
using DolphinScript.Core.WindowsApi;
using System.ComponentModel;
using System.Windows.Forms;

namespace DolphinScript.Core.Interfaces
{
    public interface IScriptState
    {
        bool IsRunning { get; set; }
        bool IsRegistering { get; set; }
        int MinimumMouseSpeed { get; set; }
        int MaximumMouseSpeed { get; set; }
        double SearchPause { get; set; }
        BindingSource AllEventsSource { get; set; }
        BindingList<ScriptEvent> AllEvents { get; }
        MouseMovementService.MouseMovementMode MouseMovementMode { get; set; }
        bool FreeMouse { get; set; }
        bool QuickRegistrationEnabled { get; set; }
        string CurrentAction { get; set; }
        CommonTypes.Rect LastSavedArea { get; set; }
        EventProcess LastSelectedProcess { get; set; }
    }
}