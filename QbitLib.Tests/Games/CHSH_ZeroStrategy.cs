using Microsoft.VisualStudio.TestTools.UnitTesting;
using QbitLib.Games.CHSH;
using QbitLib.Games.CHSH.Strategies;

namespace QbitLib.Tests
{
    [TestClass]
    public class GHSH_ZeroStrategyTests
    {
        [TestMethod]
        public void CHSH_ZeroStrategy()
        {
            var strategy = new CHSHConstant0Strategy();
            var sample = CHSHSampler.est_win_probability(strategy);
            System.Diagnostics.Debug.Print(sample.WinProbability.ToString());
        }
         
    }
}
