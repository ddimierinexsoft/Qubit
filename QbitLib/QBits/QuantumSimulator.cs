using System.Collections.Generic;

namespace QBits.QBits
{
    public class QuantumSimulator : QuantumDevice
    {
        public Stack<SimulatedQubit> availableQubits = new Stack<SimulatedQubit>();

        public override Basis Basis { get; set; } = NP.StadardBasis;

        public QuantumSimulator(int nQubits = 6)
        {
            while (nQubits-- > 0)
            {
                availableQubits.Push(new SimulatedQubit(this));
            }
        }

        public override Qubit allocate_qubit()
        {
            return availableQubits.Pop();
        }

        public override void deallocate_qubit(Qubit qubit)
        {
            availableQubits.Push((SimulatedQubit)qubit);
        }
         
        
    }

}