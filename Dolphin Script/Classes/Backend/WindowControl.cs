using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using static DolphinScript.Classes.Backend.WinApi;

namespace DolphinScript.Classes.Backend
{
    /// <summary>
    /// This class contains functionality around process windows such as getting the title of them and bringing them to the front if needed.
    /// </summary>
    class WindowControl
    {
        /// <summary>
        /// gets the title of the currently active window
        /// </summary>
        /// <returns></returns>
        public static string GetActiveWindowTitle()
        {
            // set the max number of characters
            //
            const int nChars = 256;

            // create a stringbuilder with max capacity
            //
            var buff = new StringBuilder(nChars);

            // get the handle of the currently active window
            //
            var handle = GetForegroundWindow();

            // check that the window has more than 0 characters
            //
            if (GetWindowText(handle, buff, nChars) > 0)
            {
                // return the window title as a string
                //
                return buff.ToString();
            }

            // if the window title has no characters null is returned
            //
            return null;
        }

        /// <summary>
        /// returns the title of the window handle passed in
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        public static string GetWindowTitle(IntPtr handle)
        {
            // set the max number of characters
            //
            const int nChars = 256;

            // create a stringbuilder with max capacity
            //
            var buff = new StringBuilder(nChars);

            // check that the window has more than 0 characters
            //
            if (GetWindowText(handle, buff, nChars) > 0)
            {
                // return the window title as a string
                //
                return buff.ToString();
            }

            // if the window title has no characters null is returned
            //
            return null;
        }

        /// <summary>
        /// checks if a window exists by class and window name
        /// </summary>
        /// <param name="windowClass"></param>
        /// <param name="windowName"></param>
        /// <returns></returns>
        public static bool WindowExists(string windowClass, string windowName)
        {
            // if the window name doesn't have a valid handle then we return false
            //
            return FindWindow(windowClass, windowName) != IntPtr.Zero;
        }

        /// <summary>
        /// this function brings a selected window to the front
        /// </summary>
        /// <param name="windowClass"></param>
        /// <param name="windowName"></param>
        public static void SetWindowTopMostIfExists(string windowClass, string windowName)
        {
            var handle = FindWindow(windowClass, windowName);

            // we check if the window exists first then if it does
            //
            if (WindowExists(windowClass, windowName))
            {
                // un-minimises window
                //
                ShowWindowAsync(handle, SwShownormal);

                // then we set it as the foreground window
                //
                SetForegroundWindow(handle);
            }
        }

        /// <summary>
        /// brings the window associated with the script event passed in to the front
        /// </summary>
        /// <param name="ev"></param>
        public static void BringEventWindowToFront(ScriptEventClasses.ScriptEvent ev)
        {
            // if the window is already top most we can return
            if (GetActiveWindowTitle() == ev.WindowToClickTitle) 
                return;

            // un-minimise window
            //
            ShowWindowAsync(ev.WindowToClickHandle, SwShownormal);

            // sets window to front
            //
            SetForegroundWindow(ev.WindowToClickHandle);

            // small delay to prevent click area errors
            //
            Thread.Sleep(RandomNumber.GetRandomNumber(300,500));
        }

        /// <summary>
        /// imported method which allows you to set the window which is shown in the foreground
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        /// <summary>
        /// imported method which allows you to get an intptr handle of a window
        /// </summary>
        /// <param name="lpClassName"></param>
        /// <param name="lpWindowName"></param>
        /// <returns></returns>
        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        /// <summary>
        /// imported method which allows you to get the size and position of a window
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="lpRect"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, ref Rect lpRect);

        /// <summary>
        /// imported method which allows you to get the title of a window
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="text"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        /// <summary>
        /// imported method which allows you to get the handle of the window in the foreground
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        /// <summary>
        /// imported method which allows you to set the show state of a window without waiting for the operation to complete
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="nCmdShow"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);
    }
}
