namespace QBits.QBits
{
    public static partial class NP
    {
         
         
        public static void prepare_classical_message_plusminus(bool bit, Qubit q)
        {
            if (bit)
            {
                q.x();
            }
            q.h();
        }

        public static bool eve_measure_plusminus(Qubit q, Basis basis)
        {
            q.h();
            return q.measure(basis);
        }

        public static void send_classical_bit_plusminus(QuantumDevice device, bool bit, Basis basis)
        {
            device.using_qubit((q) =>
            {
                prepare_classical_message_plusminus(bit, q);
                var result = eve_measure_plusminus(q, basis);
                q.reset(basis);
                AssertHelper.AreEqual(result, bit);
            });
        }
    }

}