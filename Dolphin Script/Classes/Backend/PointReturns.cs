using System;
using System.Collections.Generic;

using static DolphinScript.Lib.Backend.WinAPI;
using static DolphinScript.Lib.Backend.RandomNumber;
using static DolphinScript.Lib.Backend.WindowControl;
using static DolphinScript.Lib.Backend.GlobalVariables;

namespace DolphinScript.Lib.Backend
{
    class PointReturns
    {
        /// <summary>
        /// converts a config string to a point object
        /// </summary>
        /// <param name="ConfigString"></param>
        /// <returns></returns>
        static public POINT ConfigStringToPOINT(string ConfigString)
        {
            return new POINT(
                int.Parse(GetSubstringByString(Alphabet[0], Alphabet[1], ConfigString)),
                int.Parse(GetSubstringByString(Alphabet[1], Alphabet[2], ConfigString))
                );
        }

        /// <summary>
        /// converts a config string to a rect object
        /// </summary>
        /// <param name="ConfigString"></param>
        /// <returns></returns>
        static public RECT ConfigStringToRECT(string ConfigString)
        {
             return new RECT(
                int.Parse(GetSubstringByString(Alphabet[0], Alphabet[1], ConfigString)),
                int.Parse(GetSubstringByString(Alphabet[1], Alphabet[2], ConfigString)),
                int.Parse(GetSubstringByString(Alphabet[2], Alphabet[3], ConfigString)),
                int.Parse(GetSubstringByString(Alphabet[3], Alphabet[4], ConfigString))
                );
        }

        /// <summary>
        /// Returns the center point of a given rectangle area
        /// </summary>
        static public POINT FindAreaCenter(POINT p1, POINT p2)
        {
            // will store the center point we will be returning
            //
            POINT temp;

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
        /// <param name="TopleftPoint"></param>
        /// <param name="BottomRightPoint"></param>
        /// <returns></returns>
        static public POINT GetRandomPointInArea(POINT TopleftPoint, POINT BottomRightPoint)
        {
            // will store a random point in the area
            //
            POINT temp;

            // randomise x and y coordinate using the area bounds as maximum random number
            //
            temp.X = GetRandomNumber(TopleftPoint.X, BottomRightPoint.X);
            temp.Y = GetRandomNumber(TopleftPoint.Y, BottomRightPoint.Y);

            // return the randomised point
            //
            return temp;
        }

        /// <summary>
        /// Returns a random point from a given rectangle area
        /// </summary>
        /// <param name="Area"></param>
        /// <returns></returns>
        static public POINT GetRandomPointInArea(RECT Area)
        {
            // will store a random point in the area
            //
            POINT temp;

            // randomise x and y coordinate using the area bounds as maximum random number
            //
            temp.X = GetRandomNumber(Area.Left, Area.Right);
            temp.Y = GetRandomNumber(Area.Top, Area.Bottom);

            // return the randomised point
            //
            return temp;
        }

        /// <summary>
        /// returns the current position of the cursor
        /// </summary>
        /// <returns></returns>
        public static POINT GetCursorPosition()
        {
            // will store the location of the cursor
            //
            POINT CursorPos;

            // gets the position of the cursor
            //
            GetCursorPos(out CursorPos);

            // returns the cursor to the call
            //
            return CursorPos;
        }

        /// <summary>
        /// gets the current position of the cursor inside of a window
        /// </summary>
        /// <param name="Window"></param>
        /// <returns></returns>
        public static POINT GetCursorPositionOnWindow(IntPtr Window)
        {
            // will store the position of the cursor
            //
            POINT CursorPos;

            // will store the location of the window
            RECT WindowBounds = new RECT();

            // gets the window location
            //
            GetWindowRect(Window, ref WindowBounds);

            // gets the cursor position
            //
            GetCursorPos(out CursorPos);

            // work out the cursor position so it's relative to the window position
            //
            CursorPos.X -= WindowBounds.Left;
            CursorPos.Y -= WindowBounds.Top;

            // reutrn the cursor position on the window
            //
            return CursorPos;
        }

        /// <summary>
        /// gets the position of a click area bounds inside of a window
        /// </summary>
        /// <param name="Window"></param>
        /// <param name="ClickArea"></param>
        /// <returns></returns>
        public static RECT GetClickAreaPositionOnWindow(IntPtr Window, RECT ClickArea)
        {
            // get the window location
            //
            RECT WindowLocation = GetWindowPosition(Window);

            // return the click area relative to the window position
            //
            return new RECT(
                new POINT(WindowLocation.Left + ClickArea.Left, WindowLocation.Top + ClickArea.Top),
                new POINT(WindowLocation.Left + ClickArea.Right, WindowLocation.Top + ClickArea.Bottom));
        }

        /// <summary>
        /// uses shift key to save an area on the screen
        /// </summary>
        /// <returns></returns>
        static public List<POINT> RegisterClickArea()
        {
            // used to store the top left and bottom right of the registered area
            //
            POINT p1, p2;

            // this is returned to the caller containing points p1 and p2
            //
            List<POINT> area = new List<POINT>();

            // these will be used in our registering mechanics
            //
            bool areaRegistered = false;

            while (areaRegistered == false)
            {
                // listen for the shift key to start the area register
                //
                if (GetAsyncKeyState(VirtualKeyStates.VK_LSHIFT) < 0 && areaRegistered == false)
                {
                    // store the top left of the register area
                    //
                    GetCursorPos(out p1);

                    // add our top left point to the area list
                    //
                    area.Add(p1);

                    // now we wait here until the user releases the shift key
                    //
                    while (GetAsyncKeyState(VirtualKeyStates.VK_LSHIFT) < 0) { /*Pauses until user has let go of left shift button...*/ }

                    // when user releases shift key we register the bottom right point
                    //
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
        /// returns the position of a window using the handle
        /// </summary>
        /// <param name="Window"></param>
        /// <returns></returns>
        public static RECT GetWindowPosition(IntPtr Window)
        {
            // create a rect to store the window position
            //
            RECT temp = new RECT();

            // use the GetWindowRect function to get the window position
            //
            GetWindowRect(Window, ref temp);

            // return the rect to the call
            //
            return temp;
        }
    }
}
