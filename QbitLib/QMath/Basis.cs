using System;
using System.Numerics;
using static QBits.QBits.NP;

namespace QBits.QBits
{
    public class Basis
    {
        public Ket KET_0 { get; }
        public Ket KET_1 { get; }

        public Basis()
        {
            KET_0 = NP.KET_0;
            KET_1 = NP.KET_1;
        }

        public Basis(Complex[] b0, Complex[] b1)
        {
            KET_0 = b0;
            KET_1 = b1;
        }

    }
}

  