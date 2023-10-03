using DolphinScript.Core.Constants;
using DolphinScript.Core.Interfaces;
using DolphinScript.Core.WindowsApi;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace DolphinScript.Core.Concrete
{
    /// <summary>
    /// This class contains functionality around process windows such as getting the title of them and bringing them to the front if needed.
    /// </summary>
    public class WindowControlService : IWindowControlService
    {
        /// <summary>
        /// gets the title of the currently active window
        /// </summary>
        /// <returns></returns>
        public string GetActiveWindowTitle()
        {
            // set the max number of characters
            const int nChars = 256;

            // create a string builder with max capacity
            var buff = new StringBuilder(nChars);

            // get the handle of the currently active window
            var handle = PInvokeReferences.GetForegroundWindow();

            // check that the window has more than 0 characters
            if (PInvokeReferences.GetWindowText(handle, buff, nChars) > 0)
            {
                // return the window title as a string
                return buff.ToString();
            }

            // if the window title has no characters null is returned
            return null;
        }

        public IntPtr GetActiveWindowHandle()
        {
            return PInvokeReferences.GetForegroundWindow();
        }

        public string GetProcessName(IntPtr handle)
        {
            int pid = PInvokeReferences.GetWindowProcessId(handle);

            if (pid == 0)
            {
                return string.Empty;
            }

            Process p = Process.GetProcessById(pid);

            return Path.GetFileName(p.MainModule?.FileName);
        }

        /// <summary>
        /// returns the title of the window handle passed in
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        public string GetWindowTitle(IntPtr handle)
        {
            // set the max number of characters
            const int nChars = 256;

            // create a string builder with max capacity
            var buff = new StringBuilder(nChars);

            // check that the window has more than 0 characters
            if (PInvokeReferences.GetWindowText(handle, buff, nChars) > 0)
            {
                // return the window title as a string
                return buff.ToString();
            }

            // if the window title has no characters null is returned
            return null;
        }

        /// <summary>
        /// checks if a window exists by class and window name
        /// </summary>
        /// <param name="windowName"></param>
        /// <returns></returns>
        public IntPtr GetWindowHandleByName(string windowName)
        {
            // if the window name doesn't have a valid handle then we return false
            return PInvokeReferences.FindWindow(null, windowName);
        }

        /// <summary>
        /// this function brings a selected window to the front
        /// </summary>
        /// <param name="windowClass"></param>
        /// <param name="windowName"></param>
        public void SetWindowTopMostIfExists(string windowName)
        {
            var handle = GetWindowHandleByName(windowName);

            if (handle == IntPtr.Zero)
            {
                return;
            }

            if (PInvokeReferences.IsIconic(handle))
            {
                PInvokeReferences.ShowWindowAsync(handle, WinApiConstants.SW_RESTORE);
            }

            // then we set it as the foreground window
            PInvokeReferences.SetForegroundWindow(handle);
        }

        /// <summary>
        /// brings the window associated with the script event passed in to the front
        /// </summary>
        /// <param name="handle"></param>
        public void BringWindowToFront(IntPtr handle)
        {
            // if the window is already top most we can return
            if (GetActiveWindowHandle() == handle) 
                return;

            if (PInvokeReferences.IsIconic(handle))
            {
                PInvokeReferences.ShowWindowAsync(handle, WinApiConstants.SW_RESTORE);
            }

            // then we set it as the foreground window
            PInvokeReferences.SetForegroundWindow(handle);

            // delay to prevent click area errors
            Thread.Sleep(DelayConstants.SwitchWindowWaitMs);
        }

        public IntPtr GetWindowHandle(string windowTitle)
        {
            foreach (Process pList in Process.GetProcesses())
            {
                if (pList.MainWindowTitle == windowTitle)
                {
                    return pList.MainWindowHandle;
                }
            }

            return IntPtr.Zero;
        }

        public CommonTypes.Rect GetWindowLocation(IntPtr handle)
        {
            var rect = new CommonTypes.Rect();
            PInvokeReferences.GetWindowRect(handle, ref rect);
            return rect;
        }
    }
}
