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

        public static bool eve_measure_plusminus(Qubit q)
        {
            q.h();
            return q.measure();
        }

        public static void send_classical_bit_plusminus(QuantumDevice device, bool bit)
        {
            device.using_qubit((q) =>
            {
                prepare_classical_message_plusminus(bit, q);
                var result = eve_measure_plusminus(q);
                q.reset();
                AssertHelper.AreEqual(result, bit);
            });
        }
    }

}