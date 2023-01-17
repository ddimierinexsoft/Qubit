using QbitLib.Matricies;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace QBits.QBits
{
    public class MatrixNxN : ICloneable
    {
        private static Dictionary<int, MatrixNxN> cacheI = new Dictionary<int, MatrixNxN>();
        private static Dictionary<(int, int), MatrixNxN> cacheZ = new Dictionary<(int, int), MatrixNxN>();

        public static MatrixNxN I(int order)
        {
            MatrixNxN ret;
            if (!cacheI.TryGetValue(order, out ret))
            {
                ret = new MatrixNxN(order, order);
                Matricies.GetIdentity(ret.arr, order);
                cacheI.Add(order, ret);
            }            
            return ret;
        }

        public static MatrixNxN Zero(int rows, int columns)
        {
            MatrixNxN ret;
            if (!cacheZ.TryGetValue((rows, columns), out ret))
            {
                ret = new MatrixNxN(rows, columns);
                Matricies.GetZero(ret.arr, rows, columns);
                cacheZ.Add((rows, columns), ret);
            }
            return ret;
        }

        private Complex[] arr;
        
        public int Rows { get; }
        public int Columns { get; }

        public MatrixNxN(int rows, int columns, Complex[] arr = null) {
            this.Rows = rows;
            this.Columns = columns;
            if (arr is object && arr.Length > 0)
            {
                if (arr.Length != rows * columns) throw new ArgumentException($"Arr length must be {rows * columns}");
                this.arr = (Complex[])arr.Clone();
            }
            else
            {
                this.arr = Matricies.Allocate<Complex>(Rows, Columns);
            }
        }

        public MatrixNxN(Complex[][] M)
        {
            this.Rows = M.GetLength(0);
            this.Columns = M[0].GetLength(0);
            this.arr = Matricies.Allocate<Complex>(Rows, Columns);
            for(int r = 0; r < Rows; r++)
            {
                for(int c = 0; c < Columns; c++)
                {
                    Matricies.SetItem(arr, Rows, Columns, r, c, M[r][c]);
                }
            }
        }

        public Complex this[int r, int c]
        {
            get
            {
                return Matricies.GetItem(arr, Rows, Columns, r, c);
            }
        }

        public MatrixNxN Clone()
        {
            return new MatrixNxN(Rows, Columns, arr);
        }

        object ICloneable.Clone()
        {
            return Clone();
        }

        public Complex Det()
        {
            MatrixNxN tmp = Clone();
            return Matricies.Det(tmp.arr, Rows, Columns);
        }           
        public MatrixNxN Transpose()
        {
            MatrixNxN ret = new MatrixNxN(Columns, Rows);
            Matricies.GetTranspose(arr, Rows, Columns, ret.arr);
            return ret;
        }

        public MatrixNxN Invert()
        {
            MatrixNxN ret = new MatrixNxN(Rows, Rows);
            MatrixNxN tmp = Clone();
            Matricies.GetInverse(tmp.arr, ret.arr, Rows, Columns);
            return ret;
        }

        public override bool Equals(object obj)
        {
            return (obj is MatrixNxN M) && (Rows == M.Rows) && (Columns == M.Columns) && EqualityHelper.AreEqual(arr, M.arr);
        }

        public override string ToString()
        {
            var ret = new System.Text.StringBuilder();
            ret.Append("(\r\n");
            for(int r = 0; r < Rows; r++)
            {
                for(int c = 0; c < Columns; c++)
                {
                    ret.Append(this[r, c]);
                    ret.Append(",\t");
                }
                ret.Append("\r\n");
            }
            ret.Append(")");
            return ret.ToString();
        }

        public override int GetHashCode()
        {
            return HashCalculatorHelper.Calculate(arr);
        }

        public static bool operator ==(MatrixNxN A, MatrixNxN B)
        {
            return A.Equals(B);
        }

        public static bool operator !=(MatrixNxN A, MatrixNxN B)
        {
            return !A.Equals(B);
        }

        public static implicit operator Complex[][] (MatrixNxN m)
        {
            var ret = new Complex[m.Rows][];
            for(int r = 0; r < m.Rows; r++)
            {
                ret[r] = new Complex[m.Columns];
                for(int c = 0; c < m.Columns; c++)
                {
                    ret[r][c] = m[r, c];
                }
            }
            return ret;
        }

        public static implicit operator Complex[](MatrixNxN m)
        {
            return (Complex[])m.arr.Clone();
        }

        public static implicit operator MatrixNxN (Complex[][] m)
        {
            return new MatrixNxN(m);
        }
         
        public static MatrixNxN operator /(MatrixNxN m, Complex v)
        {
            MatrixNxN ret = m.Clone();
            Matricies.DivideByScalar(ret.arr, m.Rows, m.Columns, v);
            return ret;
        }

        public static MatrixNxN operator *(MatrixNxN m, Complex v)
        {
            MatrixNxN ret = m.Clone();
            Matricies.MultiplyByScalar(ret.arr, m.Rows, m.Columns, v);
            return ret;
        }

        public static MatrixNxN operator +(MatrixNxN m, Complex v)
        {
            MatrixNxN ret = m.Clone();
            Matricies.AddToScalar(ret.arr, m.Rows, m.Columns, v);
            return ret;
        }

        public static MatrixNxN operator -(MatrixNxN m, Complex v)
        {
            MatrixNxN ret = m.Clone();
            Matricies.SubstractScalar(ret.arr, m.Rows, m.Columns, v);
            return ret;
        }

        public static MatrixNxN operator +(MatrixNxN m)
        {
            return m;
        }

        public static MatrixNxN operator -(MatrixNxN m)
        {
            MatrixNxN ret = m.Clone();
            Matricies.GetOpposite(ret.arr, m.Rows, m.Columns);
            return ret;
        }

        public static MatrixNxN operator +(MatrixNxN a, MatrixNxN b)
        {
            MatrixNxN ret = a.Clone();
            Matricies.AddMatrix(ret.arr, b, a.Rows, a.Columns);
            return ret;
        }

        public static MatrixNxN operator -(MatrixNxN a, MatrixNxN b)
        {
            MatrixNxN ret = a.Clone();
            Matricies.SubtractMatrix(ret.arr, b, a.Rows, a.Columns);
            return ret;
        }

        public static MatrixNxN operator *(MatrixNxN A, MatrixNxN B)
        {
            if (A.Columns != B.Rows)
            {
                throw new Exception("A.Columns != B.Rows");
            }
            MatrixNxN C = new MatrixNxN(A.Rows, B.Columns);
            Matricies.MatrixProduct(C.arr, A.arr, B.arr, A.Rows, A.Columns, B.Columns);
            return C;
        }

        public static MatrixNxN operator /(MatrixNxN a, MatrixNxN b)
        {
            return a * b.Invert();
        }

        public MatrixNxN Kron(MatrixNxN B)
        {
            var A = this;
            MatrixNxN C = new MatrixNxN(A.Rows * B.Rows, A.Columns * B.Columns);
            Matricies.KronekerProduct(C.arr, A.arr, B.arr, A.Rows, A.Columns,B.Rows, B.Columns);
            return C;
        }

    }

}