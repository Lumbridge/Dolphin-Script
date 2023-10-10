using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace DolphinScript.Core.WindowsApi
{
    public class CommonTypes
    {
        /// <summary>
        /// different mouse states the mouse can have
        /// </summary>
        public enum VirtualMouseStates : uint
        {
            LmbDown = 0x02,
            LmbUp = 0x04,
            RmbDown = 0x08,
            RmbUp = 0x10,
            MmbDown = 0x0020,
            MmbUp = 0x0040,
            LeftClick,
            MiddleClick,
            RightClick,
            None
        }

        /// <summary>
        /// contains the different keys on the keyboard
        /// </summary>
        public enum VirtualKeyStates
        {
            Back = 0x08,
            Tab = 0x09,
            //
            Clear = 0x0C,
            Return = 0x0D,
            //
            Shift = 0x10,
            Control = 0x11,
            Menu = 0x12,
            Pause = 0x13,
            Capital = 0x14,
            //
            VkEscape = 0x1B,
            //
            Space = 0x20,
            Prior = 0x21,
            Next = 0x22,
            End = 0x23,
            Home = 0x24,
            Left = 0x25,
            Up = 0x26,
            Right = 0x27,
            Down = 0x28,
            Select = 0x29,
            Print = 0x2A,
            Execute = 0x2B,
            Snapshot = 0x2C,
            Insert = 0x2D,
            Delete = 0x2E,
            Help = 0x2F,
            //
            Lwin = 0x5B,
            Rwin = 0x5C,
            //
            Numpad0 = 0x60,
            Numpad1 = 0x61,
            Numpad2 = 0x62,
            Numpad3 = 0x63,
            Numpad4 = 0x64,
            Numpad5 = 0x65,
            Numpad6 = 0x66,
            Numpad7 = 0x67,
            Numpad8 = 0x68,
            Numpad9 = 0x69,
            Multiply = 0x6A,
            Add = 0x6B,
            Separator = 0x6C,
            Subtract = 0x6D,
            Decimal = 0x6E,
            Divide = 0x6F,
            //
            KeyA = 0x41,
            KeyB = 0x42,
            KeyC = 0x43,
            KeyD = 0x44,
            KeyE = 0x45,
            KeyF = 0x46,
            KeyG = 0x47,
            KeyH = 0x48,
            KeyI = 0x49,
            KeyJ = 0x4A,
            KeyK = 0x4B,
            KeyL = 0x4C,
            KeyM = 0x4D,
            KeyN = 0x4E,
            KeyO = 0x4F,
            KeyP = 0x50,
            KeyQ = 0x51,
            KeyR = 0x52,
            KeyS = 0x53,
            KeyT = 0x54,
            KeyU = 0x55,
            KeyV = 0x56,
            KeyW = 0x57,
            KeyX = 0x58,
            KeyY = 0x59,
            KeyZ = 0x5A,
            //
            Key0 = 0x30,
            Key1 = 0x31,
            Key2 = 0x32,
            Key3 = 0x33,
            Key4 = 0x34,
            Key5 = 0x35,
            Key6 = 0x36,
            Key7 = 0x37,
            Key8 = 0x38,
            Key9 = 0x39,
            //
            F1 = 0x70,
            F2 = 0x71,
            F3 = 0x72,
            F4 = 0x73,
            F5 = 0x74,
            F6 = 0x75,
            F7 = 0x76,
            F8 = 0x77,
            F9 = 0x78,
            F10 = 0x79,
            F11 = 0x7A,
            F12 = 0x7B,
            F13 = 0x7C,
            F14 = 0x7D,
            F15 = 0x7E,
            F16 = 0x7F,
            F17 = 0x80,
            F18 = 0x81,
            F19 = 0x82,
            F20 = 0x83,
            F21 = 0x84,
            F22 = 0x85,
            F23 = 0x86,
            F24 = 0x87,
            //
            NumLock = 0x90,
            ScrollLock = 0x91,
            //
            VkOemNecEqual = 0x92,   // '=' key on numpad
            //
            Lshift = 0xA0,
            Rshift = 0xA1,
            Lcontrol = 0xA2,
            Rcontrol = 0xA3,
            Lmenu = 0xA4,
            Rmenu = 0xA5,
            //
            BrowserBack = 0xA6,
            BrowserForward = 0xA7,
            BrowserRefresh = 0xA8,
            BrowserStop = 0xA9,
            BrowserSearch = 0xAA,
            BrowserFavorites = 0xAB,
            BrowserHome = 0xAC,
            //
            VolumeMute = 0xAD,
            VolumeDown = 0xAE,
            VolumeUp = 0xAF,
            MediaNextTrack = 0xB0,
            MediaPrevTrack = 0xB1,
            MediaStop = 0xB2,
            MediaPlayPause = 0xB3,
            LaunchMail = 0xB4,
            LaunchMediaSelect = 0xB5,
            LaunchApp1 = 0xB6,
            LaunchApp2 = 0xB7,
            //
            Oem1 = 0xBA,   // ';:' for US
            OemPlus = 0xBB,   // '+' any country
            OemComma = 0xBC,   // ',' any country
            OemMinus = 0xBD,   // '-' any country
            OemPeriod = 0xBE,   // '.' any country
            Oem2 = 0xBF,   // '/?' for US
            Oem3 = 0xC0,   // '`~' for US
                               //
            Oem4 = 0xDB,  //  '[{' for US
            Oem5 = 0xDC,  //  '\|' for US
            Oem6 = 0xDD,  //  ']}' for US
            Oem7 = 0xDE,  //  ''"' for US
            Oem8 = 0xDF,
            //
            OemAx = 0xE1,  //  'AX' key on Japanese AX kbd
            Oem102 = 0xE2,  //  "<>" or "\|" on RT 102-key kbd.
            IcoHelp = 0xE3,  //  Help key on ICO
            Ico00 = 0xE4,  //  00 key on ICO
                             //
            Processkey = 0xE5,
            //
            IcoClear = 0xE6,
            //
            Packet = 0xE7,
            //
            OemReset = 0xE9,
            OemJump = 0xEA,
            OemPa1 = 0xEB,
            OemPa2 = 0xEC,
            OemPa3 = 0xED,
            OemWsctrl = 0xEE,
            OemCusel = 0xEF,
            OemAttn = 0xF0,
            OemFinish = 0xF1,
            OemCopy = 0xF2,
            OemAuto = 0xF3,
            OemEnlw = 0xF4,
            OemBacktab = 0xF5,
            //
            Attn = 0xF6,
            Crsel = 0xF7,
            Exsel = 0xF8,
            Ereof = 0xF9,
            Play = 0xFA,
            Zoom = 0xFB,
            Noname = 0xFC,
            Pa1 = 0xFD,
            OemClear = 0xFE,
            None
        }

        /// <summary>
        /// struct used to store a rect area, offers extra functionality
        /// </summary>
        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public struct Rect
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;

            public int Height => Math.Abs(Bottom - Top);
            public int Width => Left < 0 && Right > 0 ? Right : Math.Abs(Right - Left);

            public Point Location => new Point(Left, Top);

            public Size Size => new Size(Width, Height);

            /// <summary>
            /// RECT constructor which takes two points (TopLeft & BottomRight points)
            /// </summary>
            /// <param name="topLeft"></param>
            /// <param name="bottomRight"></param>
            public Rect(Point topLeft, Point bottomRight)
            {
                Top = topLeft.Y;
                Left = topLeft.X;
                Bottom = bottomRight.Y;
                Right = bottomRight.X;
            }

            /// <summary>
            /// RECT constructor which takes four ints (each side of the RECT area)
            /// </summary>
            /// <param name="top"></param>
            /// <param name="left"></param>
            /// <param name="bottom"></param>
            /// <param name="right"></param>
            public Rect(int top, int left, int bottom, int right)
            {
                Top = top;
                Left = left;
                Bottom = bottom;
                Right = right;
            }

            public override string ToString()
            {
                return "Top-Left XY: " + Left + ", " + Top + " Bottom-Right XY: " + Right + ", " + Bottom;
            }
        }
    }
}
