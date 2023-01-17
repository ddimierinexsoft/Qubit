namespace QBits.QBits
{
    public static partial class NP
    {
        
        public static bool sample_random_bit(QuantumDevice device)
        {
            return device.using_qubit(q =>
            {
                q.h();
                var res = q.measure();
                q.reset();
                return res;
            });
        }
         
    }

}