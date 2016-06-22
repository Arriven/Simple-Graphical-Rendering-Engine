using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LinearMath
{
    public class Vec3 : Vector
    {
        public float x
        {
            get
            {
                return this[0];
            }
            set
            {
                this[0] = value;
            }
        }
        public float y
        {
            get
            {
                return this[1];
            }
            set
            {
                this[1] = value;
            }
        }
        public float z
        {
            get
            {
                return this[2];
            }
            set
            {
                this[2] = value;
            }
        }

        public Vec3(float x, float y, float z) : base(new float[] { x, y, z })
        {

        }
        public Vec3(float[] arr) : base(arr)
        {
            if (arr.Length != 3) throw new FormatException();
        }

        public static Vec3 operator ^(Vec3 a, Vec3 b)
        {
            float i = a.y * b.z - a.z * b.y;
            float j = a.z * b.x - a.x * b.z;
            float k = a.x * b.y - a.y * b.x;
            return new Vec3(i, j, k);
        }
        new public Matrix GetMoveMatrix()
        {
            float[,] res = new float[4, 4];
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                {
                    if (i == j) res[i, j] = 1;
                    else res[i, j] = 0;
                    if (i == 3 && j != 3) res[i, j] = this[j];
                }
            return new Matrix(res);
        }
    }
}