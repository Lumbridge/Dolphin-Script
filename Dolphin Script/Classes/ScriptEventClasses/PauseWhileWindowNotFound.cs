using System;
using System.Threading;

using static DolphinScript.Lib.Backend.Common;
using static DolphinScript.Lib.Backend.WindowControl;

namespace DolphinScript.Lib.ScriptEventClasses
{
    [Serializable]
    class PauseWhileWindowNotFound : ScriptEvent
    {
        public PauseWhileWindowNotFound()
        {
            EventType = Event.Pause_While_Window_Not_Found;
        }

        public override void DoEvent()
        {
            while (!WindowExists(WindowClass, WindowTitle))
            {
                // update the global status string
                //
                Status = $"Search window not found, waiting {ReSearchPause} seconds before searching again.";

                // wait for 0.5 seconds before continuing
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
