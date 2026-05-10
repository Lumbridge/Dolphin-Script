using DolphinScript.Core.WindowsApi;
using System;

namespace DolphinScript.Models
{
    public sealed class SelectionCompletedEventArgs : EventArgs
    {
        public SelectionCompletedEventArgs(CommonTypes.Rect relativeBounds, CommonTypes.Rect screenBounds)
        {
            RelativeBounds = relativeBounds;
            ScreenBounds = screenBounds;
        }

        public CommonTypes.Rect RelativeBounds { get; }

        public CommonTypes.Rect ScreenBounds { get; }
    }
}