using System;
using DolphinScript.Core.Classes;
using DolphinScript.Core.Events.BaseEvents;
using DolphinScript.Core.Interfaces;

namespace DolphinScript.Core.Events.Window
{
    [Serializable]
    public class MoveWindowToFront : ScriptEvent
    {
        private readonly IWindowControlService _windowControlService;

        public MoveWindowToFront()
        {
        }

        public MoveWindowToFront(IWindowControlService windowControlService)
        {
            _windowControlService = windowControlService;
            EventType = Constants.EventType.MoveWindowToFront;
        }

        public override void Setup()
        {
            ScriptState.CurrentAction = $"Bring window to front: {EventProcess.WindowTitle}.";
        }

        /// <summary>
        /// main overriden method used to perform this script event
        /// </summary>
        public override void Execute()
        {
            _windowControlService.SetWindowTopMostIfExists(EventProcess.WindowTitle);
        }

        /// <summary>
        /// returns a string which is added to the listbox to give information about the event which was added to the event list
        /// </summary>
        /// <returns></returns>
        public override string EventDescription()
        {
            return "Move " + EventProcess.WindowTitle + " window to front.";
        }
    }
}
