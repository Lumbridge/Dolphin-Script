using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using DolphinScript.Core.Classes;
using DolphinScript.Core.Interfaces;
using DolphinScript.Core.WindowsApi;

namespace DolphinScript.Core.Concrete
{
    /// <summary>
    /// This class contains functionality around process windows such as getting the title of them and bringing them to the front if needed.
    /// </summary>
    public class WindowControlService : IWindowControlService
    {
        private readonly IRandomService _randomService;

        public WindowControlService(IRandomService randomService)
        {
            _randomService = randomService;
        }

        /// <summary>
        /// gets the title of the currently active window
        /// </summary>
        /// <returns></returns>
        public string GetActiveWindowTitle()
        {
            // set the max number of characters
            //
            const int nChars = 256;

            // create a stringbuilder with max capacity
            //
            var buff = new StringBuilder(nChars);

            // get the handle of the currently active window
            //
            var handle = PInvokeReferences.GetForegroundWindow();

            // check that the window has more than 0 characters
            //
            if (PInvokeReferences.GetWindowText(handle, buff, nChars) > 0)
            {
                // return the window title as a string
                //
                return buff.ToString();
            }

            // if the window title has no characters null is returned
            //
            return null;
        }

        public IntPtr GetActiveWindowHandle()
        {
            return PInvokeReferences.GetForegroundWindow();
        }

        /// <summary>
        /// returns the title of the window handle passed in
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        public string GetWindowTitle(IntPtr handle)
        {
            // set the max number of characters
            //
            const int nChars = 256;

            // create a stringbuilder with max capacity
            //
            var buff = new StringBuilder(nChars);

            // check that the window has more than 0 characters
            //
            if (PInvokeReferences.GetWindowText(handle, buff, nChars) > 0)
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
        public bool WindowExists(string windowClass, string windowName)
        {
            // if the window name doesn't have a valid handle then we return false
            //
            return PInvokeReferences.FindWindow(windowClass, windowName) != IntPtr.Zero;
        }

        /// <summary>
        /// this function brings a selected window to the front
        /// </summary>
        /// <param name="windowClass"></param>
        /// <param name="windowName"></param>
        public void SetWindowTopMostIfExists(string windowClass, string windowName)
        {
            var handle = PInvokeReferences.FindWindow(windowClass, windowName);

            // we check if the window exists first then if it does
            //
            if (WindowExists(windowClass, windowName))
            {
                // un-minimises window
                //
                PInvokeReferences.ShowWindowAsync(handle, Constants.SwShowNormal);

                // then we set it as the foreground window
                //
                PInvokeReferences.SetForegroundWindow(handle);
            }
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

            // un-minimise window
            PInvokeReferences.ShowWindowAsync(handle, Constants.SwShowNormal);

            // sets window to front
            PInvokeReferences.SetForegroundWindow(handle);

            // delay to prevent click area errors
            Thread.Sleep(_randomService.GetRandomNumber(300,500));
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
