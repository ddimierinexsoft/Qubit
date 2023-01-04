using Microsoft.VisualStudio.TestTools.UnitTesting;
using QBits.QBits;

namespace QbitLib.Tests
{
    [TestClass]
    public class NP_Tests
    {
        [TestMethod]
        public void SumKetPlusAndMinus()
        {
            var kp = NP.KET_Plus;
            var km = NP.KET_Minus;
            Assert.AreEqual(NP.KET_0, (kp + km) / NP.sqrt(2));
        }
         
    }
}
