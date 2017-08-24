﻿using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

using static DolphinScript.Lib.Backend.WinAPI;

namespace DolphinScript.Lib.Backend
{
    class ScreenCapture
    {
        public static Bitmap ScreenshotArea(Point p1, Point p2)
        {
            int w = p2.X - p1.X,
                h = p2.Y - p1.Y;

            // this is where we will store a snapshot of the screen
            Bitmap bmpScreenshot = new Bitmap(w, h);

            // creates a graphics object so we can draw the screen in the bitmap (bmpScreenshot)
            Graphics g = Graphics.FromImage(bmpScreenshot);

            // copy from screen into the bitmap we created
            g.CopyFromScreen(p1.X, p1.Y, 0, 0, new Size(w * 2, h * 2));

            // return the screenshot
            return bmpScreenshot;
        }

        public static Bitmap ScreenshotArea(RECT area)
        {
            // this is where we will store a snapshot of the screen
            Bitmap bmpScreenshot = new Bitmap(area.Width, area.Height);

            // creates a graphics object so we can draw the screen in the bitmap (bmpScreenshot)
            Graphics g = Graphics.FromImage(bmpScreenshot);

            // copy from screen into the bitmap we created
            g.CopyFromScreen(area.Left, area.Top, 0, 0, new Size(area.Width * 2, area.Height * 2));

            // return the screenshot
            return bmpScreenshot;
        }

        public static Bitmap ScreenshotDestinationPoint(Point p1, Point p2)
        {
            int w = p2.X - p1.X,
                h = p2.Y - p2.X;

            // this is where we will store a snapshot of the screen
            Bitmap bmpScreenshot = new Bitmap(w * 3, h * 3);

            // creates a graphics object so we can draw the screen in the bitmap (bmpScreenshot)
            Graphics g = Graphics.FromImage(bmpScreenshot);

            Color transparentRed = Color.FromArgb(95, Color.Red);
            SolidBrush customBrush = new SolidBrush(transparentRed);

            // copy from screen into the bitmap we created
            g.CopyFromScreen(p1.X - w, p1.Y - h, 0, 0, new Size(w * 3, h * 3));

            g.FillRectangle(customBrush, new RectangleF(new PointF(w, h), new SizeF(w, h)));

            // return the screenshot
            return bmpScreenshot;
        }

        public class LockBitmap
        {
            Bitmap source = null;
            IntPtr Iptr = IntPtr.Zero;
            BitmapData bitmapData = null;

            public byte[] Pixels { get; set; }
            public int Depth { get; private set; }
            public int Width { get; private set; }
            public int Height { get; private set; }

            public LockBitmap(Bitmap source)
            {
                this.source = source;
            }

            public void LockBits()
            {
                try
                {
                    // get width and height of bitmap
                    //
                    Width = source.Width;
                    Height = source.Height;

                    // get total locked pixels count
                    //
                    int PixelCount = Width * Height;

                    // create rectangle to lock
                    //
                    Rectangle rect = new Rectangle(0, 0, Width, Height);

                    // get source bitmap pixel format size
                    //
                    Depth = System.Drawing.Bitmap.GetPixelFormatSize(source.PixelFormat);

                    // check if bpp (Bits Per Pixel) is 8, 24, or 32
                    //
                    if (Depth != 8 && Depth != 24 && Depth != 32)
                    {
                        throw new ArgumentException("Only 8, 24 and 32 bpp images are supported.");
                    }

                    // lock bitmap and return bitmap data
                    //
                    bitmapData = source.LockBits(rect, ImageLockMode.ReadWrite, source.PixelFormat);

                    // create byte array to copy pixel values
                    //
                    int step = Depth / 8;
                    Pixels = new byte[PixelCount * step];
                    Iptr = bitmapData.Scan0;

                    // copy data from pointer to array
                    //
                    Marshal.Copy(Iptr, Pixels, 0, Pixels.Length);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            
            public void UnlockBits()
            {
                try
                {
                    // copy data from byte array to pointer
                    //
                    Marshal.Copy(Pixels, 0, Iptr, Pixels.Length);

                    // unlock bitmap data
                    //
                    source.UnlockBits(bitmapData);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            /// <summary>
            /// Get the color of the specified pixel
            /// </summary>
            public Color GetPixel(int x, int y)
            {
                Color clr = Color.Empty;

                // get color components count
                //
                int cCount = Depth / 8;

                // get start index of the specified pixel
                //
                int i = ((y * Width) + x) * cCount;

                if (i > Pixels.Length - cCount)
                    throw new IndexOutOfRangeException();

                // for 32 bpp get Red, Green, Blue and Alpha
                //
                if (Depth == 32)
                {
                    byte b = Pixels[i];
                    byte g = Pixels[i + 1];
                    byte r = Pixels[i + 2];
                    byte a = Pixels[i + 3]; // a
                    clr = Color.FromArgb(a, r, g, b);
                }
                // for 24 bpp get Red, Green and Blue
                //
                if (Depth == 24)
                {
                    byte b = Pixels[i];
                    byte g = Pixels[i + 1];
                    byte r = Pixels[i + 2];
                    clr = Color.FromArgb(r, g, b);
                }
                // for 8 bpp get color value (Red, Green and Blue values are the same)
                //
                if (Depth == 8)
                {
                    byte c = Pixels[i];
                    clr = Color.FromArgb(c, c, c);
                }
                return clr;
            }

            /// <summary>
            /// Set the color of the specified pixel
            /// </summary>
            public void SetPixel(int x, int y, Color color)
            {
                // get color components count
                //
                int cCount = Depth / 8;

                // get start index of the specified pixel
                //
                int i = ((y * Width) + x) * cCount;

                // for 32 bpp set Red, Green, Blue and Alpha
                //
                if (Depth == 32)
                {
                    Pixels[i] = color.B;
                    Pixels[i + 1] = color.G;
                    Pixels[i + 2] = color.R;
                    Pixels[i + 3] = color.A;
                }
                // for 24 bpp set Red, Green and Blue
                //
                if (Depth == 24)
                {
                    Pixels[i] = color.B;
                    Pixels[i + 1] = color.G;
                    Pixels[i + 2] = color.R;
                }
                // for 8 bpp set color value (Red, Green and Blue values are the same)
                //
                if (Depth == 8)
                {
                    Pixels[i] = color.B;
                }
            }
        }
    }
}
