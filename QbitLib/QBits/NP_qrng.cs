﻿namespace QBits.QBits
{
    public static partial class NP
    {
        
        public static bool qrng(QuantumDevice device)
        {
            return device.using_qubit(q =>
            {
                q.h();
                return q.measure();
            });
        }
         
    }

}