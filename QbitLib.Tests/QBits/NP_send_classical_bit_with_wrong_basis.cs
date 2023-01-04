using Microsoft.VisualStudio.TestTools.UnitTesting;
using QBits.QBits;

namespace QbitLib.Tests
{
    [TestClass]
    public class NP_send_classical_bit_with_wrong_basis
    {
        [TestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void send_classical_bit_with_wrong_basis(bool bit)
        {
            var qsimSender = new QuantumSimulator();
            var qsimReceiver = new QuantumSimulator();

            qsimSender.using_qubit(qs =>
            {
                NP.prepare_classical_message(bit, qs);
                var result = NP.eve_measure_plusminus(qs);
                System.Diagnostics.Debug.Print($"{bit} -> {result} = {(bit == result)}");
            });
        }
         
    }
}
