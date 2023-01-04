using Microsoft.VisualStudio.TestTools.UnitTesting;
using QBits.QBits;
using System;
using System.Numerics;

namespace QbitLib.Tests
{
    [TestClass]
    public class QubitTests
    {
        [TestMethod]
        public void DefaultConstructor()
        {
            var q = new SimulatedQubit();
            Assert.IsNotNull(q);
        }

        [TestMethod]
        public void ArrayConstructor()
        {
            Complex[] arr = new Complex[] { 0, 1 };
            var q = new SimulatedQubit(arr);
            Assert.IsTrue(q.measure());
        }

        [TestMethod]
        public void reset()
        {
            var q = new SimulatedQubit();
            q.reset();
            Assert.IsFalse(q.measure());
        }

        [TestMethod]
        public void h0()
        {
            var q = new SimulatedQubit(1, 0);
            q.h();
            Bra state = q.GetState();
            Assert.AreEqual(new Bra(Math.Sqrt(2) / 2, Math.Sqrt(2) / 2), state);
        }

        [TestMethod]
        public void h1()
        {
            var q = new SimulatedQubit(0, 1);
            q.h();
            Bra state = q.GetState();
            Assert.AreEqual(new Bra(Math.Sqrt(2) / 2, -Math.Sqrt(2) / 2), state);
        }

        [TestMethod]
        public void x0()
        {
            var q = new SimulatedQubit(1, 0);
            q.x();
            Bra state = q.GetState();
            Assert.AreEqual(new Bra(0, 1), state);
        }

        [TestMethod]
        public void x1()
        {
            var q = new SimulatedQubit(0, 1);
            q.x();
            Bra state = q.GetState();
            Assert.AreEqual(new Bra(1, 0), state );
        }
    }
}
