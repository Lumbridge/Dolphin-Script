using System;
using DolphinScript.Core.Classes;

namespace DolphinScript.Event.Window
{
    [Serializable]
    public class MoveWindowToFront : ScriptEvent
    {
        /// <summary>
        /// main overriden method used to perform this script event
        /// </summary>
        public override void Invoke()
        {
            // update the status label on the main form
            //
            _scriptState.Status = $"Bring window to front: {WindowToClickTitle}.";

            _windowControlService.SetWindowTopMostIfExists(WindowClass, WindowTitle);
        }

        /// <summary>
        /// returns a string which is added to the listbox to give information about the event which was added to the event list
        /// </summary>
        /// <returns></returns>
        public override string GetEventListBoxString()
        {
            if (GroupId == -1)
                return "Move " + WindowTitle + " window to front.";
            return "[Group " + GroupId + " Repeat x" + NumberOfCycles + "] Move " + WindowTitle + " window to front.";
        }
    }
}
