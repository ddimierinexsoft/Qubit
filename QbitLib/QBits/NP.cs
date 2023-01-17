using System;
using System.Numerics;

namespace QBits.QBits
{
    public static partial class NP
    {
        public static readonly Ket KET_0 = new Ket(1, 0);
        public static readonly Ket KET_1 = new Ket(0, 1);
        public static readonly Ket KET_Plus = new Ket(1, 1) / Math.Sqrt(2);
        public static readonly Ket KET_Minus = new Ket(1, -1) / Math.Sqrt(2);
        public static readonly Matrix2x2 H = new Matrix2x2(1, 1, 1, -1) / Math.Sqrt(2);
        public static readonly Matrix2x2 X = new Matrix2x2(0, 1, 1, 0);
        public static readonly Basis StadardBasis = new Basis(KET_0, KET_1);

        private static Random rnd = new Random();

        public static double Rnd()
        {
            return rnd.NextDouble();
        }

        public static bool random_bit()
        {
            return rnd.Next(0, 2) != 0;
        }

        public static Complex sqrt(double value)
        {
            return Math.Sqrt(value);
        }
          
        public static double Pr(Bra bra, Ket ket)
        {
            return Math.Pow((bra * ket).Magnitude, 2);
        }
    }

}