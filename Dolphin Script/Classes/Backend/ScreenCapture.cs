using System.Drawing;
using static DolphinScript.Classes.Backend.WinApi;

namespace DolphinScript.Classes.Backend
{
    /// <summary>
    /// This class contains methods which allow for image capture of different areas of the screen.
    /// </summary>
    class ScreenCapture
    {
        /// <summary>
        /// This method returns a screenshot of the area passed in.
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        public static Bitmap ScreenshotArea(Rect area)
        {
            // this is where we will store a snapshot of the screen
            //
            var bmpScreenshot = new Bitmap(area.Width, area.Height);

            // creates a graphics object so we can draw the screen in the bitmap (bmpScreenshot)
            //
            var g = Graphics.FromImage(bmpScreenshot);

            // copy from screen into the bitmap we created
            //
            g.CopyFromScreen(area.Left, area.Top, 0, 0, new Size(area.Width * 2, area.Height * 2));

            // return the screenshot
            //
            return bmpScreenshot;
        }

        /// <summary>
        /// This method returns a screenshot of the click area and a small area around it, the click area is marked by a transparent overlay.
        /// </summary>
        /// <returns></returns>
        public static Bitmap ScreenshotAreaWithTransparentOverlay(Rect area)
        {
            // this is where we will store a snapshot of the screen
            //
            var bmpScreenshot = new Bitmap(area.Width * 3, area.Height * 3);

            // creates a graphics object so we can draw the screen in the bitmap (bmpScreenshot)
            //
            var g = Graphics.FromImage(bmpScreenshot);

            // create a semi-transparent colour to draw over the click area
            //
            var transparentRed = Color.FromArgb(95, Color.Red);
            
            // create a brush using the semi-transparent colour we created
            //
            var customBrush = new SolidBrush(transparentRed);

            // copy from screen into the bitmap we created
            //
            g.CopyFromScreen(area.Left - area.Width, area.Top - area.Height, 0, 0, new Size(area.Width * 3, area.Height * 3));

            // draw a transparent rectangle over the actual click area
            //
            g.FillRectangle(customBrush, new RectangleF(new PointF(area.Width, area.Height), new SizeF(area.Width, area.Height)));

            // return the screenshot
            //
            return bmpScreenshot;
        }
    }
}
