using System.Numerics;
using static QBits.QBits.NP;

namespace QBits.QBits
{
    public class SimulatedQubit : Qubit
    {
        private QuantumSimulator device;
        private Bra state;

        public SimulatedQubit(QuantumSimulator device)
        {
            this.device = device;
            this.reset();
        }         

        public Complex[] GetState()
        {
            return state;
        }

        public void SetState(Complex[] state)
        {
            this.state = state;
        }

        public virtual void h()
        {
            state = state * H;
        }

        public virtual void x()
        {
            state = state * X;
        }

        public virtual bool measure()
        {
            var pr0 = NP.Pr(state, device.Basis.KET_0);
            var montecarlo = NP.Rnd() <= pr0;
            if (montecarlo)
            {
                state = device.Basis.KET_0.Transpose();
                return false;
            }
            else
            {
                state = device.Basis.KET_1.Transpose();
                return true;
            }
        }

        public virtual void reset()
        {
            state = device.Basis.KET_0.Transpose();
        }
    }

}