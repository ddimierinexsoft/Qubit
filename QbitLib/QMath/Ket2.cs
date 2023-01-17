using System.Numerics;

namespace QBits.QBits
{
    public class Ket2
    {
        public static readonly Ket2 R00 = new Complex[] { 1, 0, 0, 0 };
        public static readonly Ket2 R01 = new Complex[] { 0, 1, 0, 0 };
        public static readonly Ket2 R10 = new Complex[] { 0, 0, 1, 0 };
        public static readonly Ket2 R11 = new Complex[] { 0, 0, 0, 1 };

        private Complex[] arr = new Complex[] { Complex.Zero, Complex.Zero, Complex.Zero, Complex.Zero };

        public int Rows => 4;
        public int Columns => 1;

        public Ket2() { }
        
        public Ket2(Ket A, Ket B)
        {
            this.arr = new[] { A[0] * B[0], A[0] * B[1], A[1] * B[0], A[1] * B[1] };
        }

        public Ket2(params Complex[] arr) {
            if (arr.Length != 4) throw new System.ArgumentException("Arr length must be 4");
            this.arr = (Complex[])arr.Clone();
        }

        public Complex this[int i]
        {
            get
            {
                return arr[i];
            }
        }

        public static implicit operator Complex[](Ket2 reg)
        {
            return (Complex[])reg.arr.Clone();
        }

        public static implicit operator Ket2 (Complex[] coefficients)
        {
            if (coefficients.Length != 4) throw new System.ArgumentException("Array must contain 4 coefficient values");
            return new Ket2 { arr = (Complex[])coefficients.Clone() };
        }

        public static Ket2 operator+(Ket2 r)
        {
            return r;
        }

        public static Ket2 operator -(Ket2 r)
        {
            return new Complex[] { -r[0], -r[1], -r[2], -r[3] };
        }

        public static Ket2 operator *(Ket2 r, Complex v)
        {
            return new Complex[] { r[0] * v, r[1] * v, r[2] * v, r[3] * v };
        }

        public static Ket2 operator *(Complex v, Ket2 r)
        {
            return new Complex[] { r[0] * v, r[1] * v, r[2] * v, r[3] * v };
        }

        public static Ket2 operator /(Ket2 r, Complex v)
        {
            return new Complex[] { r[0] / v, r[1] / v, r[2] / v, r[3] / v };
        }

        public static Ket2 operator +(Ket2 a, Ket2 b)
        {
            Ket2 ret = new Complex[] { a[0] + b[0], a[1] + b[1], a[2] + b[2], a[3] + b[3] };
            return ret / NP.sqrt(2);
        }

        public static Ket2 operator -(Ket2 a, Ket2 b)
        {
            Ket2 ret = new Complex[] { a[0] - b[0], a[1] - b[1], a[2] - b[2], a[3] - b[3] };
            return ret / NP.sqrt(2);
        }

        public override string ToString()
        {
            var ret = new System.Text.StringBuilder();
            ret.Append("|");

            ret.Append(">");
            return ret.ToString();
        }

        public override bool Equals(object obj)
        {
            return (obj is Ket2 b) && EqualityHelper.AreEqual(arr, b.arr);
        }

        public override int GetHashCode()
        {
            return HashCalculatorHelper.Calculate(arr);
        }
    }

}