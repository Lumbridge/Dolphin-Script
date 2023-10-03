using DolphinScript.Core.Events.Keyboard;
using DolphinScript.Core.Events.Mouse;
using DolphinScript.Core.Events.Pause;
using DolphinScript.Core.Events.Window;
using System.Collections.Generic;
using System;

namespace DolphinScript.Core.Constants
{
    public class ScriptEventConstants
    {
        public enum EventType
        {
            FixedPause,
            PauseWhileColourDoesntExistInArea,
            PauseWhileColourDoesntExistInAreaOnWindow,
            PauseWhileColourExistsInArea,
            PauseWhileColourExistsInAreaOnWindow,
            PauseWhileWindowNotFound,
            RandomPauseInRange,
            MoveWindowToFront,
            MouseClick,
            MouseMove,
            MouseMoveToArea,
            MouseMoveToAreaOnWindow,
            MouseMoveToColour,
            MouseMoveToColourOnWindow,
            MouseMoveToMultiColourOnWindow,
            MouseMoveToPointOnWindow,
            KeyboardHoldKey,
            KeyboardKeyPress,
            KeyboardReleaseKey
        }

        public static Dictionary<EventType, Type> EventTypeDictionary = new Dictionary<EventType, Type>()
        {
            {EventType.FixedPause, typeof(FixedPause)},
            {EventType.PauseWhileColourDoesntExistInArea, typeof(PauseWhileColourDoesntExistInArea)},
            {EventType.PauseWhileColourDoesntExistInAreaOnWindow, typeof(PauseWhileColourDoesntExistInAreaOnWindow)},
            {EventType.PauseWhileColourExistsInArea, typeof(PauseWhileColourExistsInArea)},
            {EventType.PauseWhileColourExistsInAreaOnWindow, typeof(PauseWhileColourExistsInAreaOnWindow)},
            {EventType.PauseWhileWindowNotFound, typeof(PauseWhileWindowNotFound)},
            {EventType.RandomPauseInRange, typeof(RandomPauseInRange)},
            {EventType.MoveWindowToFront, typeof(MoveWindowToFront)},
            {EventType.MouseClick, typeof(MouseClick)},
            {EventType.MouseMove, typeof(MouseMove)},
            {EventType.MouseMoveToArea, typeof(MouseMoveToArea)},
            {EventType.MouseMoveToAreaOnWindow, typeof(MouseMoveToAreaOnWindow)},
            {EventType.MouseMoveToColour, typeof(MouseMoveToColour)},
            {EventType.MouseMoveToColourOnWindow, typeof(MouseMoveToColourOnWindow)},
            {EventType.MouseMoveToMultiColourOnWindow, typeof(MouseMoveToMultiColourOnWindow)},
            {EventType.MouseMoveToPointOnWindow, typeof(MouseMoveToPointOnWindow)},
            {EventType.KeyboardHoldKey, typeof(KeyboardHoldKey)},
            {EventType.KeyboardKeyPress, typeof(KeyboardKeyPress)},
            {EventType.KeyboardReleaseKey, typeof(KeyboardReleaseKey)}
        };

    }
}