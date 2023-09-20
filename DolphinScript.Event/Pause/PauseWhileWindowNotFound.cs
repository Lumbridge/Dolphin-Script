﻿using DolphinScript.Core.Interfaces;
using DolphinScript.Event.BaseEvents;
using System;
using System.Threading;
using DolphinScript.Event.Interfaces;

namespace DolphinScript.Event.Pause
{
    [Serializable]
    public class PauseWhileWindowNotFound : PauseEvent
    {
        public PauseWhileWindowNotFound(IScriptState scriptState, IRandomService randomService,
            IColourService colourService, IPointService pointService, IWindowControlService windowControlService)
            : base(scriptState, randomService, colourService, pointService, windowControlService)
        {
        }

        /// <summary>
        /// main overriden method used to perform this script event
        /// </summary>
        public override void Invoke()
        {
            ExecuteWhileLoop(() =>
            {
                // update the status label on the main form
                //
                _scriptState.Status = $"Pause while window: {WindowToClickTitle} not found, waiting {_scriptState.SearchPause} seconds before searching again.";

                // wait before continuing
                //
                Thread.Sleep(TimeSpan.FromSeconds(_scriptState.SearchPause));
            }, () => !_windowControlService.WindowExists(WindowClass, WindowTitle));
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
