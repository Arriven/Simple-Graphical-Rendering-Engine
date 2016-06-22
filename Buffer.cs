using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BmpEditor
{
    public class Buffer<T>
    {
        public readonly int width;
        public readonly int height;
        protected T[] data;
        public Buffer(int wdth, int hght)
        {
            this.width = wdth;
            this.height = hght;
            this.data = new T[this.width * this.height];
        }
        public T this[int x, int y]
        {
            get
            {
                if (x < 0 || x >= width || y < 0 || y >= height) throw new IndexOutOfRangeException();
                return this.data[x + y * this.width];
            }
            set
            {
                if (x < 0 || x >= width || y < 0 || y >= height) throw new IndexOutOfRangeException();
                this.data[x + y * this.width] = value;
            }
        }
    }
}
