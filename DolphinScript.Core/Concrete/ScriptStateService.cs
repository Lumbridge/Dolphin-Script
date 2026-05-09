using DolphinScript.Core.Classes;
using DolphinScript.Core.Events.BaseEvents;
using DolphinScript.Core.Interfaces;
using DolphinScript.Core.Models;
using DolphinScript.Core.WindowsApi;
using System.ComponentModel;
using System.Windows.Forms;

namespace DolphinScript.Core.Concrete
{
    public class ScriptStateService : IScriptState
    {
        public bool IsRunning
        {
            get => ScriptState.IsRunning;
            set => ScriptState.IsRunning = value;
        }

        public bool IsRegistering
        {
            get => ScriptState.IsRegistering;
            set => ScriptState.IsRegistering = value;
        }

        public int MinimumMouseSpeed
        {
            get => ScriptState.MinimumMouseSpeed;
            set => ScriptState.MinimumMouseSpeed = value;
        }

        public int MaximumMouseSpeed
        {
            get => ScriptState.MaximumMouseSpeed;
            set => ScriptState.MaximumMouseSpeed = value;
        }

        public double SearchPause
        {
            get => ScriptState.SearchPause;
            set => ScriptState.SearchPause = value;
        }

        public BindingSource AllEventsSource
        {
            get => ScriptState.AllEventsSource;
            set => ScriptState.AllEventsSource = value;
        }

        public BindingList<ScriptEvent> AllEvents => ScriptState.AllEvents;

        public MouseMovementService.MouseMovementMode MouseMovementMode
        {
            get => ScriptState.MouseMovementMode;
            set => ScriptState.MouseMovementMode = value;
        }

        public bool FreeMouse
        {
            get => ScriptState.FreeMouse;
            set => ScriptState.FreeMouse = value;
        }

        public bool QuickRegistrationEnabled
        {
            get => ScriptState.QuickRegistrationEnabled;
            set => ScriptState.QuickRegistrationEnabled = value;
        }

        public string CurrentAction
        {
            get => ScriptState.CurrentAction;
            set => ScriptState.CurrentAction = value;
        }

        public CommonTypes.Rect LastSavedArea
        {
            get => ScriptState.LastSavedArea;
            set => ScriptState.LastSavedArea = value;
        }

        public EventProcess LastSelectedProcess
        {
            get => ScriptState.LastSelectedProcess;
            set => ScriptState.LastSelectedProcess = value;
        }
    }
}