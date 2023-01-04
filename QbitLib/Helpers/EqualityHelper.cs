using System;
using System.Numerics;

namespace QBits.QBits
{
    internal static class EqualityHelper
    {
        private const double TOLERANCE = 1e-6;
        
        public static bool AreEqual(Complex a, Complex b)
        {
            return AreEqual(a.Real, b.Real) && AreEqual(a.Imaginary, b.Imaginary);
        }

        public static bool AreEqual(double a, double b)
        {
            return Math.Abs(a - b) < TOLERANCE;
        }
    }

}