using System;
using System.Threading;

using static DolphinScript.Lib.Backend.GlobalVariables;
using static DolphinScript.Lib.Backend.WindowControl;

namespace DolphinScript.Lib.ScriptEventClasses
{
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
                Write("Search window not found, waiting 3 seconds before searching again.");
                Thread.Sleep(TimeSpan.FromSeconds(3.0));
            }
        }

        public override string GetEventListBoxString()
        {
            if (GroupID == -1)
                return "Pause while window " + WindowTitle + " can't be found.";
            else
                return "[Group " + GroupID + "] Pause while window " + WindowTitle + " can't be found.";
        }
    }
}
