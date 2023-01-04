using Microsoft.VisualStudio.TestTools.UnitTesting;
using QBits.QBits;
using System.Numerics;

namespace QbitLib.Tests
{
    [TestClass]
    public class Matrix2x2Tests
    {
        [TestMethod]
        public void DefaultConstructor()
        {
            var matr = new Matrix2x2();
            Assert.AreEqual(2, matr.Rows);
            Assert.AreEqual(2, matr.Columns);
        }

        [TestMethod]
        public void ItemsConstructor()
        {
            Complex a00 = 1;
            Complex a01 = 2;
            Complex a10 = 3;
            Complex a11 = 3;
            var matr = new Matrix2x2(a00, a01, a10, a11);
            Assert.AreEqual(a00, matr[0, 0]);
            Assert.AreEqual(a01, matr[0, 1]);
            Assert.AreEqual(a10, matr[1, 0]);
            Assert.AreEqual(a11, matr[1, 1]);
        }

        [TestMethod]
        public void ArrayConstructor()
        {
            Complex[] arr = new Complex[] { 0, 1, 2, 3 };
            var matr = new Matrix2x2(arr);
            Assert.AreEqual(arr[0], matr[0, 0]);
            Assert.AreEqual(arr[1], matr[0, 1]);
            Assert.AreEqual(arr[2], matr[1, 0]);
            Assert.AreEqual(arr[3], matr[1, 1]);
        }

        [TestMethod]
        public void CopyConstructor()
        {
            var matr1 = new Matrix2x2(1, 2, 3, 4);
            var matr2 = new Matrix2x2(matr1);
            Assert.AreEqual(matr1[0, 0], matr2[0, 0]);
            Assert.AreEqual(matr1[0, 1], matr2[0, 1]);
            Assert.AreEqual(matr1[1, 0], matr2[1, 0]);
            Assert.AreEqual(matr1[1, 1], matr2[1, 1]);
        }

        [TestMethod]
        public void Transpose()
        {
            Matrix2x2 matr = new Matrix2x2(1, 2, 3, 4);
            Matrix2x2 tra = matr.Transpose();
            Assert.AreEqual(matr[0, 0], tra[0, 0]);
            Assert.AreEqual(matr[0, 1], tra[1, 0]);
            Assert.AreEqual(matr[1, 0], tra[0, 1]);
            Assert.AreEqual(matr[1, 1], tra[1, 1]);
        }

        [TestMethod]
        public void EqualsOk()
        {
            var matr1 = new Matrix2x2(1, 2, 3, 4);
            var matr2 = new Matrix2x2(1, 2, 3, 4);
            Assert.IsTrue(matr1.Equals(matr2));
        }

        [TestMethod]
        public void EqualsFalse()
        {
            var matr1 = new Matrix2x2(3, 2, 1, 0);
            var matr2 = new Matrix2x2(1, 2, 3, 4);
            Assert.IsFalse(matr1.Equals(matr2));
        }

        [TestMethod]
        public void ToStringTest()
        {
            var matr = new Matrix2x2(3, 2, 1, 0);
            var str = matr.ToString();
            var expectedString = "(\r\n(3, 0), (2, 0)\r\n(1, 0), (0, 0)\r\n)";
            Assert.AreEqual(expectedString, str);
        }

        [TestMethod]
        public void GetHashCodeTest()
        {
            var matr = new Matrix2x2(3, 2, 1, 0);
            var hash = matr.GetHashCode();
            var expectedHash = matr[0, 0].GetHashCode() ^ matr[0, 1].GetHashCode() ^ matr[1, 0].GetHashCode() ^ matr[1, 1].GetHashCode();
            Assert.AreEqual(expectedHash, hash);
        }

        [TestMethod]
        public void OperatorEQOk()
        {
            var matr1 = new Matrix2x2(1, 2, 3, 4);
            var matr2 = new Matrix2x2(1, 2, 3, 4);
            Assert.IsTrue(matr1 == matr2);
        }

        [TestMethod]
        public void OperatorEQFalse()
        {
            var matr1 = new Matrix2x2(3, 2, 1, 0);
            var matr2 = new Matrix2x2(1, 2, 3, 4);
            Assert.IsFalse(matr1 == matr2);
        }

        [TestMethod]
        public void OperatorNQOk()
        {
            var matr1 = new Matrix2x2(3, 2, 1, 0);
            var matr2 = new Matrix2x2(1, 2, 3, 4);
            Assert.IsTrue(matr1 != matr2);
        }

        [TestMethod]
        public void OperatorNQFalse()
        {
            var matr1 = new Matrix2x2(1, 2, 3, 4);
            var matr2 = new Matrix2x2(1, 2, 3, 4);
            Assert.IsFalse(matr1 != matr2);
        }

