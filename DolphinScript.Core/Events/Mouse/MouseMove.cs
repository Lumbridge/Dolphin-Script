﻿using System;
using DolphinScript.Core.Classes;
using DolphinScript.Core.Constants;
using DolphinScript.Core.Events.BaseEvents;
using DolphinScript.Core.Interfaces;

namespace DolphinScript.Core.Events.Mouse
{
    /// <summary>
    /// This class provides the core mouse moving functionality.
    /// </summary>
    [Serializable]
    public class MouseMove : MouseMoveEvent
    {
        public MouseMove() { }

        public MouseMove(IMouseMovementService mouseMovementService, IPointService pointService, IWindowControlService windowControlService, IRandomService randomService, IColourService colourService) : base(mouseMovementService, pointService, windowControlService, randomService, colourService)
        {
            EventType = ScriptEventConstants.EventType.MouseMove;
        }

        /// <summary>
        /// main overriden method used to perform this script event
        /// </summary>
        public override void Execute()
        {
            ScriptState.CurrentAction = $"Mouse move: {CoordsToMoveTo}.";
            MouseMovementService.MoveMouseToPoint(CoordsToMoveTo);
        }

        /// <summary>
        /// returns a string which is added to the listbox to give information about the event which was added to the event list
        /// </summary>
        /// <returns></returns>
        public override string EventDescription()
        {
            return "Move mouse to Point X: " + CoordsToMoveTo.X + " Y: " + CoordsToMoveTo.Y + ".";
        }
    }
}