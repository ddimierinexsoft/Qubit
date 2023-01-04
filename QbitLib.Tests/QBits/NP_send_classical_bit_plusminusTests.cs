using Microsoft.VisualStudio.TestTools.UnitTesting;
using QBits.QBits;

namespace QbitLib.Tests
{
    [TestClass]
    public class NP_send_classical_bit_plusminus
    {
        [TestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void send_classical_bit_plusminus(bool bit)
        {
            var qsim = new QuantumSimulator();
            NP.send_classical_bit_plusminus(qsim, bit);
        }
         
    }
}
