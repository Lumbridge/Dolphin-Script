using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using static DolphinScript.Lib.Backend.WinAPI;

namespace DolphinScript.Lib.Backend
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
            StringBuilder Buff = new StringBuilder(nChars);

            // get the handle of the currently active window
            //
            IntPtr handle = GetForegroundWindow();

            // check that the window has more than 0 characters
            //
            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                // return the window title as a string
                //
                return Buff.ToString();
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
            StringBuilder Buff = new StringBuilder(nChars);

            // check that the window has more than 0 characters
            //
            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                // return the window title as a string
                //
                return Buff.ToString();
            }

            // if the window title has no characters null is returned
            //
            return null;
        }

        /// <summary>
        /// checks if a window exists by class and windowname
        /// </summary>
        /// <param name="WindowClass"></param>
        /// <param name="WindowName"></param>
        /// <returns></returns>
        public static bool WindowExists(string WindowClass, string WindowName)
        {
            // if the gamewindow handle doesn't have a valid handle then we return false
            //
            if (FindWindow(WindowClass, WindowName) == IntPtr.Zero)
                return false;
            // otherwise we return true
            //
            else
                return true;
        }

        /// <summary>
        /// this function brings a selected window to the front
        /// </summary>
        /// <param name="WindowClass"></param>
        /// <param name="WindowName"></param>
        public static void SetWindowTopMostIfExists(string WindowClass, string WindowName)
        {
            IntPtr handle = FindWindow(WindowClass, WindowName);

            // we check if the window exists first then if it does
            //
            if (WindowExists(WindowClass, WindowName))
                // then we set it as the foreground window
                //
                SetForegroundWindow(handle);
        }

        /// <summary>
        /// brings the window associated with the script event passed in to the front
        /// </summary>
        /// <param name="ev"></param>
        public static void BringEventWindowToFront(ScriptEventClasses.ScriptEvent ev)
        {
            if (GetActiveWindowTitle() != ev.WindowToClickTitle)
            {
                // un-minimises window
                //
                ShowWindowAsync(ev.WindowToClickHandle, SW_SHOWNORMAL);

                // sets window to front
                //
                SetForegroundWindow(ev.WindowToClickHandle);

                // small delay to prevent click area errors
                //
                Thread.Sleep(1);
            }
        }

        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);

        [DllImport("user32.dll")]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);
    }
}
