using System;
using System.Drawing;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using static DolphinScript.Lib.Backend.WinAPI;
using static DolphinScript.Lib.Backend.ScreenCapture;
using static DolphinScript.Lib.Backend.PointReturns;

namespace DolphinScript.Lib.Backend
{
    class ColourEvent
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetDesktopWindow();

        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern uint GetPixel(IntPtr dc, int x, int y);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetWindowDC(IntPtr window);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int ReleaseDC(IntPtr window, IntPtr dc);

        public static Color GetColorAt(Point position)
        {
            IntPtr desk = GetDesktopWindow();
            IntPtr dc = GetWindowDC(desk);
            int a = (int)GetPixel(dc, position.X, position.Y);
            ReleaseDC(desk, dc);
            return Color.FromArgb(255, (a >> 0) & 0xff, (a >> 8) & 0xff, (a >> 16) & 0xff);
        }

        public static Color GetColorAt(int x, int y)
        {
            IntPtr desk = GetDesktopWindow();
            IntPtr dc = GetWindowDC(desk);
            int a = (int)GetPixel(dc, x, y);
            ReleaseDC(desk, dc);
            return Color.FromArgb(255, (a >> 0) & 0xff, (a >> 8) & 0xff, (a >> 16) & 0xff);
        }

        public static Color SaveSearchColour()
        {
            Color SearchColour = Color.White;
            POINT p1;
            bool ColourChosen = false;

            while (!ColourChosen)
            {
                if (GetAsyncKeyState(VirtualKeyStates.VK_LSHIFT) < 0)
                {
                    p1 = GetCursorPosition();

                    SearchColour = GetColorAt(p1.X, p1.Y);

                    ColourChosen = true;
                }
            }

            return SearchColour;
        }

        /// <summary>
        /// Determines if a colour exists in a given search area
        /// </summary>
        /// <param name="ColourSearchArea"></param>
        /// <param name="SearchColour"></param>
        /// <returns></returns>
        public static bool ColourExistsInArea(List<POINT> ColourSearchArea, int SearchColour)
        {
            POINT p1 = ColourSearchArea[0],
                    p2 = ColourSearchArea[1];

            Bitmap b = ScreenshotArea(p1, p2);

            lock (b)
            {
                LockBitmap lockBitmap = new LockBitmap(b);
                try
                {
                    lockBitmap.LockBits();

                    for (int y = 0; y < lockBitmap.Height; y++)
                    {
                        for (int x = 0; x < lockBitmap.Width; x++)
                        {
                            if (lockBitmap.GetPixel(x, y).ToArgb() == SearchColour)
                            {
                                return true;
                            }
                        }
                    }
                }
                finally
                {
                    lockBitmap.UnlockBits();
                }
            }
            return false;
        }

        /// <summary>
        /// Determines if a colour exists in a given search area
        /// </summary>
        /// <param name="ColourSearchArea"></param>
        /// <param name="SearchColour"></param>
        /// <returns></returns>
        public static bool ColourExistsInArea(RECT ColourSearchArea, int SearchColour)
        {
            POINT
                p1 = new POINT(ColourSearchArea.Left, ColourSearchArea.Top),
                p2 = new POINT(ColourSearchArea.Right, ColourSearchArea.Bottom);

            Bitmap b = ScreenshotArea(p1, p2);

            lock (b)
            {
                LockBitmap lockBitmap = new LockBitmap(b);
                try
                {
                    lockBitmap.LockBits();

                    for (int y = 0; y < lockBitmap.Height; y++)
                    {
                        for (int x = 0; x < lockBitmap.Width; x++)
                        {
                            if (lockBitmap.GetPixel(x, y).ToArgb() == SearchColour)
                            {
                                return true;
                            }
                        }
                    }
                }
                finally
                {
                    lockBitmap.UnlockBits();
                }
            }
            return false;
        }
        
        /// <summary>
        /// Returns a list of matching colour points found in the search area
        /// </summary>
        /// <param name="ColourSearchArea"></param>
        /// <param name="SearchColour"></param>
        /// <returns></returns>
        public static List<POINT> GetMatchingPixelList(List<POINT> ColourSearchArea, int SearchColour)
        {
            List<POINT> MatchingColourPixels = new List<POINT>();
            POINT MatchingPixel;

            POINT p1 = ColourSearchArea[0],
                    p2 = ColourSearchArea[1];

            Bitmap b = ScreenshotArea(p1, p2);

            lock (b)
            {
                LockBitmap lockBitmap = new LockBitmap(b);
                try
                {
                    lockBitmap.LockBits();

                    for (int y = 0; y < lockBitmap.Height; y++)
                    {
                        for (int x = 0; x < lockBitmap.Width; x++)
                        {
                            if (lockBitmap.GetPixel(x, y).ToArgb() == SearchColour)
                            {
                                MatchingPixel.X = p1.X + x;
                                MatchingPixel.Y = p1.Y + y;
                                MatchingColourPixels.Add(MatchingPixel);
                            }
                        }
                    }
                }
                finally
                {
                    lockBitmap.UnlockBits();
                }
            }
            return MatchingColourPixels;
        }

        /// <summary>
        /// Returns a list of matching colour points found in the search area
        /// </summary>
        /// <param name="ColourSearchArea"></param>
        /// <param name="SearchColour"></param>
        /// <returns></returns>
        public static List<POINT> GetMatchingPixelList(RECT ColourSearchArea, int SearchColour)
        {
            List<POINT> MatchingColourPixels = new List<POINT>();
            POINT MatchingPixel;

            POINT
                p1 = new POINT(ColourSearchArea.Left, ColourSearchArea.Top),
                p2 = new POINT(ColourSearchArea.Right, ColourSearchArea.Bottom);

            Bitmap b = ScreenshotArea(p1, p2);

            lock (b)
            {
                LockBitmap lockBitmap = new LockBitmap(b);
                try
                {
                    lockBitmap.LockBits();

                    for (int y = 0; y < lockBitmap.Height; y++)
                    {
                        for (int x = 0; x < lockBitmap.Width; x++)
                        {
                            if (lockBitmap.GetPixel(x, y).ToArgb() == SearchColour)
                            {
                                MatchingPixel.X = p1.X + x;
                                MatchingPixel.Y = p1.Y + y;
                                MatchingColourPixels.Add(MatchingPixel);
                            }
                        }
                    }
                }
                finally
                {
                    lockBitmap.UnlockBits();
                }
            }
            return MatchingColourPixels;
        }

        public static Bitmap SetMatchingColourPixels(Bitmap b, int SearchColour, Color NewColour)
        {
            var temp = b;
            lock (temp)
            {
                LockBitmap lockBitmap = new LockBitmap(temp);
                try
                {
                    lockBitmap.LockBits();

                    for (int y = 0; y < lockBitmap.Height; y++)
                    {
                        for (int x = 0; x < lockBitmap.Width; x++)
                        {
                            if (lockBitmap.GetPixel(x, y).ToArgb() == SearchColour)
                            {
                                lockBitmap.SetPixel(x, y, NewColour);
                            }
                        }
                    }
                }
                finally
                {
                    lockBitmap.UnlockBits();
                }
            }
            return temp;
        }
    }
}
