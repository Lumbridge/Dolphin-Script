using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

using static DolphinScript.Lib.Backend.RandomNumber;
using static DolphinScript.Lib.Backend.WinAPI;

namespace DolphinScript.Lib.ScriptEventClasses
{
    [Serializable]
    class MouseClick : ScriptEvent
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

        public MouseClick()
        {
            switch(MouseButton)
            {
                case VirtualMouseStates.Left_Click:
                    {
                        EventType = Event.Mouse_Left_Click;
                        break;
                    }
                case VirtualMouseStates.Right_Click:
                    {
                        EventType = Event.Mouse_Right_Click;
                        break;
                    }
                case VirtualMouseStates.Middle_Click:
                    {
                        EventType = Event.Mouse_Middle_Click;
                        break;
                    }
                case VirtualMouseStates.LMB_Down:
                    {
                        EventType = Event.Mouse_Left_Down;
                        break;
                    }
                case VirtualMouseStates.LMB_Up:
                    {
                        EventType = Event.Mouse_Left_UP;
                        break;
                    }
                case VirtualMouseStates.MMB_Down:
                    {
                        EventType = Event.Mouse_Middle_Down;
                        break;
                    }
                case VirtualMouseStates.MMB_Up:
                    {
                        EventType = Event.Mouse_Middle_Up;
                        break;
                    }
                case VirtualMouseStates.RMB_Down:
                    {
                        EventType = Event.Mouse_Right_Down;
                        break;
                    }
                case VirtualMouseStates.RMB_Up:
                    {
                        EventType = Event.Mouse_Right_Up;
                        break;
                    }
            }
        }

        public override void DoEvent()
        {
            if (MouseButton == VirtualMouseStates.Left_Click)
                LeftClick();
            else if (MouseButton == VirtualMouseStates.Right_Click)
                RightClick();
            else if (MouseButton == VirtualMouseStates.Middle_Click)
                MiddleClick();
            else if (MouseButton == VirtualMouseStates.LMB_Down)
                LeftDown();
            else if (MouseButton == VirtualMouseStates.LMB_Up)
                LeftUP();
            else if (MouseButton == VirtualMouseStates.MMB_Down)
                MiddleDown();
            else if (MouseButton == VirtualMouseStates.MMB_Up)
                MiddleUP();
            else if (MouseButton == VirtualMouseStates.RMB_Down)
                RightDown();
            else if (MouseButton == VirtualMouseStates.RMB_Up)
                RightUP();
        }

        public override string GetEventListBoxString()
        {
            if (GroupID == -1)
                return "Mouse Click: " + MouseButton + ".";
            else
                return "[Group " + GroupID + " Repeat x" + NumberOfCycles + "] Mouse Click:  " + MouseButton + ".";
        }

        static public void RightClick()
        {
            mouse_event((uint)VirtualMouseStates.RMB_Down, 0, 0, 0, 0);
            Task.WaitAll(Task.Delay((TimeSpan.FromSeconds(GetRandomDouble(0.1, 0.3)))));
            mouse_event((uint)VirtualMouseStates.RMB_Up, 0, 0, 0, 0);

            Thread.Sleep(TimeSpan.FromSeconds(GetRandomDouble(0.1, 0.3)));
        }

        static public void RightDown()
        {
            mouse_event((uint)VirtualMouseStates.RMB_Down, 0, 0, 0, 0);

            Task.WaitAll(Task.Delay((TimeSpan.FromSeconds(GetRandomDouble(0.1, 0.3)))));
        }

        static public void RightUP()
        {
            mouse_event((uint)VirtualMouseStates.RMB_Up, 0, 0, 0, 0);

            Task.WaitAll(Task.Delay(TimeSpan.FromSeconds(GetRandomDouble(0.1, 0.3))));
        }

        static public void LeftClick()
        {
            mouse_event((uint)VirtualMouseStates.LMB_Down, 0, 0, 0, 0);
            Task.WaitAll(Task.Delay(TimeSpan.FromSeconds(GetRandomDouble(0.1, 0.3))));
            mouse_event((uint)VirtualMouseStates.LMB_Up, 0, 0, 0, 0);

            Task.WaitAll(Task.Delay(TimeSpan.FromSeconds(GetRandomDouble(0.1, 0.3))));
        }

        static public void LeftDown()
        {
            mouse_event((uint)VirtualMouseStates.LMB_Down, 0, 0, 0, 0);

            Task.WaitAll(Task.Delay(TimeSpan.FromSeconds(GetRandomDouble(0.1, 0.3))));
        }

        static public void LeftUP()
        {
            mouse_event((uint)VirtualMouseStates.LMB_Up, 0, 0, 0, 0);

            Task.WaitAll(Task.Delay(TimeSpan.FromSeconds(GetRandomDouble(0.1, 0.3))));
        }

        static public void MiddleClick()
        {
            mouse_event((uint)VirtualMouseStates.MMB_Down, 0, 0, 0, 0);
            Task.WaitAll(Task.Delay(TimeSpan.FromSeconds(GetRandomDouble(0.1, 0.3))));
            mouse_event((uint)VirtualMouseStates.MMB_Up, 0, 0, 0, 0);

            Task.WaitAll(Task.Delay(TimeSpan.FromSeconds(GetRandomDouble(0.1, 0.3))));
        }

        static public void MiddleDown()
        {
            mouse_event((uint)VirtualMouseStates.MMB_Down, 0, 0, 0, 0);

            Task.WaitAll(Task.Delay(TimeSpan.FromSeconds(GetRandomDouble(0.1, 0.3))));
        }

        static public void MiddleUP()
        {
            mouse_event((uint)VirtualMouseStates.MMB_Up, 0, 0, 0, 0);

            Task.WaitAll(Task.Delay(TimeSpan.FromSeconds(GetRandomDouble(0.1, 0.3))));
        }
    }
}
