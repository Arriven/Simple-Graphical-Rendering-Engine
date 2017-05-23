using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FrontEnd
{
    class BMP : GameEngine.ICanvas
    {
        public BMP(uint width, uint height)
        {
            m_width = width;
            m_height = height;
            m_data = new GameEngine.Color[m_width * m_height];
        }
        public GameEngine.Color this[uint x, uint y]
        {
            get
            {
                if (x >= m_width || y >= m_height)
                {
                    throw new ArgumentOutOfRangeException();
                }
                return m_data[x + m_width * y];
            }
            set
            {
                if (x >= m_width || y >= m_height)
                {
                    throw new ArgumentOutOfRangeException();
                }
                m_data[x + m_width * y] = value;
            }
        }

        public GameEngine.Color this[float x, float y]
        {
            get
            {
                uint px = (uint)((x + 1) * m_width / 2);
                px = px < m_width ? px : m_width - 1;
                uint py = (uint)((y + 1) * m_height / 2);
                py = py < m_height ? py : m_height - 1;
                return this[px, py];
            }
            set
            {
                uint px = (uint)((x + 1) * m_width / 2);
                px = px < m_width ? px : m_width - 1;
                uint py = (uint)((y + 1) * m_height / 2);
                py = py < m_height ? py : m_height - 1;
                this[px, py] = value;
            }
        }

        public uint Width
        {
            get
            {
                return m_width;
            }
        }

        public uint Height
        {
            get
            {
                return m_height;
            }
        }

        private GameEngine.Color[] m_data;
        private readonly uint m_width;
        private readonly uint m_height;

        public unsafe BMP(Bitmap bmp)
        {
            System.Drawing.Imaging.BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
                System.Drawing.Imaging.ImageLockMode.ReadWrite, bmp.PixelFormat);

            bool hasA = Math.Abs(bmpData.Stride) / bmp.Width == 4;

            int bytes = Math.Abs(bmpData.Stride) * bmp.Height;
            byte* rgbValues = (byte*)bmpData.Scan0;
            if (hasA)
                for (int counter = 0; counter < bytes; counter += 4)
                {
                    this.m_data[counter / 4].B = rgbValues[counter];
                    this.m_data[counter / 4].G = rgbValues[counter + 1];
                    this.m_data[counter / 4].R = rgbValues[counter + 2];
                    this.m_data[counter / 4].A = rgbValues[counter + 3];
                }
            else
                for (int counter = 0; counter < bytes; counter += 3)
                {
                    this.m_data[counter / 4].B = rgbValues[counter];
                    this.m_data[counter / 4].G = rgbValues[counter + 1];
                    this.m_data[counter / 4].R = rgbValues[counter + 2];
                    this.m_data[counter / 4].A = 0;
                }
            bmp.UnlockBits(bmpData);
        }

        public unsafe Bitmap ToBmp()
        {
            Bitmap bmp = new Bitmap((int)this.Width, (int)this.Height);
            System.Drawing.Imaging.BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, (int)this.Width, (int)this.Height),
                System.Drawing.Imaging.ImageLockMode.ReadWrite, bmp.PixelFormat);

            int bytes = Math.Abs(bmpData.Stride) * bmp.Height;
            byte* rgbValues = (byte*)bmpData.Scan0;


            for (int counter = 0; counter < bytes; counter += 4)
            {
                Color color = Color.FromArgb(
                    this.m_data[(counter / 4)].A,
                    this.m_data[(counter / 4)].R,
                    this.m_data[(counter / 4)].G,
                    this.m_data[(counter / 4)].B
                    );
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
