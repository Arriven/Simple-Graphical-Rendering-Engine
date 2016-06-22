using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearMath
{
    public struct Matrix
    {
        private float[] matr;
        public readonly int width;
        public readonly int height;
        public float determinant
        {
            get
            {
                if (this.width != this.height) throw new FormatException();
                if (this.width == 1 && this.height == 1) return this[0, 0];
                float det = 0;
                for (int i = 0; i < this.width; i++)
                {
                    if (i % 2 == 0) det += this[0, i] * this.Minor(0, i).determinant;
                    else det -= this[0, i] * this.Minor(0, i).determinant;
                }
                return det;
            }
        }

        public Matrix(float[] arr, int wdth, int hght)
        {
            matr = (float[])arr.Clone();
            width = wdth;
            height = hght;
        }
        public Matrix(float[,] arr)
        {
            width = arr.GetLength(0);
            height = arr.GetLength(1);
            matr = new float[width * height];
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                    matr[i + j * width] = arr[i, j];
        }
        public Matrix(Vector[] arr)
        {
            this.width = arr.Length;
            this.height = arr[0].size;
            this.matr = new float[this.width * this.height];
            for (int i = 0; i < width; i++)
            {
                if (arr[i].size != this.height) throw new FormatException();
                for (int j = 0; j < this.height; j++)
                    this.matr[i + j * this.width] = arr[i][j];
            }
        }

        public float this[int indexI, int indexJ]
        {
            get
            {
                return this.matr[indexI * this.width + indexJ];
            }
            set
            {
                this.matr[indexI * this.width + indexJ] = value;
            }
        }

        public Vector this[int index]
        {
            get
            {
                float[] arr = new float[this.height];
                for (int i = 0; i < this.height; i++)
                    arr[i] = this[i, index];
                return new Vector(arr);
            }
        }

        public static Matrix E(int size)
        {
            float[,] res = new float[size, size];
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    if (i == j) res[i, j] = 1;
                    else res[i, j] = 0;
            return new Matrix(res);
        }
        public static Matrix operator +(Matrix a, Matrix b)
        {
            if (a.height != b.height || a.width != b.width) throw new FormatException();
            Matrix result = new Matrix(new float[a.width * a.height], a.width, a.height);
            for (int i = 0; i < a.height; i++)
                for (int j = 0; j < a.width; i++)
                    result[i, j] = a[i, j] + b[i, j];
            return result;
        }
        public static Matrix operator -(Matrix a, Matrix b)
        {
            if (a.height != b.height || a.width != b.width) throw new FormatException();
            Matrix result = new Matrix(new float[a.width * a.height], a.width, a.height);
            for (int i = 0; i < a.height; i++)
                for (int j = 0; j < a.width; i++)
                    result[i, j] = a[i, j] - b[i, j];
            return result;
        }
        public static Matrix operator *(Matrix a, Matrix b)
        {
            if (a.width != b.height) throw new FormatException();
            Matrix result = new Matrix(new float[b.width * a.height], b.width, a.height);
            for (int i = 0; i < a.height; i++)
                for (int j = 0; j < b.width; j++)
                {
                    float el = 0;
                    for (int f = 0; f < a.width; f++)
                        el += a[i, f] * b[f, j];
                    result[i, j] = el;
                }
            return result;
        }
        public static Matrix operator *(Matrix a, float b)
        {
            Matrix result = new Matrix(new float[a.width * a.height], a.width, a.height);
            for (int i = 0; i < a.height; i++)
                for (int j = 0; j < a.width; j++)
                    result[i, j] = a[i, j] * b;
            return result;
        }

        public Matrix Transponated()
        {
            Matrix result = new Matrix(new float[this.width * this.height], this.height, this.width);
            float[,] res = new float[this.height, this.width];
            for (int i = 0; i < this.height; i++)
                for (int j = 0; j < this.width; j++)
                    result[j, i] = this[i, j];
            return result;
        }

        public Matrix Minor(int iM, int jM)
        {
            Matrix result = new Matrix(new float[(this.width - 1) * (this.height - 1)], this.width - 1, this.height - 1);
            for (int i = 0; i < this.height; i++)
                for (int j = 0; j < this.width; j++)
                    if (i != iM && j != jM)
                    {
                        int currI = i;
                        int currJ = j;
                        if (i > iM) currI--;
                        if (j > jM) currJ--;
                        result[currI, currJ] = this[i, j];
                    }
            return result;
        }

        public Matrix Inversed()
        {
            float det = this.determinant;
            Matrix result = new Matrix(new float[this.width * this.height], this.width, this.height);
            for (int i = 0; i < this.height; i++)
                for (int j = 0; j < this.width; j++)
                    if ((i + j) % 2 == 0)
                        result[i, j] = this.Minor(i, j).determinant;
                    else result[i, j] = -this.Minor(i, j).determinant;
            return (result * (1 / det)).Transponated();
        }

        public Matrix AddDimension()
        {
            Matrix result = new Matrix(new float[(this.width + 1) * (this.height + 1)], this.width + 1, this.height + 1);
            for (int i = 0; i < this.height; i++)
                for (int j = 0; j < this.width; j++)
                    result[i, j] = this[i, j];
            for (int i = 0; i < this.width; i++)
                result[this.height, i] = 0;
            for (int i = 0; i < this.height; i++)
                result[i, this.width] = 0;
            result[this.height, this.width] = 1;
            return result;
        }

        public float[] GetContainer()
        {
            return this.matr;
        }

        public static Matrix Parse(string s)
        {
            bool inside = false;
            bool inVec = false;
            List<string> vec = new List<string>();
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '(')
                    if (inside) { inVec = true; vec.Add("("); }
                    else inside = true;
                else if (s[i] == ')')
                    if (inside && !inVec) break;
                    else { inVec = false; vec[vec.Count - 1] += s[i]; }
                else if (inVec) vec[vec.Count - 1] += s[i];
            }
            Vector[] arr = new Vector[vec.Count];
            for (int i = 0; i < arr.Length; i++)
                arr[i] = Vector.Parse(vec[i]);
            return new Matrix(arr);
        }

        public override string ToString()
        {
            string res = "";
            for (int j = 0; j < this.height; j++)
            {
                if (j != 0) res += "\n";
                res += "[";
                for (int i = 0; i < this.width; i++)
                {
                    if (i != 0) res += ",";
                    res += this[j, i].ToString();
                }
                res += "]";
            }
            return res;
        }
    }
}
