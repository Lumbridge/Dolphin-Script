﻿using System;
using DolphinScript.Core.Classes;
using DolphinScript.Core.Events.BaseEvents;
using DolphinScript.Core.Interfaces;
using DolphinScript.Core.WindowsApi;

namespace DolphinScript.Core.Events.Mouse
{
    /// <summary>
    /// This event moves the mouse cursor to a random point in a given area.
    /// </summary>
    [Serializable]
    public class MouseMoveToArea : MouseMoveEvent
    {
        public MouseMoveToArea() { }

        public MouseMoveToArea(IMouseMovementService mouseMovementService, IPointService pointService, IWindowControlService windowControlService, IRandomService randomService) : base(mouseMovementService, pointService, windowControlService, randomService)
        {
        }

        /// <summary>
        /// main overriden method used to perform this script event
        /// </summary>
        public override void Invoke()
        {
            // update the status label on the main form
            ScriptState.Status = $"Mouse move to random point in area: {ClickArea.PrintArea()}.";
            CoordsToMoveTo = PointService.GetRandomPointInArea(ClickArea);
            base.Invoke();
        }

        /// <summary>
        /// returns a string which is added to the listbox to give information about the event which was added to the event list
        /// </summary>
        /// <returns></returns>
        public override string GetEventListBoxString()
        {
            if (!IsPartOfGroup)
                return "Move mouse to random point in area " + ClickArea.PrintArea() + ".";
            return "[Group " + GroupId + " Repeat x" + NumberOfCycles + "] Move mouse to random point in area " + ClickArea.PrintArea() + ".";
        }
    }
}