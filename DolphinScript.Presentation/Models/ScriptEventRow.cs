using DolphinScript.Core.Events.BaseEvents;
using System;
using System.ComponentModel;

namespace DolphinScript.Models
{
    public class ScriptEventRow : INotifyPropertyChanged
    {
        private string _currentAction;

        public ScriptEventRow(ScriptEvent scriptEvent, int index)
        {
            ScriptEvent = scriptEvent;
            Index = index;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ScriptEvent ScriptEvent { get; }
        public int Index { get; }
        public string ScriptEventName => ScriptEvent.GetType().Name;
        public string Description => ScriptEvent.EventDescription();
        public int GroupId => ScriptEvent.GroupId;
        public int RepeatCount => ScriptEvent.NumberOfCycles;
        public string WindowTitle => ScriptEvent.EventProcess.WindowTitle;
        public string WindowHandle => ScriptEvent.EventProcess.WindowHandle == IntPtr.Zero ? string.Empty : ScriptEvent.EventProcess.WindowHandle.ToString();
        public string ProcessName => ScriptEvent.EventProcess.ProcessName;

        public string CurrentAction
        {
            get => _currentAction;
            set
            {
                if (_currentAction == value)
                {
                    return;
                }

                _currentAction = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentAction)));
            }
        }
    }
}