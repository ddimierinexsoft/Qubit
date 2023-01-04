using Microsoft.VisualStudio.TestTools.UnitTesting;
using QBits.QBits;
using System.Numerics;

namespace QbitLib.Tests
{
    [TestClass]
    public class BraTests
    {
        [TestMethod]
        public void DefaultConstructor()
        {
            var bra = new Bra();
            Assert.AreEqual(1, bra.Rows);
            Assert.AreEqual(2, bra.Columns);
        }

        [TestMethod]
        public void ItemsConstructor()
        {
            Complex a0 = 1;
            Complex a1 = 2;
            var bra = new Bra(a0, a1);
            Assert.AreEqual(a0, bra[0]);
            Assert.AreEqual(a1, bra[1]);
        }

        [TestMethod]
        public void ArrayConstructor()
        {
            Complex[] arr = new Complex[] { (Complex)0, (Complex)1 };
            var bra = new Bra(arr);
            Assert.AreEqual(arr[0], bra[0]);
            Assert.AreEqual(arr[1], bra[1]);
        }

        [TestMethod]
        public void CopyConstructor()
        {
            var bra1 = new Bra(1, 2);
            var bra2 = new Bra(bra1);
            Assert.AreEqual(bra1[0], bra2[0]);
            Assert.AreEqual(bra1[1], bra2[1]);
        }

        [TestMethod]
        public void Transpose()
        {
            Bra bra = new Bra(1, 2);
            Ket ket = bra.Transpose();
            Assert.AreEqual(bra[0], ket[0]);
            Assert.AreEqual(bra[1], ket[1]);
        }

        [TestMethod]
        public void EqualsOk()
        {
            var bra1 = new Bra(1, 2);
            var bra2 = new Bra(1, 2);
            Assert.IsTrue(bra1.Equals(bra2));
        }

        [TestMethod]
        public void EqualsFalse()
        {
            var bra1 = new Bra(3, 2);
            var bra2 = new Bra(1, 2);
            Assert.IsFalse(bra1.Equals(bra2));
        }

        [TestMethod]
        public void ToStringTest()
        {
            var bra = new Bra(3, 2);
            var str = bra.ToString();
            var expectedString = "<((3, 0), (2, 0))";
            Assert.AreEqual(expectedString, str);
        }

        [TestMethod]
        public void GetHashCodeTest()
        {
            var bra = new Bra(3, 2);
            var hash = bra.GetHashCode();
            var expectedHash = bra[0].GetHashCode() ^ bra[1].GetHashCode();
            Assert.AreEqual(expectedHash, hash);
        }

        [TestMethod]
        public void OperatorEQOk()
        {
            var bra1 = new Bra(1, 2);
            var bra2 = new Bra(1, 2);
            Assert.IsTrue(bra1 == bra2);
        }

        [TestMethod]
        public void OperatorEQFalse()
        {
            var bra1 = new Bra(3, 2);
            var bra2 = new Bra(1, 2);
            Assert.IsFalse(bra1 == bra2);
        }

        [TestMethod]
        public void OperatorNQOk()
        {
            var bra1 = new Bra(3, 2);
            var bra2 = new Bra(1, 2);
            Assert.IsTrue(bra1 != bra2);
        }

        [TestMethod]
        public void OperatorNQFalse()
        {
            var bra1 = new Bra(1, 2);
            var bra2 = new Bra(1, 2);
            Assert.IsFalse(bra1 != bra2);
        }

        [TestMethod]
        public void OperatorImplicitConvertToArray()
        {
            var bra = new Bra(1, 2);
            Complex[] arr = bra;
            Assert.AreEqual(arr[0], bra[0]);
            Assert.AreEqual(arr[1], bra[1]);
        }

        [TestMethod]
        public void OperatorImplicitConvertFromArray()
        {
            Complex[] arr = new Complex[] { 1, 2 };
            Bra bra = arr;
            Assert.AreEqual(arr[0], bra[0]);
            Assert.AreEqual(arr[1], bra[1]);
        }

        [TestMethod]
        public void OperatorDivideByScalar()
        {
            Bra bra = new Bra(2, 4);
            Bra res = bra / 2;
            Assert.AreEqual(bra[0] / 2, res[0]);
            Assert.AreEqual(bra[1] / 2, res[1]);
        }

        [TestMethod]
        public void OperatorMultiplyByScalar()
        {
            Bra bra = new Bra(2, 4);
            Bra res = bra * 2;
            Assert.AreEqual(bra[0] * 2, res[0]);
            Assert.AreEqual(bra[1] * 2, res[1]);
        }

        [TestMethod]
        public void OperatorAddToScalar()
        {
            Bra bra = new Bra(2, 4);
            Bra res = bra + 2;
            Assert.AreEqual(bra[0] + 2, res[0]);
            Assert.AreEqual(bra[1] + 2, res[1]);
        }

        [TestMethod]
        public void OperatorSubtractScalar()
        {
            Bra bra = new Bra(2, 4);
            Bra res = bra - 2;
            Assert.AreEqual(bra[0] - 2, res[0]);
            Assert.AreEqual(bra[1] - 2, res[1]);
        }

        [TestMethod]
        public void OperatorUnaryPlus()
        {
            Bra bra = new Bra(2, 4);
            Bra res = +bra;
            Assert.AreEqual(bra[0], res[0]);
            Assert.AreEqual(bra[1], res[1]);
        }

        [TestMethod]
        public void OperatorUnaryMinus()
        {
            Bra bra = new Bra(2, 4);
            Bra res = -bra;
            Assert.AreEqual(-bra[0], res[0]);
            Assert.AreEqual(-bra[1], res[1]);
        }

        [TestMethod]
        public void OperatorAddToBra()
        {
            Bra bra1 = new Bra(2, 4);
            Bra bra2 = new Bra(3, -1);
            Bra res = bra1 + bra2;
            Assert.AreEqual(bra1[0] + bra2[0], res[0]);
            Assert.AreEqual(bra1[1] + bra2[1], res[1]);
        }

        [TestMethod]
        public void OperatorSubtractBra()
        {
            Bra bra1 = new Bra(2, 4);
            Bra bra2 = new Bra(3, -1);
            Bra res = bra1 - bra2;
            Assert.AreEqual(bra1[0] - bra2[0], res[0]);
            Assert.AreEqual(bra1[1] - bra2[1], res[1]);
        }
    }
}
