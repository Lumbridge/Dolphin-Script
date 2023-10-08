using DolphinScript.Core.Interfaces;
using DolphinScript.Core.WindowsApi;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace DolphinScript.Core.Concrete
{
    public class PointService : IPointService
    {
        private readonly IRandomService _randomService;

        public PointService(IRandomService randomService)
        {
            _randomService = randomService;
        }

        /// <summary>
        /// Returns the center point of a given rectangle area
        /// </summary>
        public Point FindAreaCenter(Point p1, Point p2)
        {
            // will store the center point we will be returning
            Point temp = new Point();

            // work out mid points of x and y individually
            temp.X = (p1.X + p2.X) / 2;
            temp.Y = (p1.Y + p2.Y) / 2;

            // return the centerpoint of the area
            return temp;
        }

        /// <summary>
        /// Returns a random point from a given rectangle area (two points passed as parameters)
        /// </summary>
        /// <param name="topleftPoint"></param>
        /// <param name="bottomRightPoint"></param>
        /// <returns></returns>
        public Point GetRandomPointInArea(Point topleftPoint, Point bottomRightPoint)
        {
            // will store a random point in the area
            Point temp = new Point();

            // randomise x and y coordinate using the area bounds as maximum random number
            temp.X = _randomService.GetRandomNumber(topleftPoint.X, bottomRightPoint.X);
            temp.Y = _randomService.GetRandomNumber(topleftPoint.Y, bottomRightPoint.Y);

            // return the randomised point
            return temp;
        }

        /// <summary>
        /// Returns a random point from a given rectangle area
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        public Point GetRandomPointInArea(CommonTypes.Rect area)
        {
            // will store a random point in the area
            Point temp = new Point();

            // randomise x and y coordinate using the area bounds as maximum random number
            temp.X = _randomService.GetRandomNumber(area.left, area.right);
            temp.Y = _randomService.GetRandomNumber(area.top, area.bottom);

            // return the randomised point
            return temp;
        }

        public CommonTypes.Rect GetRectAroundCenterPoint(Point centerPoint, int areaSize)
        {
            return new CommonTypes.Rect(
                centerPoint.Y - areaSize,
                centerPoint.X - areaSize,
                centerPoint.Y + areaSize,
                centerPoint.X + areaSize);
        }

        /// <summary>
        /// returns the current position of the cursor
        /// </summary>
        /// <returns></returns>
        public Point GetCursorPosition()
        {
            // gets the position of the cursor
            PInvokeReferences.GetCursorPos(out var cursorPos);

            // returns the cursor to the call
            return cursorPos;
        }

        /// <summary>
        /// gets the current position of the cursor inside of a window
        /// </summary>
        /// <param name="window"></param>
        /// <returns></returns>
        public Point GetCursorPositionOnWindow(IntPtr window)
        {
            // will store the location of the window
            var windowBounds = new CommonTypes.Rect();

            // gets the window location
            PInvokeReferences.GetWindowRect(window, ref windowBounds);

            // gets the cursor position
            PInvokeReferences.GetCursorPos(out var cursorPos);

            // work out the cursor position so it's relative to the window position
            cursorPos.X -= windowBounds.left;
            cursorPos.Y -= windowBounds.top;

            // return the cursor position on the window
            return cursorPos;
        }

        /// <summary>
        /// uses shift key to save an area on the screen
        /// </summary>
        /// <returns></returns>
        public IList<Point> RegisterClickArea()
        {
            // this is returned to the caller containing points p1 and p2
            var area = new List<Point>();

            // these will be used in our registering mechanics
            var areaRegistered = false;

            while (areaRegistered == false)
            {
                // listen for the shift key to start the area register
                if (PInvokeReferences.GetAsyncKeyState(CommonTypes.VirtualKeyStates.Lshift) < 0)
                {
                    // store the top left of the register area
                    Point p1;
                    PInvokeReferences.GetCursorPos(out p1);

                    // add our top left point to the area list
                    area.Add(p1);

                    // now we wait here until the user releases the shift key
                    while (PInvokeReferences.GetAsyncKeyState(CommonTypes.VirtualKeyStates.Lshift) < 0) { /*Pauses until user has let go of left shift button...*/ }

                    // when user releases shift key we register the bottom right point
                    Point p2;
                    PInvokeReferences.GetCursorPos(out p2);

                    // add bottom right point to area
                    area.Add(p2);

                    // mark areaRegistered as true
                    areaRegistered = true;
                }
            }

            // return the registered area list
            return area;
        }

        /// <summary>
        /// gets the position of a click area bounds inside of a window
        /// </summary>
        /// <param name="window"></param>
        /// <param name="clickArea"></param>
        /// <returns></returns>
        public CommonTypes.Rect GetClickAreaPositionOnWindow(IntPtr window, CommonTypes.Rect clickArea)
        {
            // get the window location
            //
            var windowLocation = GetWindowPosition(window);

            // return the click area relative to the window position
            //
            return new CommonTypes.Rect(
                new Point(windowLocation.left + clickArea.left, windowLocation.top + clickArea.top),
                new Point(windowLocation.left + clickArea.right, windowLocation.top + clickArea.bottom));
        }

        /// <summary>
        /// returns the position of a window using the handle
        /// </summary>
        /// <param name="window"></param>
        /// <returns></returns>
        public CommonTypes.Rect GetWindowPosition(IntPtr window)
        {
            // create a rect to store the window position
            var temp = new CommonTypes.Rect();

            // use the GetWindowRect function to get the window position
            PInvokeReferences.GetWindowRect(window, ref temp);

            // return the rect to the call
            return temp;
        }
    }
}