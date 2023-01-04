using Microsoft.VisualStudio.TestTools.UnitTesting;
using QBits.QBits;
using System.Numerics;

namespace QbitLib.Tests
{
    [TestClass]
    public class KetTests
    {
        [TestMethod]
        public void DefaultConstructor()
        {
            var ket = new Ket();
            Assert.AreEqual(2, ket.Rows);
            Assert.AreEqual(1, ket.Columns);
        }

        [TestMethod]
        public void ItemsConstructor()
        {
            Complex a0 = 1;
            Complex a1 = 2;
            var ket = new Ket(a0, a1);
            Assert.AreEqual(a0, ket[0]);
            Assert.AreEqual(a1, ket[1]);
        }

        [TestMethod]
        public void ArrayConstructor()
        {
            Complex[] arr = new Complex[] { (Complex)0, (Complex)1 };
            var ket = new Ket(arr);
            Assert.AreEqual(arr[0], ket[0]);
            Assert.AreEqual(arr[1], ket[1]);
        }

        [TestMethod]
        public void CopyConstructor()
        {
            var ket1 = new Ket(1, 2);
            var ket2 = new Ket(ket1);
            Assert.AreEqual(ket1[0], ket2[0]);
            Assert.AreEqual(ket1[1], ket2[1]);
        }

        [TestMethod]
        public void Transpose()
        {
            Ket ket = new Ket(1, 2);
            Bra bra = ket.Transpose();
            Assert.AreEqual(ket[0], bra[0]);
            Assert.AreEqual(ket[1], bra[1]);
        }

        [TestMethod]
        public void EqualsOk()
        {
            var ket1 = new Ket(1, 2);
            var ket2 = new Ket(1, 2);
            Assert.IsTrue(ket1.Equals(ket2));
        }

        [TestMethod]
        public void EqualsFalse()
        {
            var ket1 = new Ket(3, 2);
            var ket2 = new Ket(1, 2);
            Assert.IsFalse(ket1.Equals(ket2));
        }

        [TestMethod]
        public void ToStringTest()
        {
            var ket = new Ket(3, 2);
            var str = ket.ToString();
            var expectedString = "<((3, 0), (2, 0))";
            Assert.AreEqual(expectedString, str);
        }

        [TestMethod]
        public void GetHashCodeTest()
        {
            var ket = new Ket(3, 2);
            var hash = ket.GetHashCode();
            var expectedHash = ket[0].GetHashCode() ^ ket[1].GetHashCode();
            Assert.AreEqual(expectedHash, hash);
        }

        [TestMethod]
        public void OperatorEQOk()
        {
            var ket1 = new Ket(1, 2);
            var ket2 = new Ket(1, 2);
            Assert.IsTrue(ket1 == ket2);
        }

        [TestMethod]
        public void OperatorEQFalse()
        {
            var ket1 = new Ket(3, 2);
            var ket2 = new Ket(1, 2);
            Assert.IsFalse(ket1 == ket2);
        }

        [TestMethod]
        public void OperatorNQOk()
        {
            var ket1 = new Ket(3, 2);
            var ket2 = new Ket(1, 2);
            Assert.IsTrue(ket1 != ket2);
        }

        [TestMethod]
        public void OperatorNQFalse()
        {
            var ket1 = new Ket(1, 2);
            var ket2 = new Ket(1, 2);
            Assert.IsFalse(ket1 != ket2);
        }

        [TestMethod]
        public void OperatorImplicitConvertToArray()
        {
            var ket = new Ket(1, 2);
            Complex[] arr = ket;
            Assert.AreEqual(arr[0], ket[0]);
            Assert.AreEqual(arr[1], ket[1]);
        }

        [TestMethod]
        public void OperatorImplicitConvertFromArray()
        {
            Complex[] arr = new Complex[] { 1, 2 };
            Ket ket = arr;
            Assert.AreEqual(arr[0], ket[0]);
            Assert.AreEqual(arr[1], ket[1]);
        }

        [TestMethod]
        public void OperatorDivideByScalar()
        {
            Ket ket = new Ket(2, 4);
            Ket res = ket / 2;
            Assert.AreEqual(ket[0] / 2, res[0]);
            Assert.AreEqual(ket[1] / 2, res[1]);
        }

        [TestMethod]
        public void OperatorMultiplyByScalar()
        {
            Ket ket = new Ket(2, 4);
            Ket res = ket * 2;
            Assert.AreEqual(ket[0] * 2, res[0]);
            Assert.AreEqual(ket[1] * 2, res[1]);
        }

        [TestMethod]
        public void OperatorAddToScalar()
        {
            Ket ket = new Ket(2, 4);
            Ket res = ket + 2;
            Assert.AreEqual(ket[0] + 2, res[0]);
            Assert.AreEqual(ket[1] + 2, res[1]);
        }

        [TestMethod]
        public void OperatorSubtractScalar()
        {
            Ket ket = new Ket(2, 4);
            Ket res = ket - 2;
            Assert.AreEqual(ket[0] - 2, res[0]);
            Assert.AreEqual(ket[1] - 2, res[1]);
        }

        [TestMethod]
        public void OperatorUnaryPlus()
        {
            Ket ket = new Ket(2, 4);
            Ket res = +ket;
            Assert.AreEqual(ket[0], res[0]);
            Assert.AreEqual(ket[1], res[1]);
        }

        [TestMethod]
        public void OperatorUnaryMinus()
        {
            Ket ket = new Ket(2, 4);
            Ket res = -ket;
            Assert.AreEqual(-ket[0], res[0]);
            Assert.AreEqual(-ket[1], res[1]);
        }

        [TestMethod]
        public void OperatorAddToKet()
        {
            Ket ket1 = new Ket(2, 4);
            Ket ket2 = new Ket(3, -1);
            Ket res = ket1 + ket2;
            Assert.AreEqual(ket1[0] + ket2[0], res[0]);
            Assert.AreEqual(ket1[1] + ket2[1], res[1]);
        }

        [TestMethod]
        public void OperatorSubtractKet()
        {
            Ket ket1 = new Ket(2, 4);
            Ket ket2 = new Ket(3, -1);
            Ket res = ket1 - ket2;
            Assert.AreEqual(ket1[0] - ket2[0], res[0]);
            Assert.AreEqual(ket1[1] - ket2[1], res[1]);
        }
    }
}
