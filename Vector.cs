using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearMath
{
    public class Vector
    {
        protected float[] vec;
        public readonly int size;
        public float Length
        {
            get
            {
                float a = 0;
                for (int i = 0; i < this.size; i++)
                    a += this.vec[i] * this.vec[i];
                return (float)Math.Sqrt(a);
            }
        }

        public Vector(float[] arr)
        {
            size = arr.Length;
            vec = (float[])arr.Clone();
        }

        public float this[int index]
        {
            get
            {
                return this.vec[index];
            }
            set
            {
                this.vec[index] = value;
            }
        }

        public float[] GetContainer()
        {
            return this.vec;
        }
        public Matrix ToMatrix()
        {
            return new Matrix(this.vec, 1, this.size);
        }
        public Vector AddDimension()
        {
            float[] res = new float[this.size + 1];
            Array.Copy(this.vec, res, this.size);
            res[this.size] = 1;
            return new Vector(res);
        }
        public Vector RemoveDimension()
        {
            float[] res = new float[this.size - 1];
            for (int i = 0; i < res.Length; i++)
                res[i] = this.vec[i] / this.vec[this.size - 1];
            return new Vector(res);
        }
        public Matrix GetMoveMatrix()
        {
            Matrix matr = Matrix.E(this.size + 1);
            for (int i = 0; i < this.size; i++)
            {
                matr[i, this.size] = this[i];
            }
            return matr;
        }
        public void Normalize()
        {
            float length = this.Length;
            for (int i = 0; i < this.size; i++)
                this.vec[i] = this.vec[i] / length;
        }

        public static Vector operator *(Matrix a, Vector b)
        {
            Matrix res = a * b.ToMatrix();
            return new Vector(res.GetContainer());
        }
        public static float operator *(Vector a, Vector b)
        {
            if (a.size != b.size) throw new FormatException();
            float res = 0;
            for (int i = 0; i < a.size; i++)
                res += a[i] * b[i];
            return res;
        }
        public static Vector operator +(Vector a, Vector b)
        {
            if (a.size != b.size) throw new FormatException();
            float[] res = new float[a.size];
            for (int i = 0; i < a.size; i++)
                res[i] = a[i] + b[i];
            return new Vector(res);
        }
        public static Vector operator -(Vector a, Vector b)
        {
            if (a.size != b.size) throw new FormatException();
            float[] res = new float[a.size];
            for (int i = 0; i < a.size; i++)
                res[i] = a[i] - b[i];
            return new Vector(res);
        }
        public static Vector operator *(float b, Vector a)
        {
            float[] res = new float[a.size];
            for (int i = 0; i < a.size; i++)
                res[i] = a[i] * b;
            return new Vector(res);
        }
        public static Vector operator *(Vector a, float b)
        {
            float[] res = new float[a.size];
            for (int i = 0; i < a.size; i++)
                res[i] = a[i] * b;
            return new Vector(res);
        }

        public Vec3 ToVec3()
        {
            if (this.size != 3) throw new FormatException();
            return new Vec3(this.vec);
        }

        public override string ToString()
        {
            string res = "(";
            for(int i = 0; i<this.size;i++)
            {
                if (i != 0) res += ", ";
                res += this[i];
            }
            res += ")";
            return res;
        }

        public static Vector Parse(string s)
        {
            string vec = "";
            bool inside = false;
            for(int i = 0; i < s.Length; i++)
            {
                if (s[i] == '(') inside = true;
                else if (s[i] == ')') break;
                else if (inside) vec += s[i];
            }
            string[] vector = vec.Split(',');
            float[] arr = new float[vector.Length];
            for (int i = 0; i < arr.Length; i++)
                arr[i] = float.Parse(vector[i].Replace(".", ","));
            return new Vector(arr);
        }
    }
}
