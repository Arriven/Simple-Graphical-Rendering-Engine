using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BmpEditor
{
    public class Img : Buffer<Color>
    {
        public Img(int wdth, int hght) : base(wdth, hght)
        {

        }
        public unsafe Img(Bitmap bmp) : base(bmp.Width, bmp.Height)
        {
            System.Drawing.Imaging.BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
                System.Drawing.Imaging.ImageLockMode.ReadWrite, bmp.PixelFormat);

            bool hasA = Math.Abs(bmpData.Stride) / bmp.Width == 4;

            int bytes = Math.Abs(bmpData.Stride) * bmp.Height;
            byte* rgbValues = (byte*)bmpData.Scan0;
            if (hasA)
                for (int counter = 0; counter < bytes; counter += 4)
                {
                    byte B = rgbValues[counter];
                    byte G = rgbValues[counter + 1];
                    byte R = rgbValues[counter + 2];
                    byte A = rgbValues[counter + 3];
                    this.data[counter / 4] = Color.FromArgb(A, R, G, B);
                }
            else
                for (int counter = 0; counter < bytes; counter += 3)
                {
                    byte B = rgbValues[counter];
                    byte G = rgbValues[counter + 1];
                    byte R = rgbValues[counter + 2];
                    this.data[counter / 3] = Color.FromArgb(R, G, B);
                }
            bmp.UnlockBits(bmpData);
        }

        public unsafe Bitmap ToBmp()
        {
            Bitmap bmp = new Bitmap(this.width, this.height);
            System.Drawing.Imaging.BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, this.width, this.height),
                System.Drawing.Imaging.ImageLockMode.ReadWrite, bmp.PixelFormat);

            int bytes = Math.Abs(bmpData.Stride) * bmp.Height;
            byte* rgbValues = (byte*)bmpData.Scan0;


            for (int counter = 0; counter < bytes; counter += 4)
            {
                Color color = this.data[(counter / 4)];
                rgbValues[counter] = color.B;
                rgbValues[counter + 1] = color.G;
                rgbValues[counter + 2] = color.R;
                rgbValues[counter + 3] = color.A;
            }

            bmp.UnlockBits(bmpData);
            return bmp;
        }
    }
}
