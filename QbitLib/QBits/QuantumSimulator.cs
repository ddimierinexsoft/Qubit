using System.Collections.Generic;

namespace QBits.QBits
{
    public class QuantumSimulator : QuantumDevice
    {
        public Stack<SimulatedQubit> availableQubits = new Stack<SimulatedQubit>();


        public QuantumSimulator(int nQubits = 6)
        {
            while (nQubits-- > 0)
            {
                availableQubits.Push(new SimulatedQubit());
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