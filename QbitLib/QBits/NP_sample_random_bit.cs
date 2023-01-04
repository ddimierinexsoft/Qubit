namespace QBits.QBits
{
    public static partial class NP
    {
        
        public static bool sample_random_bit(QuantumDevice device, Basis basis)
        {
            return device.using_qubit(q =>
            {
                q.h();
                var res = q.measure(basis);
                q.reset(basis);
                return res;
            });
        }
         
    }

}