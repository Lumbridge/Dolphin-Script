using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using static DolphinScript.Core.WindowsApi.CommonTypes;

namespace DolphinScript.Core.WindowsApi
{
    public static class PInvokeReferences
    {
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

        /// <summary>
        /// we import this mouse_event method so we can use it to perform different operations using the mouse buttons
        /// </summary>
        /// <param name="dwFlags"></param>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        /// <param name="cButtons"></param>
        /// <param name="dwExtraInfo"></param>
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

        /// <summary>
        /// Imported method which allows us to set the position of the mouse cursor.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        [DllImport("User32.Dll")]
        public static extern long SetCursorPos(int x, int y);

        /// <summary>
        /// Imported method which allows us to set the position of the mouse cursor.
        /// </summary>
        /// <returns></returns>
        [DllImport("User32.Dll")]
        public static extern long SetCursorPos(Point point);
    }
}
