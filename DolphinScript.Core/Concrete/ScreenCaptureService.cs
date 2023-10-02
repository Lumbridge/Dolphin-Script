using DolphinScript.Core.Interfaces;
using DolphinScript.Core.WindowsApi;
using System.Drawing;

namespace DolphinScript.Core.Concrete
{
    public class ScreenCaptureService : IScreenCaptureService
    {
        /// <summary>
        /// This method returns a screenshot of the area passed in.
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        public Bitmap ScreenshotArea(CommonTypes.Rect area)
        {
            // this is where we will store a snapshot of the screen
            var bmpScreenshot = new Bitmap(area.Width, area.Height);

            // creates a graphics object so we can draw the screen in the bitmap (bmpScreenshot)
            var g = Graphics.FromImage(bmpScreenshot);

            // copy from screen into the bitmap we created
            g.CopyFromScreen(area.left, area.top, 0, 0, new Size(area.Width, area.Height));

            // return the screen shot
            return bmpScreenshot;
        }

        /// <summary>
        /// This method returns a screenshot of the click area and a small area around it, the click area is marked by a transparent overlay.
        /// </summary>
        /// <returns></returns>
        public Bitmap ScreenshotAreaWithTransparentOverlay(CommonTypes.Rect area)
        {
            // this is where we will store a snapshot of the screen
            var bmpScreenshot = new Bitmap(area.Width * 3, area.Height * 3);

            // creates a graphics object so we can draw the screen in the bitmap (bmpScreenshot)
            var g = Graphics.FromImage(bmpScreenshot);

            // create a semi-transparent colour to draw over the click area
            var transparentRed = Color.FromArgb(95, Color.Red);

            // create a brush using the semi-transparent colour we created
            var customBrush = new SolidBrush(transparentRed);

            // copy from screen into the bitmap we created
            g.CopyFromScreen(area.left - area.Width, area.top - area.Height, 0, 0, new Size(area.Width * 3, area.Height * 3));

            // draw a transparent rectangle over the actual click area
            g.FillRectangle(customBrush, new RectangleF(new PointF(area.Width, area.Height), new SizeF(area.Width, area.Height)));

            // return the screenshot
            return bmpScreenshot;
        }
    }
}
