using System;
using System.Numerics;

namespace QBits.QBits
{
    public class Ket : Vect2
    {
        public override int Rows => 2;
        public override int Columns => 1;

        public Ket() { }
        public Ket(Complex[] items) : this(items[0], items[1]) { }
        public Ket(Complex a0, Complex a1) : base(a0, a1) { }
        
        public Bra Transpose()
        {
            return new Bra(this[0], this[1]);
        }

        public override bool Equals(object obj)
        {
            return (obj is Ket k) && EqualityHelper.AreEqual(k[0], this[0]) && EqualityHelper.AreEqual(k[1], this[1]);
        }

        public override string ToString()
        {
            return $"<({this[0]}, {this[1]})";
        }

        public override int GetHashCode()
        {
            return this[0].GetHashCode() ^ this[1].GetHashCode();
        }

        public static bool operator ==(Ket A, Ket B)
        {
            return A.Equals(B);
        }

        public static bool operator !=(Ket A, Ket B)
        {
            return !A.Equals(B);
        }

        public static implicit operator Complex[](Ket m)
        {
            return new Complex[] { m[0], m[1] };
        }

        public static implicit operator Ket(Complex[] m)
        {
            return new Ket(m);
        }

        public static Ket operator /(Ket m, Complex v)
        {
            return new Ket(m[0] / v, m[1] / v);
        }

        public static Ket operator *(Ket m, Complex v)
        {
            return new Ket(m[0] * v, m[1] * v);
        }

        public static Ket operator +(Ket m, Complex v)
        {
            return new Ket(m[0] + v, m[1] + v);
        }

        public static Ket operator -(Ket m, Complex v)
        {
            return new Ket(m[0] - v, m[1] - v);
        }

        public static Ket operator +(Ket m)
        {
            return m;
        }

        public static Ket operator -(Ket m)
        {
            return new Ket(-m[0], -m[1]);
        }

        public static Ket operator +(Ket a, Ket b)
        {
            return new Ket(a[0] + b[0], a[1] + b[1]);
        }

        public static Ket operator -(Ket a, Ket b)
        {
            return new Ket(a[0] - b[0], a[1] - b[1]);
        }
    }

}