        [TestMethod]
        public void OperatorImplicitConvertToArray()
        {
            var matr = new Matrix2x2(1, 2, 3, 4);
            Complex[] arr = matr;
            Assert.AreEqual(arr[0], matr[0, 0]);
            Assert.AreEqual(arr[1], matr[0, 1]);
            Assert.AreEqual(arr[2], matr[1, 0]);
            Assert.AreEqual(arr[3], matr[1, 1]);
        }

        [TestMethod]
        public void OperatorImplicitConvertFromArray()
        {
            Complex[] arr = new Complex[] { 1, 2, 3, 4 };
            Matrix2x2 matr = arr;
            Assert.AreEqual(arr[0], matr[0, 0]);
            Assert.AreEqual(arr[1], matr[0, 1]);
            Assert.AreEqual(arr[2], matr[1, 0]);
            Assert.AreEqual(arr[3], matr[1, 1]);
        }

        [TestMethod]
        public void OperatorImplicitConvertToArray2()
        {
            var matr = new Matrix2x2(1, 2, 3, 4);
            Complex[][] arr = matr;
            Assert.AreEqual(arr[0][0], matr[0, 0]);
            Assert.AreEqual(arr[0][1], matr[0, 1]);
            Assert.AreEqual(arr[1][0], matr[1, 0]);
            Assert.AreEqual(arr[1][1], matr[1, 1]);
        }

        [TestMethod]
        public void OperatorImplicitConvertFromArray2()
        {
            Complex[][] arr = new Complex[][] { new Complex[] { 1, 2 }, new Complex[] {3 , 4} };
            Matrix2x2 matr = arr;
            Assert.AreEqual(arr[0][0], matr[0, 0]);
            Assert.AreEqual(arr[0][1], matr[0, 1]);
            Assert.AreEqual(arr[1][0], matr[1, 0]);
            Assert.AreEqual(arr[1][1], matr[1, 1]);
        }

        [TestMethod]
        public void OperatorDivideByScalar()
        {
            Matrix2x2 matr = new Matrix2x2(2, 4, 3, 2);
            Matrix2x2 res = matr / 2;
            Assert.AreEqual(matr[0, 0] / 2, res[0, 0]);
            Assert.AreEqual(matr[0, 1] / 2, res[0, 1]);
            Assert.AreEqual(matr[1, 0] / 2, res[1, 0]);
            Assert.AreEqual(matr[1, 1] / 2, res[1, 1]);
        }

        [TestMethod]
        public void OperatorMultiplyByScalar()
        {
            Matrix2x2 matr = new Matrix2x2(2, 4, 3, 2);
            Matrix2x2 res = matr * 2;
            Assert.AreEqual(matr[0, 0] * 2, res[0, 0]);
            Assert.AreEqual(matr[0, 1] * 2, res[0, 1]);
            Assert.AreEqual(matr[1, 0] * 2, res[1, 0]);
            Assert.AreEqual(matr[1, 1] * 2, res[1, 1]);
        }

        [TestMethod]
        public void OperatorAddToScalar()
        {
            Matrix2x2 matr = new Matrix2x2(2, 4, 3, 2);
            Matrix2x2 res = matr + 2;
            Assert.AreEqual(matr[0, 0] + 2, res[0, 0]);
            Assert.AreEqual(matr[0, 1] + 2, res[0, 1]);
            Assert.AreEqual(matr[1, 0] + 2, res[1, 0]);
            Assert.AreEqual(matr[1, 1] + 2, res[1, 1]);
        }

        [TestMethod]
        public void OperatorSubtractScalar()
        {
            Matrix2x2 matr = new Matrix2x2(2, 4, 3, 2);
            Matrix2x2 res = matr - 2;
            Assert.AreEqual(matr[0, 0] - 2, res[0, 0]);
            Assert.AreEqual(matr[0, 1] - 2, res[0, 1]);
            Assert.AreEqual(matr[1, 0] - 2, res[1, 0]);
            Assert.AreEqual(matr[1, 1] - 2, res[1, 1]);
        }

        [TestMethod]
        public void OperatorUnaryPlus()
        {
            Matrix2x2 matr = new Matrix2x2(2, 4, 3, 2);
            Matrix2x2 res = +matr;
            Assert.AreEqual(matr[0, 0], res[0, 0]);
            Assert.AreEqual(matr[0, 1], res[0, 1]);
            Assert.AreEqual(matr[1, 0], res[1, 0]);
            Assert.AreEqual(matr[1, 1], res[1, 1]);
        }

        [TestMethod]
        public void OperatorUnaryMinus()
        {
            Matrix2x2 matr = new Matrix2x2(2, 4, 3, 2);
            Matrix2x2 res = -matr;
            Assert.AreEqual(-matr[0, 0], res[0, 0]);
            Assert.AreEqual(-matr[0, 1], res[0, 1]);
            Assert.AreEqual(-matr[1, 0], res[1, 0]);
            Assert.AreEqual(-matr[1, 1], res[1, 1]);
        }

