using DolphinScript.Core.WindowsApi;
using System.Drawing;

namespace DolphinScript.Core.Interfaces
{
    public interface IScreenCaptureService
    {
        Bitmap ScreenshotArea(CommonTypes.Rect area);
        Bitmap ScreenshotAreaWithTransparentOverlay(CommonTypes.Rect area);
        Bitmap ResizeImage(Image image, int width, int height);
    }
}