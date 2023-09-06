using System;
using System.Threading;
using static DolphinScript.Classes.Backend.Common;
using static DolphinScript.Classes.Backend.WindowControl;

namespace DolphinScript.Classes.ScriptEventClasses
{
    [Serializable]
    public class PauseWhileWindowNotFound : ScriptEvent
    {
        /// <summary>
        /// main overriden method used to perform this script event
        /// </summary>
        public override void DoEvent()
        {
            while (!WindowExists(WindowClass, WindowTitle))
            {
                // update the status label on the main form
                //
                Status = $"Pause while window: {WindowToClickTitle} not found, waiting {ReSearchPause} seconds before searching again.";

                // wait before continuing
                //
                Thread.Sleep(TimeSpan.FromSeconds(ReSearchPause));
            }
        }

        /// <summary>
        /// returns a string which is added to the listbox to give information about the event which was added to the event list
        /// </summary>
        /// <returns></returns>
        public override string GetEventListBoxString()
        {
            if (GroupId == -1)
                return "Pause while window " + WindowTitle + " can't be found.";
            return "[Group " + GroupId + " Repeat x" + NumberOfCycles + "] Pause while window " + WindowTitle + " can't be found.";
        }
    }
}
