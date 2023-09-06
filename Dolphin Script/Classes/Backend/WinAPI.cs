using System;
using System.Runtime.InteropServices;

namespace DolphinScript.Classes.Backend
{
    /// <summary>
    /// This class contains methods and variables invoked from other languages and are used in multiple different classes.
    /// </summary>
    public class WinApi
    {
        // minimise/maximise window flags
        //
        public static int SwShownormal = 1;
        public static int SwShowminimized = 2;
        public static int SwShowmaximized = 3;

        // keypressed mem state
        //
        public const int KeyPressed = 0x8000;

        /// <summary>
        /// struct used to store a rect area, offers extra functionality
        /// </summary>
        [Serializable]
        public struct Rect
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;

            public int Height { get { return Bottom - Top; } }
            public int Width { get { return Right - Left; } }

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
                this.Top = top;
                this.Left = left;
                this.Bottom = bottom;
                this.Right = right;
            }

            /// <summary>
            /// prints the area of the rect
            /// </summary>
            /// <returns></returns>
            public string PrintArea()
            {
                return "Top-Left XY: " + Left + ", " + Top + " Bottom-Right XY: " + Right + ", " + Bottom;
            }
        }

        /// <summary>
        /// this struct is used to store a point, also offers extra functionality
        /// </summary>
        [Serializable]
        public struct Point
        {
            public int X;
            public int Y;
            
            /// <summary>
            /// POINT constructor takes two ints which are the x and y position of the coordinate
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            public Point(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            public override string ToString()
            {
                return $"X: {X} Y: {Y}";
            }

            /// <summary>
            /// converts the normal C# point to our POINT structure
            /// </summary>
            /// <param name="point"></param>
            public static implicit operator System.Drawing.Point(Point point)
            {
                return new System.Drawing.Point(point.X, point.Y);
            }
        }

        /// <summary>
        /// stores the different mouse states the mouse can take
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
            VkBack = 0x08,
            VkTab = 0x09,
            //
            VkClear = 0x0C,
            VkReturn = 0x0D,
            //
            VkShift = 0x10,
            VkControl = 0x11,
            VkMenu = 0x12,
            VkPause = 0x13,
            VkCapital = 0x14,
            //
            VkEscape = 0x1B,
            //
            VkSpace = 0x20,
            VkPrior = 0x21,
            VkNext = 0x22,
            VkEnd = 0x23,
            VkHome = 0x24,
            VkLeft = 0x25,
            VkUp = 0x26,
            VkRight = 0x27,
            VkDown = 0x28,
            VkSelect = 0x29,
            VkPrint = 0x2A,
            VkExecute = 0x2B,
            VkSnapshot = 0x2C,
            VkInsert = 0x2D,
            VkDelete = 0x2E,
            VkHelp = 0x2F,
            //
            VkLwin = 0x5B,
            VkRwin = 0x5C,
            //
            VkNumpad0 = 0x60,
            VkNumpad1 = 0x61,
            VkNumpad2 = 0x62,
            VkNumpad3 = 0x63,
            VkNumpad4 = 0x64,
            VkNumpad5 = 0x65,
            VkNumpad6 = 0x66,
            VkNumpad7 = 0x67,
            VkNumpad8 = 0x68,
            VkNumpad9 = 0x69,
            VkMultiply = 0x6A,
            VkAdd = 0x6B,
            VkSeparator = 0x6C,
            VkSubtract = 0x6D,
            VkDecimal = 0x6E,
            VkDivide = 0x6F,
            //
            VkKeyA = 0x41,
            VkKeyB = 0x42,
            VkKeyC = 0x43,
            VkKeyD = 0x44,
            VkKeyE = 0x45,
            VkKeyF = 0x46,
            VkKeyG = 0x47,
            VkKeyH = 0x48,
            VkKeyI = 0x49,
            VkKeyJ = 0x4A,
            VkKeyK = 0x4B,
            VkKeyL = 0x4C,
            VkKeyM = 0x4D,
            VkKeyN = 0x4E,
            VkKeyO = 0x4F,
            VkKeyP = 0x50,
            VkKeyQ = 0x51,
            VkKeyR = 0x52,
            VkKeyS = 0x53,
            VkKeyT = 0x54,
            VkKeyU = 0x55,
            VkKeyV = 0x56,
            VkKeyW = 0x57,
            VkKeyX = 0x58,
            VkKeyY = 0x59,
            VkKeyZ = 0x5A,
            //
            VkKey0 = 0x30,
            VkKey1 = 0x31,
            VkKey2 = 0x32,
            VkKey3 = 0x33,
            VkKey4 = 0x34,
            VkKey5 = 0x35,
            VkKey6 = 0x36,
            VkKey7 = 0x37,
            VkKey8 = 0x38,
            VkKey9 = 0x39,
            //
            VkF1 = 0x70,
            VkF2 = 0x71,
            VkF3 = 0x72,
            VkF4 = 0x73,
            VkF5 = 0x74,
            VkF6 = 0x75,
            VkF7 = 0x76,
            VkF8 = 0x77,
            VkF9 = 0x78,
            VkF10 = 0x79,
            VkF11 = 0x7A,
            VkF12 = 0x7B,
            VkF13 = 0x7C,
            VkF14 = 0x7D,
            VkF15 = 0x7E,
            VkF16 = 0x7F,
            VkF17 = 0x80,
            VkF18 = 0x81,
            VkF19 = 0x82,
            VkF20 = 0x83,
            VkF21 = 0x84,
            VkF22 = 0x85,
            VkF23 = 0x86,
            VkF24 = 0x87,
            //
            VkNumlock = 0x90,
            VkScroll = 0x91,
            //
            VkOemNecEqual = 0x92,   // '=' key on numpad
            //
            VkLshift = 0xA0,
            VkRshift = 0xA1,
            VkLcontrol = 0xA2,
            VkRcontrol = 0xA3,
            VkLmenu = 0xA4,
            VkRmenu = 0xA5,
            //
            VkBrowserBack = 0xA6,
            VkBrowserForward = 0xA7,
            VkBrowserRefresh = 0xA8,
            VkBrowserStop = 0xA9,
            VkBrowserSearch = 0xAA,
            VkBrowserFavorites = 0xAB,
            VkBrowserHome = 0xAC,
            //
            VkVolumeMute = 0xAD,
            VkVolumeDown = 0xAE,
            VkVolumeUp = 0xAF,
            VkMediaNextTrack = 0xB0,
            VkMediaPrevTrack = 0xB1,
            VkMediaStop = 0xB2,
            VkMediaPlayPause = 0xB3,
            VkLaunchMail = 0xB4,
            VkLaunchMediaSelect = 0xB5,
            VkLaunchApp1 = 0xB6,
            VkLaunchApp2 = 0xB7,
            //
            VkOem1 = 0xBA,   // ';:' for US
            VkOemPlus = 0xBB,   // '+' any country
            VkOemComma = 0xBC,   // ',' any country
            VkOemMinus = 0xBD,   // '-' any country
            VkOemPeriod = 0xBE,   // '.' any country
            VkOem2 = 0xBF,   // '/?' for US
            VkOem3 = 0xC0,   // '`~' for US
                               //
            VkOem4 = 0xDB,  //  '[{' for US
            VkOem5 = 0xDC,  //  '\|' for US
            VkOem6 = 0xDD,  //  ']}' for US
            VkOem7 = 0xDE,  //  ''"' for US
            VkOem8 = 0xDF,
            //
            VkOemAx = 0xE1,  //  'AX' key on Japanese AX kbd
            VkOem102 = 0xE2,  //  "<>" or "\|" on RT 102-key kbd.
            VkIcoHelp = 0xE3,  //  Help key on ICO
            VkIco00 = 0xE4,  //  00 key on ICO
                               //
            VkProcesskey = 0xE5,
            //
            VkIcoClear = 0xE6,
            //
            VkPacket = 0xE7,
            //
            VkOemReset = 0xE9,
            VkOemJump = 0xEA,
            VkOemPa1 = 0xEB,
            VkOemPa2 = 0xEC,
            VkOemPa3 = 0xED,
            VkOemWsctrl = 0xEE,
            VkOemCusel = 0xEF,
            VkOemAttn = 0xF0,
            VkOemFinish = 0xF1,
            VkOemCopy = 0xF2,
            VkOemAuto = 0xF3,
            VkOemEnlw = 0xF4,
            VkOemBacktab = 0xF5,
            //
            VkAttn = 0xF6,
            VkCrsel = 0xF7,
            VkExsel = 0xF8,
            VkEreof = 0xF9,
            VkPlay = 0xFA,
            VkZoom = 0xFB,
            VkNoname = 0xFC,
            VkPa1 = 0xFD,
            VkOemClear = 0xFE,
            None
        }

        /// <summary>
        /// imported method which allows you to listen for a keypress
        /// </summary>
        /// <param name="vKey"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(VirtualKeyStates vKey);

        /// <summary>
        /// imported method which returns the current position of the mouse cursor
        /// </summary>
        /// <param name="lpPoint"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out Point lpPoint);
    }
}