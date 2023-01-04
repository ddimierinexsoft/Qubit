using Microsoft.VisualStudio.TestTools.UnitTesting;
using QBits.QBits;

namespace QbitLib.Tests
{
    [TestClass]
    public class NP_send_classical_bit
    {
        [TestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void send_classical_bit(bool bit)
        {
            var qsim = new QuantumSimulator();
            NP.send_classical_bit(qsim, bit);
        }
         
    }
}
