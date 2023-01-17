using Microsoft.VisualStudio.TestTools.UnitTesting;
using QBits.QBits;
using System;
using System.Numerics;

namespace QbitLib.Tests
{
    [TestClass]
    public class QubitTests
    {
        QuantumSimulator device = new QuantumSimulator();


        [TestMethod]
        public void DefaultConstructor()
        {
            var q = new SimulatedQubit(device);
            Assert.IsNotNull(q);
        }

        [TestMethod]
        public void SetState()
        {
            var q = new SimulatedQubit(device);
            Bra arr = new Complex[] { 0, 1 };
            q.SetState(arr);
            Bra resArr = q.GetState();
            Assert.AreEqual(resArr, arr);
        }

        [TestMethod]
        public void reset()
        {
            var q = new SimulatedQubit(device);
            q.reset();
            Assert.IsFalse(q.measure());
        }

        [TestMethod]
        public void h0()
        {
            var q = new SimulatedQubit(device);
            q.SetState(NP.KET_0);
            q.h();
            Bra state = q.GetState();
            Assert.AreEqual(new Bra(Math.Sqrt(2) / 2, Math.Sqrt(2) / 2), state);
        }

        [TestMethod]
        public void h1()
        {
            var q = new SimulatedQubit(device);
            q.SetState(NP.KET_1);
            q.h();
            Bra state = q.GetState();
            Assert.AreEqual(new Bra(Math.Sqrt(2) / 2, -Math.Sqrt(2) / 2), state);
        }

        [TestMethod]
        public void x0()
        {
            var q = new SimulatedQubit(device);
            q.SetState(NP.KET_0);
            q.x();
            Bra state = q.GetState();
            Assert.AreEqual(new Bra(0, 1), state);
        }

        [TestMethod]
        public void x1()
        {
            var q = new SimulatedQubit(device);
            q.SetState(NP.KET_1);
            q.x();
            Bra state = q.GetState();
            Assert.AreEqual(new Bra(1, 0), state );
        }
    }
}
