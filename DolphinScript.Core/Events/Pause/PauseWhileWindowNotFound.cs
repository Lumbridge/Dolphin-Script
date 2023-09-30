using System;
using System.Threading;
using DolphinScript.Core.Classes;
using DolphinScript.Core.Events.BaseEvents;
using DolphinScript.Core.Interfaces;

namespace DolphinScript.Core.Events.Pause
{
    [Serializable]
    public class PauseWhileWindowNotFound : PauseEvent
    {
        public PauseWhileWindowNotFound() { }

        public PauseWhileWindowNotFound(IRandomService randomService,
            IColourService colourService, IPointService pointService, IWindowControlService windowControlService)
            : base(randomService, colourService, pointService, windowControlService)
        {
        }

        /// <summary>
        /// main overriden method used to perform this script event
        /// </summary>
        public override void InvokeScriptEvent()
        {
            ExecuteWhileLoop(() =>
            {
                ScriptState.Status = $"Pause while window: {WindowTitle} not found, waiting {ScriptState.SearchPause} seconds before searching again.";
                Thread.Sleep(TimeSpan.FromSeconds(ScriptState.SearchPause));
            }, () => !WindowControlService.WindowExists(WindowClass, WindowTitle));
        }

        /// <summary>
        /// returns a string which is added to the listbox to give information about the event which was added to the event list
        /// </summary>
        /// <returns></returns>
        public override string GetEventListBoxString()
        {
            return "Pause while window " + WindowTitle + " can't be found.";
        }
    }
}
