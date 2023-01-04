using Microsoft.VisualStudio.TestTools.UnitTesting;
using QBits.QBits;

namespace QbitLib.Tests
{
    [TestClass]
    public class NP_QRNGTests
    {
        [TestMethod]
        public void qrng()
        {
            var qsim = new QuantumSimulator();
            for (int idx_sample = 0; idx_sample < 10; idx_sample++)
            {
                var random_sample = NP.qrng(qsim);
                System.Diagnostics.Debug.Print($"Our QRNG returned {random_sample}.");
            }
        }
         
    }
}
