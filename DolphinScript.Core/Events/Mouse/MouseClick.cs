using System;
using System.Threading.Tasks;
using DolphinScript.Core.Classes;
using DolphinScript.Core.Events.BaseEvents;
using DolphinScript.Core.Interfaces;
using DolphinScript.Core.WindowsApi;

namespace DolphinScript.Core.Events.Mouse
{
    /// <summary>
    /// This event can be used to simulate a variety of different mouse clicks
    /// </summary>
    [Serializable]
    public class MouseClick : ScriptEvent
    {
        private readonly IRandomService _randomService;

        public MouseClick() { }

        public MouseClick(IRandomService randomService)
        {
            _randomService = randomService;
            EventType = Constants.EventType.MouseClick;
        }

        public override void Setup()
        {
            ScriptState.CurrentAction = $"Mouse Click: {MouseButton}";
        }

        /// <summary>
        /// main overriden method used to perform this script event
        /// </summary>
        public override void Execute()
        {
            switch (MouseButton)
            {
                case CommonTypes.VirtualMouseStates.LeftClick:
                    LeftClick();
                    break;
                case CommonTypes.VirtualMouseStates.RightClick:
                    RightClick();
                    break;
                case CommonTypes.VirtualMouseStates.MiddleClick:
                    MiddleClick();
                    break;
                case CommonTypes.VirtualMouseStates.LmbDown:
                    LeftDown();
                    break;
                case CommonTypes.VirtualMouseStates.LmbUp:
                    LeftUp();
                    break;
                case CommonTypes.VirtualMouseStates.MmbDown:
                    MiddleDown();
                    break;
                case CommonTypes.VirtualMouseStates.MmbUp:
                    MiddleUp();
                    break;
                case CommonTypes.VirtualMouseStates.RmbDown:
                    RightDown();
                    break;
                case CommonTypes.VirtualMouseStates.RmbUp:
                    RightUp();
                    break;
            }
        }

        /// <summary>
        /// returns a string which is added to the listbox to give information about the event which was added to the event list
        /// </summary>
        /// <returns></returns>
        public override string EventDescription()
        {
            return "Mouse Click: " + MouseButton + ".";
        }

        /// <summary>
        /// this method will simulate a simple left click
        /// </summary>
        public void LeftClick()
        {
            PInvokeReferences.mouse_event((uint)CommonTypes.VirtualMouseStates.LmbDown, 0, 0, 0, 0);
            Task.WaitAll(Task.Delay(_randomService.GetRandomNumber(20, 60)));
            PInvokeReferences.mouse_event((uint)CommonTypes.VirtualMouseStates.LmbUp, 0, 0, 0, 0);
            Task.WaitAll(Task.Delay(_randomService.GetRandomNumber(20, 60)));
        }

        /// <summary>
        /// this method will simulate a simple right click
        /// </summary>
        public void RightClick()
        {
            RightDown();
            Task.WaitAll(Task.Delay(_randomService.GetRandomNumber(20, 60)));
            RightUp();
            Task.WaitAll(Task.Delay(_randomService.GetRandomNumber(20, 60)));
        }

        /// <summary>
        /// this method will simulate a simple middle click
        /// </summary>
        public void MiddleClick()
        {
            PInvokeReferences.mouse_event((uint)CommonTypes.VirtualMouseStates.MmbDown, 0, 0, 0, 0);
            Task.WaitAll(Task.Delay(_randomService.GetRandomNumber(20, 60)));
            PInvokeReferences.mouse_event((uint)CommonTypes.VirtualMouseStates.MmbUp, 0, 0, 0, 0);
            Task.WaitAll(Task.Delay(_randomService.GetRandomNumber(20, 60)));
        }

        /// <summary>
        /// this method will simulate the right mouse button being held down
        /// </summary>
        public void RightDown()
        {
            PInvokeReferences.mouse_event((uint)CommonTypes.VirtualMouseStates.RmbDown, 0, 0, 0, 0);
        }

        /// <summary>
        /// this method will simulate the right mouse button being released
        /// </summary>
        public void RightUp()
        {
            PInvokeReferences.mouse_event((uint)CommonTypes.VirtualMouseStates.RmbUp, 0, 0, 0, 0);
        }

        /// <summary>
        /// this method will simulate the left mouse button being held down
        /// </summary>
        public void LeftDown()
        {
            PInvokeReferences.mouse_event((uint)CommonTypes.VirtualMouseStates.LmbDown, 0, 0, 0, 0);
        }

        /// <summary>
        /// this method will simulate the left mouse button being released
        /// </summary>
        public void LeftUp()
        {
            PInvokeReferences.mouse_event((uint)CommonTypes.VirtualMouseStates.LmbUp, 0, 0, 0, 0);
        }
        
        /// <summary>
        /// this method will simulate the middle mouse button being held down
        /// </summary>
        public void MiddleDown()
        {
            PInvokeReferences.mouse_event((uint)CommonTypes.VirtualMouseStates.MmbDown, 0, 0, 0, 0);
        }

        /// <summary>
        /// this method will simulate the middle mouse button being released
        /// </summary>
        public void MiddleUp()
        {
            PInvokeReferences.mouse_event((uint)CommonTypes.VirtualMouseStates.MmbUp, 0, 0, 0, 0);
        }
    }
}
