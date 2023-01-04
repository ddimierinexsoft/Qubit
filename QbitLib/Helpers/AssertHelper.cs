using System;
using System.Numerics;

namespace QBits.QBits
{
    internal static class AssertHelper
    {
        public static void AreEqual<T>(T a, T b)
        {
            if (!IsEQ(a, b))
            {
                throw new Exception($"Assert fail (a == b): {a} == {b}");
            }
        }

        public static bool IsEQ<T>(T a, T b)
        {
            if (IsNull(a))
            {
                if (IsNull(b))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (IsNull(b))
                {
                    return false;
                }
                else
                {
                    return a.Equals(b);
                }
            }
        }
        
        private static bool IsNull(object a) => a is null || a is DBNull;
    }

}