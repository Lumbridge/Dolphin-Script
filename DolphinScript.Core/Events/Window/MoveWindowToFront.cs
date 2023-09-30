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
        }

        /// <summary>
        /// main overriden method used to perform this script event
        /// </summary>
        public override void InvokeScriptEvent()
        {
            // update the status label on the main form
            ScriptState.Status = $"Bring window to front: {WindowTitle}.";
            _windowControlService.SetWindowTopMostIfExists(WindowClass, WindowTitle);
        }

        /// <summary>
        /// returns a string which is added to the listbox to give information about the event which was added to the event list
        /// </summary>
        /// <returns></returns>
        public override string GetEventListBoxString()
        {
            return "Move " + WindowTitle + " window to front.";
        }
    }
}
