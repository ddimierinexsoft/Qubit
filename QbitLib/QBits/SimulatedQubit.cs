using System;
using System.Numerics;
using static QBits.QBits.NP;

namespace QBits.QBits
{
    public class SimulatedQubit : Qubit
    {
        private Bra state;
        
        public SimulatedQubit()
        {
             
        }

        public SimulatedQubit(Complex s0, Complex s1)
        {
            this.state = new Bra(s0, s1);
        }

        public SimulatedQubit(Complex[] state)
        {
            this.state = state;
        }

        public Complex[] GetState()
        {
            return state;
        }

        public virtual void h()
        {
            state = state * H;
        }

        public virtual void x()
        {
            state = state * X;
        }

        public virtual bool measure(Basis basis)
        {
            var pr0 = NP.Pr(state, basis.KET_0);
            var montecarlo = NP.Rnd() <= pr0;
            if (montecarlo)
            {
                state = basis.KET_0.Transpose();
                return false;
            }
            else
            {
                state = basis.KET_1.Transpose();
                return true;
            }
        }

        public virtual void reset(Basis basis)
        {
            state = basis.KET_0.Transpose();
        }
    }

}