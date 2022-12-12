using System;
using System.Collections.Generic;
using static DolphinScript.Classes.Backend.WinApi;
using static DolphinScript.Classes.Backend.RandomNumber;
using static DolphinScript.Classes.Backend.WindowControl;

namespace DolphinScript.Classes.Backend
{
    /// <summary>
    /// This class contains methods which generally return point values
    /// there are exceptions for the RECT methods and list of point method.
    /// </summary>
    internal class PointReturns
    {
        /// <summary>
        /// Returns the center point of a given rectangle area
        /// </summary>
        public static Point FindAreaCenter(Point p1, Point p2)
        {
            // will store the center point we will be returning
            //
            Point temp;

            // work out mid points of x and y individually
            //
            temp.X = (p1.X + p2.X) / 2;
            temp.Y = (p1.Y + p2.Y) / 2;
            
            // return the centerpoint of the area
            //
            return temp;
        }

        /// <summary>
        /// Returns a random point from a given rectangle area (two points passed as parameters)
        /// </summary>
        /// <param name="topleftPoint"></param>
        /// <param name="bottomRightPoint"></param>
        /// <returns></returns>
        public static Point GetRandomPointInArea(Point topleftPoint, Point bottomRightPoint)
        {
            // will store a random point in the area
            //
            Point temp;

            // randomise x and y coordinate using the area bounds as maximum random number
            //
            temp.X = GetRandomNumber(topleftPoint.X, bottomRightPoint.X);
            temp.Y = GetRandomNumber(topleftPoint.Y, bottomRightPoint.Y);

            // return the randomised point
            //
            return temp;
        }

        /// <summary>
        /// Returns a random point from a given rectangle area
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        public static Point GetRandomPointInArea(Rect area)
        {
            // will store a random point in the area
            //
            Point temp;

            // randomise x and y coordinate using the area bounds as maximum random number
            //
            temp.X = GetRandomNumber(area.Left, area.Right);
            temp.Y = GetRandomNumber(area.Top, area.Bottom);

            // return the randomised point
            //
            return temp;
        }

        /// <summary>
        /// returns the current position of the cursor
        /// </summary>
        /// <returns></returns>
        public static Point GetCursorPosition()
        {
            // gets the position of the cursor
            //
            GetCursorPos(out var cursorPos);

            // returns the cursor to the call
            //
            return cursorPos;
        }

        /// <summary>
        /// gets the current position of the cursor inside of a window
        /// </summary>
        /// <param name="window"></param>
        /// <returns></returns>
        public static Point GetCursorPositionOnWindow(IntPtr window)
        {
            // will store the location of the window
            var windowBounds = new Rect();

            // gets the window location
            //
            GetWindowRect(window, ref windowBounds);

            // gets the cursor position
            //
            GetCursorPos(out var cursorPos);

            // work out the cursor position so it's relative to the window position
            //
            cursorPos.X -= windowBounds.Left;
            cursorPos.Y -= windowBounds.Top;

            // return the cursor position on the window
            //
            return cursorPos;
        }

        /// <summary>
        /// uses shift key to save an area on the screen
        /// </summary>
        /// <returns></returns>
        public static List<Point> RegisterClickArea()
        {
            // used to store the top left and bottom right of the registered area
            //

            // this is returned to the caller containing points p1 and p2
            //
            var area = new List<Point>();

            // these will be used in our registering mechanics
            //
            var areaRegistered = false;

            while (areaRegistered == false)
            {
                // listen for the shift key to start the area register
                //
                if (GetAsyncKeyState(VirtualKeyStates.VkLshift) < 0)
                {
                    // store the top left of the register area
                    //
                    Point p1;
                    GetCursorPos(out p1);

                    // add our top left point to the area list
                    //
                    area.Add(p1);

                    // now we wait here until the user releases the shift key
                    //
                    while (GetAsyncKeyState(VirtualKeyStates.VkLshift) < 0) { /*Pauses until user has let go of left shift button...*/ }

                    // when user releases shift key we register the bottom right point
                    //
                    Point p2;
                    GetCursorPos(out p2);

                    // add bottom right point to area
                    //
                    area.Add(p2);

                    // mark areaRegistered as true
                    //
                    areaRegistered = true;
                }
            }

            // return the registered area list
            //
            return area;
        }

        /// <summary>
        /// gets the position of a click area bounds inside of a window
        /// </summary>
        /// <param name="window"></param>
        /// <param name="clickArea"></param>
        /// <returns></returns>
        public static Rect GetClickAreaPositionOnWindow(IntPtr window, Rect clickArea)
        {
            // get the window location
            //
            var windowLocation = GetWindowPosition(window);

            // return the click area relative to the window position
            //
            return new Rect(
                new Point(windowLocation.Left + clickArea.Left, windowLocation.Top + clickArea.Top),
                new Point(windowLocation.Left + clickArea.Right, windowLocation.Top + clickArea.Bottom));
        }
        
        /// <summary>
        /// returns the position of a window using the handle
        /// </summary>
        /// <param name="window"></param>
        /// <returns></returns>
        public static Rect GetWindowPosition(IntPtr window)
        {
            // create a rect to store the window position
            //
            var temp = new Rect();

            // use the GetWindowRect function to get the window position
            //
            GetWindowRect(window, ref temp);

            // return the rect to the call
            //
            return temp;
        }
    }
}
