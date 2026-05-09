using System;

namespace DolphinScript.Models
{
    public class ProcessChangedEventArgs : EventArgs
    {
        public ProcessChangedEventArgs(string processName)
        {
            ProcessName = processName;
        }

        public string ProcessName { get; }
    }
}