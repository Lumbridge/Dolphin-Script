using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DolphinScript.Classes.Backend
{
    /// <summary>
    /// This class is used to mainly control bitmap data so we can use it in arrays and loops quickly.
    /// </summary>
    public class LockBitmap
    {
        private readonly Bitmap _source;
        IntPtr _iptr = IntPtr.Zero;
        BitmapData _bitmapData;

        public byte[] Pixels { get; set; }
        public int Depth { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public LockBitmap(Bitmap source)
        {
            this._source = source;
        }

        public void LockBits()
        {
            try
            {
                // get width and height of bitmap
                //
                Width = _source.Width;
                Height = _source.Height;

                // get total locked pixels count
                //
                var pixelCount = Width * Height;

                // create rectangle to lock
                //
                var rect = new Rectangle(0, 0, Width, Height);

                // get source bitmap pixel format size
                //
                Depth = Image.GetPixelFormatSize(_source.PixelFormat);

                // check if bpp (Bits Per Pixel) is 8, 24, or 32
                //
                if (Depth != 8 && Depth != 24 && Depth != 32)
                {
                    throw new ArgumentException("Only 8, 24 and 32 bpp images are supported.");
                }

                // lock bitmap and return bitmap data
                //
                _bitmapData = _source.LockBits(rect, ImageLockMode.ReadWrite, _source.PixelFormat);

                // create byte array to copy pixel values
                //
                var step = Depth / 8;
                Pixels = new byte[pixelCount * step];
                _iptr = _bitmapData.Scan0;

                // copy data from pointer to array
                //
                Marshal.Copy(_iptr, Pixels, 0, Pixels.Length);
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
                Marshal.Copy(Pixels, 0, _iptr, Pixels.Length);

                // unlock bitmap data
                //
                _source.UnlockBits(_bitmapData);
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
            var clr = Color.Empty;

            // get color components count
            //
            var cCount = Depth / 8;

            // get start index of the specified pixel
            //
            var i = ((y * Width) + x) * cCount;

            if (i > Pixels.Length - cCount)
                throw new IndexOutOfRangeException();

            // for 32 bpp get Red, Green, Blue and Alpha
            //
            if (Depth == 32)
            {
                var b = Pixels[i];
                var g = Pixels[i + 1];
                var r = Pixels[i + 2];
                var a = Pixels[i + 3]; // a
                clr = Color.FromArgb(a, r, g, b);
            }
            // for 24 bpp get Red, Green and Blue
            //
            if (Depth == 24)
            {
                var b = Pixels[i];
                var g = Pixels[i + 1];
                var r = Pixels[i + 2];
                clr = Color.FromArgb(r, g, b);
            }
            // for 8 bpp get color value (Red, Green and Blue values are the same)
            //
            if (Depth == 8)
            {
                var c = Pixels[i];
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
            var cCount = Depth / 8;

            // get start index of the specified pixel
            //
            var i = ((y * Width) + x) * cCount;

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
