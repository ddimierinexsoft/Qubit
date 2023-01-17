using System.Numerics;

namespace QBits.QBits
{
    internal static class HashCalculatorHelper
    {
        public static int Calculate(Complex[] arr)
        {
            int ret = 0;
            for(int i = 0; i < arr.Length; i++)
            {
                ret = ret ^ arr[i].GetHashCode();
            }
            return ret;
        }
                 
    }

}