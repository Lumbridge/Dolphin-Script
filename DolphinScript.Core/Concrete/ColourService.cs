using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using DolphinScript.Core.Interfaces;
using DolphinScript.Core.WindowsApi;

namespace DolphinScript.Core.Concrete
{
    /// <summary>
    /// This class contains methods which have some colour functionality such as getting the colour of a particular pixel.
    /// </summary>
    public class ColourService : IColourService
    {
        private readonly IPointService _pointService;
        private readonly IScreenCaptureService _screenCaptureService;

        public ColourService(IPointService pointService, IScreenCaptureService screenCaptureService)
        {
            _pointService = pointService;
            _screenCaptureService = screenCaptureService;
        }

        /// <summary>
        /// gets the pixel colour at a point
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public Color GetColourAtPoint(Point position)
        {
            // gets a handle to the screen
            //
            var desk = GetDesktopWindow();

            // gets a device context for the screen
            //
            var dc = GetWindowDC(desk);

            // gets an unsigned int pixel at the selected position
            //
            var a = (int)GetPixel(dc, position.X, position.Y);

            // frees the device context memory
            //
            ReleaseDC(desk, dc);

            // returns a colour object of the colour at the pixel position
            //
            return Color.FromArgb(255, (a >> 0) & 0xff, (a >> 8) & 0xff, (a >> 16) & 0xff);
        }

        /// <summary>
        /// method of saving the colour under the mouse cursor when the user presses shift
        /// </summary>
        /// <returns></returns>
        public Color SaveSearchColour()
        {
            // continuously loop
            //
            while (true)
            {
                // listen for left shift key
                //
                if (PInvokeReferences.GetAsyncKeyState(CommonTypes.VirtualKeyStates.Lshift) < 0)
                {
                    // return the pixel colour under the cursor position
                    //
                    return GetColourAtPoint(_pointService.GetCursorPosition());
                }
            }
        }

        /// <summary>
        /// Determines if a colour exists in a given search area
        /// </summary>
        /// <param name="colourSearchArea"></param>
        /// <param name="searchColour"></param>
        /// <returns></returns>
        public bool ColourExistsInArea(CommonTypes.Rect colourSearchArea, int searchColour)
        {
            // create a bitmap of the search area
            //
            var b = _screenCaptureService.ScreenshotArea(colourSearchArea);

            // lock the bitmap memory
            //
            lock (b)
            {
                // create a lockbitmap object
                //
                var lockBitmap = new LockBitmap(b);

                try
                {
                    // lock the object data
                    //
                    lockBitmap.LockBits();

                    // loop through all pixels
                    //
                    for (var y = 0; y < lockBitmap.Height; y++)
                    {
                        for (var x = 0; x < lockBitmap.Width; x++)
                        {
                            // check if the search pixel matches the current pixel we're on
                            //
                            if (lockBitmap.GetPixel(x, y).ToArgb() == searchColour)
                            {
                                // no need to continue searching
                                //
                                return true;
                            }
                        }
                    }
                }
                finally
                {
                    // unlock the data
                    //
                    lockBitmap.UnlockBits();
                }
            }
            
            // we finished searching all pixels without finding a matching pixel
            //
            return false;
        }

        /// <summary>
        /// Returns a list of matching colour points found in the search area
        /// </summary>
        /// <param name="colourSearchArea"></param>
        /// <param name="searchColour"></param>
        /// <returns></returns>
        public List<Point> GetMatchingPixelList(CommonTypes.Rect colourSearchArea, int searchColour)
        {
            // this list will store the list of pixels matching the search colour
            //
            var matchingColourPixels = new List<Point>();

            // create a bitmap to use in the search process
            //
            var b = _screenCaptureService.ScreenshotArea(colourSearchArea);

            // lock the bitmap image memory
            //
            lock (b)
            {
                // create a lockbitmap object to use in the search process
                //
                var lockBitmap = new LockBitmap(b);

                try
                {
                    // lock the bitmap memory
                    //
                    lockBitmap.LockBits();

                    // loop through all pixels on the image
                    //
                    for (var y = 0; y < lockBitmap.Height; y++)
                    {
                        for (var x = 0; x < lockBitmap.Width; x++)
                        {
                            // check if the current pixel matches the search colour
                            //
                            if (lockBitmap.GetPixel(x, y).ToArgb() == searchColour)
                            {
                                // if it matches then add the pixel to the matching pixels list
                                //
                                matchingColourPixels.Add(new Point(colourSearchArea.Left + x, colourSearchArea.Top + y));
                            }
                        }
                    }
                }
                finally
                {
                    // when we're done, unlock the memory region
                    //
                    lockBitmap.UnlockBits();
                }
            }

            // return the list of matching pixels we found (if any)
            //
            return matchingColourPixels;
        }

        /// <summary>
        /// Changes the colour of all matching colour pixels on a given bitmap image
        /// </summary>
        /// <param name="b"></param>
        /// <param name="searchColour"></param>
        /// <param name="newColour"></param>
        /// <returns></returns>
        public Bitmap SetMatchingColourPixels(Bitmap b, int searchColour, Color newColour)
        {
            // create a copy of the bitmap image we're going to edit
            //
            var temp = b;
            
            // lock the bitmap memory region
            //
            lock (temp)
            {
                // create a lockbitmap object
                //
                var lockBitmap = new LockBitmap(temp);

                try
                {
                    // lock the image data
                    //
                    lockBitmap.LockBits();

                    // loop through all pixels on the image
                    //
                    for (var y = 0; y < lockBitmap.Height; y++)
                    {
                        for (var x = 0; x < lockBitmap.Width; x++)
                        {
                            // check if the current pixel colour matches the search colour
                            //
                            if (lockBitmap.GetPixel(x, y).ToArgb() == searchColour)
                            {
                                // change the matching search colour to the colour we want it to be changed to
                                //
                                lockBitmap.SetPixel(x, y, newColour);
                            }
                        }
                    }
                }
                finally
                {
                    // when we're done, unlock the memory region
                    //
                    lockBitmap.UnlockBits();
                }
            }

            // return the new bitmap with the new colour for the matching pixels
            //
            return temp;
        }

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetDesktopWindow();

        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern uint GetPixel(IntPtr dc, int x, int y);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetWindowDC(IntPtr window);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int ReleaseDC(IntPtr window, IntPtr dc);
    }
}
