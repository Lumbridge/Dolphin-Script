using System;
using System.Runtime.InteropServices;
using static DolphinScript.Lib.Backend.GlobalVariables;
using static DolphinScript.Lib.Backend.WinAPI;

namespace DolphinScript.Lib.Backend
{
    class WindowControl
    {
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);

        // checks if a window exists by class and windowname
        //
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

        // this function sets the selected window as top most
        //
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
    }
}
