using System;
using System.Threading;

using static DolphinScript.Lib.Backend.Common;
using static DolphinScript.Lib.Backend.WindowControl;

namespace DolphinScript.Lib.ScriptEventClasses
{
    [Serializable]
    class PauseWhileWindowNotFound : ScriptEvent
    {
        /// <summary>
        /// main overriden method used to perform this script event
        /// </summary>
        public override void DoEvent()
        {
            while (!WindowExists(WindowClass, WindowTitle))
            {
                // update the global status string
                //
                Status = $"Pause while window: {WindowToClickTitle} not found, waiting {ReSearchPause} seconds before searching again.";

                // wait before continuing
                //
                Thread.Sleep(TimeSpan.FromSeconds(ReSearchPause));
            }
        }

        public override string GetEventListBoxString()
        {
            if (GroupID == -1)
                return "Pause while window " + WindowTitle + " can't be found.";
            else
                return "[Group " + GroupID + " Repeat x" + NumberOfCycles + "] Pause while window " + WindowTitle + " can't be found.";
        }
    }
}
