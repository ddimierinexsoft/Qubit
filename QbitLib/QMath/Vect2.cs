using System;
using System.Numerics;

namespace QBits.QBits
{
    public abstract class Vect2
    {
        private Complex[] arr = new[] { Complex.Zero, Complex.Zero };
        
        public abstract int Rows { get; }
        public abstract int Columns { get; }
        
        public Vect2() { }
        public Vect2(Complex[] items) : this(items[0], items[1]) { }
        public Vect2(Complex a0, Complex a1)
        {
            arr = new[] { a0, a1 };
        }
         

        public Complex this[int i]
        {
            get
            {
                return arr[i];
            }
        }
         
        
    }

}