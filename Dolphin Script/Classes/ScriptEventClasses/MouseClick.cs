using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using static DolphinScript.Classes.Backend.Common;
using static DolphinScript.Classes.Backend.RandomNumber;
using static DolphinScript.Classes.Backend.WinApi;

namespace DolphinScript.Classes.ScriptEventClasses
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
            Status = $"Mouse Click: {MouseButton}";

            // check which virtual mouse state is associated with the event and perform that type of mouse event
            //
            if (MouseButton == VirtualMouseStates.LeftClick)
                LeftClick();
            else if (MouseButton == VirtualMouseStates.RightClick)
                RightClick();
            else if (MouseButton == VirtualMouseStates.MiddleClick)
                MiddleClick();
            else if (MouseButton == VirtualMouseStates.LmbDown)
                LeftDown();
            else if (MouseButton == VirtualMouseStates.LmbUp)
                LeftUp();
            else if (MouseButton == VirtualMouseStates.MmbDown)
                MiddleDown();
            else if (MouseButton == VirtualMouseStates.MmbUp)
                MiddleUp();
            else if (MouseButton == VirtualMouseStates.RmbDown)
                RightDown();
            else if (MouseButton == VirtualMouseStates.RmbUp)
                RightUp();
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
        public static void RightClick()
        {
            RightDown();
            Task.WaitAll(Task.Delay(TimeSpan.FromSeconds(GetRandomDouble(0.03, 0.07))));
            RightUp();
            Task.WaitAll(Task.Delay(TimeSpan.FromSeconds(GetRandomDouble(0.03, 0.07))));
        }

        /// <summary>
        /// this method will simulate the right mouse button being held down
        /// </summary>
        public static void RightDown()
        {
            mouse_event((uint)VirtualMouseStates.RmbDown, 0, 0, 0, 0);
        }

        /// <summary>
        /// this method will simulate the right mouse button being released
        /// </summary>
        public static void RightUp()
        {
            mouse_event((uint)VirtualMouseStates.RmbUp, 0, 0, 0, 0);
        }

        /// <summary>
        /// this method will simulate a simple left click
        /// </summary>
        public static void LeftClick()
        {
            mouse_event((uint)VirtualMouseStates.LmbDown, 0, 0, 0, 0);
            Task.WaitAll(Task.Delay(TimeSpan.FromSeconds(GetRandomDouble(0.03, 0.07))));
            mouse_event((uint)VirtualMouseStates.LmbUp, 0, 0, 0, 0);
            Task.WaitAll(Task.Delay(TimeSpan.FromSeconds(GetRandomDouble(0.03, 0.07))));
        }

        /// <summary>
        /// this method will simulate the left mouse button being held down
        /// </summary>
        public static void LeftDown()
        {
            mouse_event((uint)VirtualMouseStates.LmbDown, 0, 0, 0, 0);
        }

        /// <summary>
        /// this method will simulate the left mouse button being released
        /// </summary>
        public static void LeftUp()
        {
            mouse_event((uint)VirtualMouseStates.LmbUp, 0, 0, 0, 0);
        }

        /// <summary>
        /// this method will simulate a simple middle click
        /// </summary>
        public static void MiddleClick()
        {
            mouse_event((uint)VirtualMouseStates.MmbDown, 0, 0, 0, 0);
            Task.WaitAll(Task.Delay(TimeSpan.FromSeconds(GetRandomDouble(0.1, 0.3))));
            mouse_event((uint)VirtualMouseStates.MmbUp, 0, 0, 0, 0);
            Task.WaitAll(Task.Delay(TimeSpan.FromSeconds(GetRandomDouble(0.1, 0.3))));
        }

        /// <summary>
        /// this method will simulate the middle mouse button being held down
        /// </summary>
        public static void MiddleDown()
        {
            mouse_event((uint)VirtualMouseStates.MmbDown, 0, 0, 0, 0);
        }

        /// <summary>
        /// this method will simulate the middle mouse button being released
        /// </summary>
        public static void MiddleUp()
        {
            mouse_event((uint)VirtualMouseStates.MmbUp, 0, 0, 0, 0);
        }

        /// <summary>
        /// we import this mouse_event method so we can use it to perform different operations using the mouse buttons
        /// </summary>
        /// <param name="dwFlags"></param>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        /// <param name="cButtons"></param>
        /// <param name="dwExtraInfo"></param>
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);
    }
}
