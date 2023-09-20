using DolphinScript.Core.Classes;
using DolphinScript.Core.WindowsApi;
using System;
using System.Threading.Tasks;

namespace DolphinScript.Event.Mouse
{
    /// <summary>
    /// This event can be used to simulate a variety of different mouse clicks
    /// </summary>
    [Serializable]
    public class MouseClick : ScriptEvent
    {
        /// <summary>
        /// main overriden method used to perform this script event
        /// </summary>
        public override void Invoke()
        {
            // update the status label on the main form
            //
            _scriptState.Status = $"Mouse Click: {MouseButton}";

            switch (MouseButton)
            {
                // check which virtual mouse state is associated with the event and perform that type of mouse event
                //
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
        public override string GetEventListBoxString()
        {
            if (GroupId == -1)
                return "Mouse Click: " + MouseButton + ".";
            return "[Group " + GroupId + " Repeat x" + NumberOfCycles + "] Mouse Click:  " + MouseButton + ".";
        }

        /// <summary>
        /// this method will simulate a simple right click
        /// </summary>
        public void RightClick()
        {
            RightDown();
            Task.WaitAll(Task.Delay(TimeSpan.FromSeconds(_randomService.GetRandomDouble(0.03, 0.07))));
            RightUp();
            Task.WaitAll(Task.Delay(TimeSpan.FromSeconds(_randomService.GetRandomDouble(0.03, 0.07))));
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
        /// this method will simulate a simple left click
        /// </summary>
        public void LeftClick()
        {
            PInvokeReferences.mouse_event((uint)CommonTypes.VirtualMouseStates.LmbDown, 0, 0, 0, 0);
            Task.WaitAll(Task.Delay(TimeSpan.FromSeconds(_randomService.GetRandomDouble(0.03, 0.07))));
            PInvokeReferences.mouse_event((uint)CommonTypes.VirtualMouseStates.LmbUp, 0, 0, 0, 0);
            Task.WaitAll(Task.Delay(TimeSpan.FromSeconds(_randomService.GetRandomDouble(0.03, 0.07))));
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
        /// this method will simulate a simple middle click
        /// </summary>
        public void MiddleClick()
        {
            PInvokeReferences.mouse_event((uint)CommonTypes.VirtualMouseStates.MmbDown, 0, 0, 0, 0);
            Task.WaitAll(Task.Delay(TimeSpan.FromSeconds(_randomService.GetRandomDouble(0.1, 0.3))));
            PInvokeReferences.mouse_event((uint)CommonTypes.VirtualMouseStates.MmbUp, 0, 0, 0, 0);
            Task.WaitAll(Task.Delay(TimeSpan.FromSeconds(_randomService.GetRandomDouble(0.1, 0.3))));
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
