using DolphinScript.Core.Interfaces;
using DolphinScript.Core.WindowsApi;
using System;
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
            Point temp = new Point
            {
                // work out mid points of x and y individually
                X = (p1.X + p2.X) / 2,
                Y = (p1.Y + p2.Y) / 2
            };

            // return the center point of the area
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
            Point temp = new Point
            {
                // randomise x and y coordinate using the area bounds as maximum random number
                X = _randomService.GetRandomNumber(topleftPoint.X, bottomRightPoint.X),
                Y = _randomService.GetRandomNumber(topleftPoint.Y, bottomRightPoint.Y)
            };

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
            Point temp = new Point
            {
                // randomise x and y coordinate using the area bounds as maximum random number
                X = _randomService.GetRandomNumber(area.Left, area.Right),
                Y = _randomService.GetRandomNumber(area.Top, area.Bottom)
            };

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
            cursorPos.X -= windowBounds.Left;
            cursorPos.Y -= windowBounds.Top;

            // return the cursor position on the window
            return cursorPos;
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
                new Point(windowLocation.Left + clickArea.Left, windowLocation.Top + clickArea.Top),
                new Point(windowLocation.Left + clickArea.Right, windowLocation.Top + clickArea.Bottom));
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