using System;
using System.Linq;
using DolphinScript.Core.Interfaces;

namespace DolphinScript.Core.Models
{
    public class EventProcess
    {
        private readonly IWindowControlService _windowControlService;

        public EventProcess() { }

        public EventProcess(IWindowControlService windowControlService)
        {
            _windowControlService = windowControlService;
        }

        public string ProcessName { get; set; }

        public IntPtr WindowHandle => 
            WindowTitle == null ? IntPtr.Zero : _windowControlService.GetWindowHandleByName(WindowTitle);

        public string WindowTitle { get; set; } = null;
    }
}