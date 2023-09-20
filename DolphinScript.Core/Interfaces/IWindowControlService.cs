using DolphinScript.Core.Classes;
using System;

namespace DolphinScript.Core.Interfaces
{
    public interface IWindowControlService
    {
        string GetActiveWindowTitle();
        string GetWindowTitle(IntPtr handle);
        bool WindowExists(string windowClass, string windowName);
        void SetWindowTopMostIfExists(string windowClass, string windowName);
        void BringWindowToFront(IntPtr handle);
        IntPtr GetActiveWindowHandle();
    }
}