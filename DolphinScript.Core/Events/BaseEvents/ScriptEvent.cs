using DolphinScript.Core.Classes;
using DolphinScript.Core.Events.Keyboard;
using DolphinScript.Core.Events.Mouse;
using DolphinScript.Core.Events.Pause;
using DolphinScript.Core.Events.Window;
using DolphinScript.Core.WindowsApi;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Serialization;

namespace DolphinScript.Core.Events.BaseEvents
{
    [Serializable]
    [XmlInclude(typeof(FixedPause))]
    [XmlInclude(typeof(KeyboardHoldKey))]
    [XmlInclude(typeof(KeyboardKeyPress))]
    [XmlInclude(typeof(KeyboardReleaseKey))]
    [XmlInclude(typeof(MouseClick))]
    [XmlInclude(typeof(MouseMove))]
    [XmlInclude(typeof(MouseMoveToArea))]
    [XmlInclude(typeof(MouseMoveToAreaOnWindow))]
    [XmlInclude(typeof(MouseMoveToColour))]
    [XmlInclude(typeof(MouseMoveToColourOnWindow))]
    [XmlInclude(typeof(MouseMoveToMultiColourOnWindow))]
    [XmlInclude(typeof(MouseMoveToPointOnWindow))]
    [XmlInclude(typeof(MoveWindowToFront))]
    [XmlInclude(typeof(PauseWhileColourDoesntExistInArea))]
    [XmlInclude(typeof(PauseWhileColourDoesntExistInAreaOnWindow))]
    [XmlInclude(typeof(PauseWhileColourExistsInArea))]
    [XmlInclude(typeof(PauseWhileColourExistsInAreaOnWindow))]
    [XmlInclude(typeof(PauseWhileWindowNotFound))]
    [XmlInclude(typeof(RandomPauseInRange))]
    public abstract class ScriptEvent
    {
        // mandatory override methods
        public abstract void Setup();
        public abstract void Execute();

        public virtual string EventDescription()
        {
            return string.Empty;
        }
        
        public void ExecuteWhileLoop(Action whileLoopBody, Func<bool> runCondition)
        {
            while (runCondition())
            {
                if (!ScriptState.IsRunning)
                {
                    return;
                }
                whileLoopBody();
            }
        }

        // click event mouse button
        public CommonTypes.VirtualMouseStates MouseButton { get; set; } = CommonTypes.VirtualMouseStates.None;

        // string of keys we are sending during a keyboard type event
        public string KeyboardKeys { get; set; }

        // gives us the index of the event inside it's event group
        public int GroupEventIndex => EventsInGroup.IndexOf(this);

        public int GroupId { get; set; }

        public List<ScriptEvent> EventsInGroup { get; set; } = new List<ScriptEvent>();
        
        public bool IsPartOfGroup { get; set; }

        // tells us how many times the repeat group is going to repeat for before continuing
        public int NumberOfCycles { get; set; }

        // pause specific properties
        public double DelayDuration { get; set; }
        public double? DelayMinimum { get; set; }
        public double? DelayMaximum { get; set; }

        // window specific properties
        public string WindowClass { get; set; }
        public string WindowTitle { get; set; }

        // mouse move specific properties
        public Point CoordsToMoveTo { get; set; }
        public CommonTypes.Rect ClickArea { get; set; }

        // colour specific properties
        public CommonTypes.Rect ColourSearchArea { get; set; }
        public int SearchColour { get; set; }
        // list of search colours for multi-colour search events
        public List<int> SearchColours { get; set; }

        public Constants.EventType EventType { get; set; }
    }
}
