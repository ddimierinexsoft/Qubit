using System;
using System.Numerics;

namespace QBits.QBits
{
    public class Matrix2x2
    {
        public static Matrix2x2 I = new Matrix2x2(1, 0, 0, 1);
        public static Matrix2x2 Zero = new Matrix2x2(0, 0, 0, 0);

        private Complex[] arr = new[] { Complex.Zero, Complex.Zero, Complex.Zero, Complex.Zero };
        
        public int Rows => 2;
        public int Columns => 2;
        
        public Matrix2x2() { }
        public Matrix2x2(Complex[] items) : this(items[0], items[1], items[2], items[3]) { }
        public Matrix2x2(Complex[][] rows) : this(rows[0][0], rows[0][1], rows[1][0], rows[1][1]) { }
        public Matrix2x2(Complex a00, Complex a01, Complex a10, Complex a11)
        {
            arr = new[] { a00, a01, a10, a11 };
        }
        public Matrix2x2(Matrix2x2 m)
        {
            arr = (Complex[]) m.arr.Clone(); 
        }

        public Complex this[int r, int c]
        {
            get
            {
                return arr[r * 2 + c];
            }
        }

        public Complex Det()
        {
            return this[0, 0] * this[1, 1] - this[0, 1] * this[1, 0];
        }

        public Matrix2x2 Transpose()
        {
            return new Matrix2x2(this[0, 0], this[1, 0], this[0, 1], this[1, 1]);
        }

        public Matrix2x2 Invert()
        {
            var d = Det();
            if (d == Complex.Zero) throw new DivideByZeroException("Singular matrix");
            return new Matrix2x2(this[1, 1], -this[0, 1], -this[1, 0], this[0, 0]) / d;
        }

        public override bool Equals(object obj)
        {
            return (obj is Matrix2x2 M) && EqualityHelper.AreEqual(M[0, 0], this[0, 0]) && EqualityHelper.AreEqual(M[0, 1], this[0, 1]) && EqualityHelper.AreEqual(M[1, 0], this[1, 0]) && EqualityHelper.AreEqual(M[1, 1], this[1, 1]);
        }

        public override string ToString()
        {
            return $"(\r\n{this[0, 0]}, {this[0, 1]}\r\n{this[1, 0]}, {this[1, 1]}\r\n)";
        }

        public override int GetHashCode()
        {
            return this[0, 0].GetHashCode() ^ this[0, 1].GetHashCode() ^ this[1, 0].GetHashCode() ^ this[1, 1].GetHashCode();
        }

        public static bool operator ==(Matrix2x2 A, Matrix2x2 B)
        {
            return A.Equals(B);
        }

        public static bool operator !=(Matrix2x2 A, Matrix2x2 B)
        {
            return !A.Equals(B);
        }

        public static implicit operator Complex[][] (Matrix2x2 m)
        {
            return new Complex[][] { new Complex[] { m[0, 0], m[0, 1] }, new Complex[] { m[1, 0], m[1, 1] } };
        }

        public static implicit operator Complex[](Matrix2x2 m)
        {
            return new Complex[] { m[0, 0], m[0, 1], m[1, 0], m[1, 1] };
        }

        public static implicit operator Matrix2x2 (Complex[][] m)
        {
            return new Matrix2x2(m);
        }

        public static implicit operator Matrix2x2 (Complex[] m)
        {
            return new Matrix2x2(m);
        }

        public static Matrix2x2 operator /(Matrix2x2 m, Complex v)
        {
            return new Matrix2x2(m[0, 0] / v, m[0, 1] / v, m[1, 0] / v, m[1, 1] / v);
        }

        public static Matrix2x2 operator *(Matrix2x2 m, Complex v)
        {
            return new Matrix2x2(m[0, 0] * v, m[0, 1] * v, m[1, 0] * v, m[1, 1] * v);
        }

        public static Matrix2x2 operator +(Matrix2x2 m, Complex v)
        {
            return new Matrix2x2(m[0, 0] + v, m[0, 1] + v, m[1, 0] + v, m[1, 1] + v);
        }

        public static Matrix2x2 operator -(Matrix2x2 m, Complex v)
        {
            return new Matrix2x2(m[0, 0] - v, m[0, 1] - v, m[1, 0] - v, m[1, 1] - v);
        }

        public static Matrix2x2 operator +(Matrix2x2 m)
        {
            return m;
        }

        public static Matrix2x2 operator -(Matrix2x2 m)
        {
            return new Matrix2x2(-m[0, 0], -m[0, 1], -m[1, 0], -m[1, 1]);
        }

        public static Matrix2x2 operator +(Matrix2x2 a, Matrix2x2 b)
        {
            return new Matrix2x2(a[0, 0] + b[0, 0], a[0, 1] + b[0, 1], a[1, 0] + b[1, 0], a[1, 1] + b[1,1]);
        }

        public static Matrix2x2 operator -(Matrix2x2 a, Matrix2x2 b)
        {
            return new Matrix2x2(a[0, 0] - b[0, 0], a[0, 1] - b[0, 1], a[1, 0] - b[1, 0], a[1, 1] - b[1, 1]);
        }

        public static Matrix2x2 operator *(Matrix2x2 a, Matrix2x2 b)
        {
            var c00 = a[0, 0] * b[0, 0] + a[0, 1] * b[1, 0];
            var c01 = a[0, 0] * b[0, 1] + a[0, 1] * b[1, 1];
            var c10 = a[1, 0] * b[0, 0] + a[1, 1] * b[1, 0];
            var c11 = a[1, 0] * b[0, 1] + a[1, 1] * b[1, 1];
            return new Matrix2x2(c00, c01, c10, c11);
        }

        public static Matrix2x2 operator /(Matrix2x2 a, Matrix2x2 b)
        {
            return a * b.Invert();
        }

        public static Bra operator *(Bra b, Matrix2x2 M)
        {
            return new Bra(b[0] * M[0, 0] + b[1] * M[1, 0], b[0] * M[0, 1] + b[1] * M[1, 1]);
        }

        public static Ket operator *(Matrix2x2 M,  Ket k)
        {
            return new Ket(k[0] * M[0, 0] + k[1] * M[0, 1], k[0] * M[1, 0] + k[1] * M[1, 1]);
        }
    }

}