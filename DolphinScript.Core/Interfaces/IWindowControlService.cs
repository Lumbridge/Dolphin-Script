using System;
using System.Collections.Generic;

namespace DolphinScript.Core.Interfaces
{
    public interface IWindowControlService
    {
        string GetProcessName(IntPtr handle);
        string GetActiveWindowTitle();
        string GetWindowTitle(IntPtr handle);
        IntPtr GetWindowHandleByName(string windowName);
        void SetWindowTopMostIfExists(string windowName);
        void BringWindowToFront(IntPtr handle);
        IntPtr GetActiveWindowHandle();
        IntPtr GetWindowHandle(string windowTitle);
    }
}