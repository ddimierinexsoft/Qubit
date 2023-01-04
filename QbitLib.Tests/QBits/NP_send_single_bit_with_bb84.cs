using Microsoft.VisualStudio.TestTools.UnitTesting;
using QBits.QBits;

namespace QbitLib.Tests
{
    [TestClass]
    public class NP_send_single_bit_with_bb84
    {
        [TestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void send_single_bit_with_bb84(bool bit)
        {
            var senderDevice = new QuantumSimulator();
            var receiverDevice = new QuantumSimulator();
            var ((sentMessage, senderBasis), (receivedMessage, receiverBasis)) = NP.send_single_bit_with_bb84(bit, senderDevice, receiverDevice);
            Assert.AreEqual(bit, receivedMessage);
        }
         
    }
}
