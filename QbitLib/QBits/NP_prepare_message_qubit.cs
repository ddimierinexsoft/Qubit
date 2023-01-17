using System;
using System.Collections.Generic;
using System.Linq;

namespace QBits.QBits
{
    public static partial class NP
    {
         
         
        public static void prepare_message_qubit(bool message, bool basis, Qubit q)
        {
            if (message)
            {
                q.x();
            }
            if (basis)
            {
                q.h();
            }
        }

        public static bool measure_message_qubit(bool b, Qubit q)
        {
            if (b)
            {
                q.h();
            }
            var res = q.measure();
            q.reset();
            return res;
        }

        public static string convert_to_hex(IEnumerable<bool> bits)
        {
            var bitString = string.Join("", bits.Select(bit => (bit == true) ? "1" : "0"));
            var intVal = Convert.ToInt32(bitString, 2);
            return Convert.ToString(intVal, 16);
        }
    }

}