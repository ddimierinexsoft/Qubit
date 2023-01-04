namespace QBits.QBits
{
    public static partial class NP
    {
         
         
        public static void prepare_classical_message(bool bit, Qubit q)
        {
            if (bit)
            {
                q.x();
            }
        }

        public static bool eve_measure(Qubit q, Basis basis)
        {
            return q.measure(basis);
        }

        public static void send_classical_bit(QuantumDevice device, bool bit, Basis basis)
        {
            device.using_qubit((q) =>
            {
                prepare_classical_message(bit, q);
                var result = eve_measure(q, basis);
                q.reset(basis);
                AssertHelper.AreEqual(result, bit);
            });
        }
    }

}