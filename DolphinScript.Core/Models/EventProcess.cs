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

        public IntPtr WindowHandle
        {
            get
            {
                if (ProcessId != null)
                {
                    return _windowControlService.GetWindowHandle(ProcessId.Value);
                }

                return WindowTitle == null ? IntPtr.Zero : _windowControlService.GetWindowHandleByName(WindowTitle);
            }
        }

        public string WindowTitle { get; set; } = null;

        public int? ProcessId { get; set; }
    }
}