        [TestMethod]
        public void OperatorAddToMatrix()
        {
            Matrix2x2 matr1 = new Matrix2x2(2, 4, 1, 0);
            Matrix2x2 matr2 = new Matrix2x2(3, -1, 0, 1);
            Matrix2x2 res = matr1 + matr2;
            Assert.AreEqual(matr1[0, 0] + matr2[0, 0], res[0, 0]);
            Assert.AreEqual(matr1[0, 1] + matr2[0, 1], res[0, 1]);
            Assert.AreEqual(matr1[1, 0] + matr2[1, 0], res[1, 0]);
            Assert.AreEqual(matr1[1, 1] + matr2[1, 1], res[1, 1]);
        }

        [TestMethod]
        public void OperatorSubtractMatrix()
        {
            Matrix2x2 matr1 = new Matrix2x2(2, 4, 1, 0);
            Matrix2x2 matr2 = new Matrix2x2(3, -1, 0, 1);
            Matrix2x2 res = matr1 - matr2;
            Assert.AreEqual(matr1[0, 0] - matr2[0, 0], res[0, 0]);
            Assert.AreEqual(matr1[0, 1] - matr2[0, 1], res[0, 1]);
            Assert.AreEqual(matr1[1, 0] - matr2[1, 0], res[1, 0]);
            Assert.AreEqual(matr1[1, 1] - matr2[1, 1], res[1, 1]);
        }

        [TestMethod]
        public void DetZeroMatrix()
        {
            Matrix2x2 zero = Matrix2x2.Zero;
            Complex det = zero.Det();
            Assert.AreEqual(Complex.Zero, det);
        }

        [TestMethod]
        public void DetIdentityMatrix()
        {
            Matrix2x2 I = Matrix2x2.I;
            Complex det = I.Det();
            Assert.AreEqual(Complex.One, det);
        }

        [TestMethod]
        public void DetSingularMatrix()
        {
            Matrix2x2 M = new Matrix2x2(1, 2, 2, 4);
            Complex det = M.Det();
            Assert.AreEqual(Complex.Zero, det);
        }

        [TestMethod]
        public void DetRegularMatrix()
        {
            Matrix2x2 M = new Matrix2x2(1, 2, 1, 4);
            Complex det = M.Det();
            Assert.AreEqual((Complex)2, det);
        }

        [TestMethod]
        public void InvertIdentityMatrix()
        {
            Matrix2x2 M = Matrix2x2.I;
            Matrix2x2 res = M.Invert();
            Assert.AreEqual(Matrix2x2.I, res);
        }

        [TestMethod]
        [ExpectedException(typeof(System.DivideByZeroException))]
        public void InvertZeroMatrix()
        {
            Matrix2x2 M = Matrix2x2.Zero;
            Matrix2x2 res = M.Invert();
        }

        [TestMethod]
        [ExpectedException(typeof(System.DivideByZeroException))]
        public void InvertSingularMatrix()
        {
            Matrix2x2 M = new Matrix2x2(1, 2, 2, 4);
            Matrix2x2 res = M.Invert();
        }

        [TestMethod]
        public void InvertRegularMatrix()
        {
            Matrix2x2 M = new Matrix2x2(1, 2, 1, 4);
            Matrix2x2 res = M.Invert();
            Assert.AreEqual(new Matrix2x2(2, -1, -0.5, 0.5), res);
            Assert.AreEqual(Matrix2x2.I, M * res);
            Assert.AreEqual(Matrix2x2.I, res * M);
        }

        [TestMethod]
        public void Multiply2Matrixies()
        {
            Matrix2x2 M1 = new Matrix2x2(1, 2, 1, 4);
            Matrix2x2 M2 = new Matrix2x2(1, 2, 1, 4);
            Matrix2x2 res = M1 * M2;
            Assert.AreEqual(new Matrix2x2(3, 10, 5, 18), res);
        }

        [TestMethod]
        public void Divide2Matrixies()
        {
            Matrix2x2 M1 = new Matrix2x2(1, 2, 1, 4);
            Matrix2x2 M2 = new Matrix2x2(1, 2, 1, 4);
            Matrix2x2 res = M1 / M2;
            Assert.AreEqual(Matrix2x2.I, res);
        }

        [TestMethod]
        public void LeftMultiplyBra()
        {
            Bra bra = new Bra(1, 2);
            Matrix2x2 M = new Matrix2x2(1, 2, 1, 4);
            Bra res = bra * M;
            Assert.AreEqual(new Bra(3, 10), res);
        }

        [TestMethod]
        public void RightMultiplyKet()
        {
            Ket ket = new Ket(1, 2);
            Matrix2x2 M = new Matrix2x2(1, 2, 1, 4);
            Ket res = M * ket;
            Assert.AreEqual(new Ket(5, 9), res);
        }
    }
}
