using System;
using System.Numerics;

namespace QBits.QBits
{
    public class Bra : Vect2
    {
        public override int Rows => 1;
        public override int Columns => 2;

        public Bra() { }
        public Bra(Complex[] items) : this(items[0], items[1]) { }
        public Bra(Complex a0, Complex a1) : base(a0, a1) { }

        public Ket Transpose()
        {
            return new Ket(this[0], this[1]);
        }


        public override bool Equals(object obj)
        {
            return (obj is Bra b) && EqualityHelper.AreEqual(b[0], this[0]) && EqualityHelper.AreEqual(b[1], this[1]);
        }

        public override string ToString()
        {
            return $"<({this[0]}, {this[1]})";
        }

        public override int GetHashCode()
        {
            return this[0].GetHashCode() ^ this[1].GetHashCode();
        }

        public static bool operator ==(Bra A, Bra B)
        {
            return A.Equals(B);
        }

        public static bool operator !=(Bra A, Bra B)
        {
            return !A.Equals(B);
        }

        public static implicit operator Complex[](Bra m)
        {
            return new Complex[] { m[0], m[1] };
        }

        public static implicit operator Bra(Complex[] m)
        {
            return new Bra(m);
        }

        public static Bra operator /(Bra m, Complex v)
        {
            return new Bra(m[0] / v, m[1] / v);
        }

        public static Bra operator *(Bra m, Complex v)
        {
            return new Bra(m[0] * v, m[1] * v);
        }

        public static Bra operator +(Bra m, Complex v)
        {
            return new Bra(m[0] + v, m[1] + v);
        }

        public static Bra operator -(Bra m, Complex v)
        {
            return new Bra(m[0] - v, m[1] - v);
        }

        public static Bra operator +(Bra m)
        {
            return m;
        }

        public static Bra operator -(Bra m)
        {
            return new Bra(-m[0], -m[1]);
        }

        public static Bra operator +(Bra a, Bra b)
        {
            return new Bra(a[0] + b[0], a[1] + b[1]);
        }

        public static Bra operator -(Bra a, Bra b)
        {
            return new Bra(a[0] - b[0], a[1] - b[1]);
        }

        public static Complex operator *(Bra a, Ket b)
        {
            return a[0] * b[0] + a[1] * b[1];
        }
    }

}