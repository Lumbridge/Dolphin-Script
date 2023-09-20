using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Serialization;
using DolphinScript.Core.Interfaces;
using DolphinScript.Core.WindowsApi;

namespace DolphinScript.Core.Classes
{
    [Serializable]
    //[XmlInclude(typeof(FixedPause))]
    //[XmlInclude(typeof(KeyboardHoldKey))]
    //[XmlInclude(typeof(KeyboardKeyPress))]
    //[XmlInclude(typeof(KeyboardReleaseKey))]
    //[XmlInclude(typeof(MouseClick))]
    //[XmlInclude(typeof(MouseMove))]
    //[XmlInclude(typeof(MouseMoveToArea))]
    //[XmlInclude(typeof(MouseMoveToAreaOnWindow))]
    //[XmlInclude(typeof(MouseMoveToColour))]
    //[XmlInclude(typeof(MouseMoveToColourOnWindow))]
    //[XmlInclude(typeof(MouseMoveToMultiColourOnWindow))]
    //[XmlInclude(typeof(MouseMoveToPointOnWindow))]
    //[XmlInclude(typeof(MoveWindowToFront))]
    //[XmlInclude(typeof(PauseWhileColourDoesntExistInArea))]
    //[XmlInclude(typeof(PauseWhileColourDoesntExistInAreaOnWindow))]
    //[XmlInclude(typeof(PauseWhileColourExistsInArea))]
    //[XmlInclude(typeof(PauseWhileColourExistsInAreaOnWindow))]
    //[XmlInclude(typeof(PauseWhileWindowNotFound))]
    //[XmlInclude(typeof(RandomPauseInRange))]
    public abstract class ScriptEvent
    {
        protected readonly IScriptState _scriptState;
        protected readonly IWindowControlService _windowControlService;
        protected readonly IRandomService _randomService;
        protected readonly IPointService _pointService;
        protected readonly IMouseMathService _mouseMathService;
        protected readonly IMouseMovementService _mouseMovementService;
        protected readonly IColourService _colourService;

        protected ScriptEvent(IScriptState scriptState, IWindowControlService windowControlService, IRandomService randomService, IPointService pointService, IMouseMathService mouseMathService, IMouseMovementService mouseMovementService, IColourService colourService)
        {
            _scriptState = scriptState;
            _windowControlService = windowControlService;
            _randomService = randomService;
            _pointService = pointService;
            _mouseMathService = mouseMathService;
            _mouseMovementService = mouseMovementService;
            _colourService = colourService;
        }

        /// <summary>
        /// Script Event Constructor - sets default values for properties
        /// </summary>
        protected ScriptEvent()
        {
            WindowToClickHandle = IntPtr.Zero;
            WindowToClickLocation = new CommonTypes.Rect();
            WindowToClickTitle = "NoWindow";
            CoordsToMoveTo = new Point();
            ClickArea = new CommonTypes.Rect();
            MouseButton = CommonTypes.VirtualMouseStates.None;
            KeyboardKeys = "NoKey";
            SearchColours = new List<int>();
            ColourSearchArea = new CommonTypes.Rect();
            EventsInGroup = new List<ScriptEvent>();
            IsPartOfGroup = false;
            GroupId = -1;
            NumberOfCycles = -1;
        }

        // private window variables
        private CommonTypes.Rect _windowRect;
        private IntPtr _windowHandle;

        // mandatory override methods
        public abstract void Invoke();
        public abstract string GetEventListBoxString();

        public void ExecuteWhileLoop(Action whileLoopBody, Func<bool> runCondition)
        {
            while (runCondition())
            {
                if (!_scriptState.IsRunning)
                {
                    return;
                }
                whileLoopBody();
            }
        }

        // gets the window handle by title (because handle ID can change often)
        [XmlIgnore]
        public IntPtr WindowToClickHandle
        {
            get
            {
                _windowHandle = PInvokeReferences.FindWindow(null, WindowToClickTitle);
                return _windowHandle;
            }
            set { _windowHandle = value; }
        }

        public string WindowClass { get; set; }
        public string WindowTitle { get; set; }

        // gets the window location (because the window can move/scale often)
        //
        public CommonTypes.Rect WindowToClickLocation
        {
            get
            {
                PInvokeReferences.GetWindowRect(WindowToClickHandle, ref _windowRect);
                return _windowRect;
            }
            set { _windowRect = value; }
        }

        // gets/sets the title of the window, if the name of the window changes then the event will break
        // window title can change but it's the most consistent window feature we can use to get the handle and location of the window
        //
        public string WindowToClickTitle { get; set; }

        // POINT we are going to move the mouse to in mouse move events
        //
        public Point CoordsToMoveTo { get; set; }

        // screen region we are going to move the mouse to in mouse move events
        //
        public CommonTypes.Rect ClickArea { get; set; }
        
        // click event mouse button
        //
        public CommonTypes.VirtualMouseStates MouseButton { get; set; }

        // string of keys we are sending during a keyboard type event
        //
        public string KeyboardKeys { get; set; }

        // double variables to use in pause events
        //
        public double DelayDuration { get; set; }
        public double? DelayMinimum { get; set; }
        public double? DelayMaximum { get; set; }

        // colour integer we are going to use when searching for colour in colour search events
        //
        public int SearchColour { get; set; }

        // list of search colours for multi-colour search events
        //
        public List<int> SearchColours { get; set; }

        // area we will search for colour in colour search events
        //
        public CommonTypes.Rect ColourSearchArea { get; set; }

        // repeat group event list
        //
        public List<ScriptEvent> EventsInGroup { get; set; }

        // tells us if this event is part of a group event list
        //
        public bool IsPartOfGroup { get; set; }

        // gives us the Id of the group that this event is part of
        //
        public int GroupId { get; set; }

        // gives us the index of the event inside it's event group
        //
        public int GroupEventIndex
        {
            get
            {
                for (var i = 0; i < EventsInGroup.Count; i++)
                    if (EventsInGroup[i] == this)
                        return i;

                return -1;
            }
        }

        // tells us how many times the repeat group is going to repeat for before continuing
        //
        public int NumberOfCycles { get; set; }
    }
}
