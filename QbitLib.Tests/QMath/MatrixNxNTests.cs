using Microsoft.VisualStudio.TestTools.UnitTesting;
using QBits.QBits;
using System.Numerics;

namespace QbitLib.Tests
{
    [TestClass]
    public class MatrixNxNTests
    {
        [TestMethod]
        public void DefaultConstructor()
        {
            var matr = new MatrixNxN(2, 2);
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
            var matr = new MatrixNxN(2, 2, new Complex[] { a00, a01, a10, a11 });
            Assert.AreEqual(a00, matr[0, 0]);
            Assert.AreEqual(a01, matr[0, 1]);
            Assert.AreEqual(a10, matr[1, 0]);
            Assert.AreEqual(a11, matr[1, 1]);
        }

        [TestMethod]
        public void ArrayConstructor()
        {
            Complex[] arr = new Complex[] { 0, 1, 2, 3 };
            var matr = new MatrixNxN(2, 2, arr);
            Assert.AreEqual(arr[0], matr[0, 0]);
            Assert.AreEqual(arr[1], matr[0, 1]);
            Assert.AreEqual(arr[2], matr[1, 0]);
            Assert.AreEqual(arr[3], matr[1, 1]);
        }

        [TestMethod]
        public void CopyConstructor()
        {
            var matr1 = new MatrixNxN(2, 2, new Complex[] { 1, 2, 3, 4 });
            var matr2 = new MatrixNxN(matr1);
            Assert.AreEqual(matr1[0, 0], matr2[0, 0]);
            Assert.AreEqual(matr1[0, 1], matr2[0, 1]);
            Assert.AreEqual(matr1[1, 0], matr2[1, 0]);
            Assert.AreEqual(matr1[1, 1], matr2[1, 1]);
        }

        [TestMethod]
        public void Transpose()
        {
            MatrixNxN matr = new MatrixNxN(2, 2, new Complex[] { 1, 2, 3, 4 });
            MatrixNxN tra = matr.Transpose();
            Assert.AreEqual(matr[0, 0], tra[0, 0]);
            Assert.AreEqual(matr[0, 1], tra[1, 0]);
            Assert.AreEqual(matr[1, 0], tra[0, 1]);
            Assert.AreEqual(matr[1, 1], tra[1, 1]);
        }

        [TestMethod]
        public void EqualsOk()
        {
            var matr1 = new MatrixNxN(2, 2, new Complex[] { 1, 2, 3, 4 });
            var matr2 = new MatrixNxN(2, 2, new Complex[] { 1, 2, 3, 4 });
            Assert.IsTrue(matr1.Equals(matr2));
        }

        [TestMethod]
        public void EqualsFalse()
        {
            var matr1 = new MatrixNxN(2, 2, new Complex[] { 3, 2, 1, 0 });
            var matr2 = new MatrixNxN(2, 2, new Complex[] { 1, 2, 3, 4 });
            Assert.IsFalse(matr1.Equals(matr2));
        }

        [TestMethod]
        public void GetHashCodeTest()
        {
            var matr = new MatrixNxN(2, 2, new Complex[] { 3, 2, 1, 0 });
            var hash = matr.GetHashCode();
            int expectedHash = 0;
            for(int r = 0; r  < matr.Rows; r++)
            {
                for(int c = 0; c < matr.Columns; c++)
                {
                    expectedHash = expectedHash ^ matr[r, c].GetHashCode();
                }
            }
            Assert.AreEqual(expectedHash, hash);
        }

        [TestMethod]
        public void OperatorEQOk()
        {
            var matr1 = new MatrixNxN(2, 2, new Complex[] { 1, 2, 3, 4 });
            var matr2 = new MatrixNxN(2, 2, new Complex[] { 1, 2, 3, 4 });
            Assert.IsTrue(matr1 == matr2);
        }

        [TestMethod]
        public void OperatorEQFalse()
        {
            var matr1 = new MatrixNxN(2, 2, new Complex[] { 3, 2, 1, 0 });
            var matr2 = new MatrixNxN(2, 2, new Complex[] { 1, 2, 3, 4 });
            Assert.IsFalse(matr1 == matr2);
        }

        [TestMethod]
        public void OperatorNQOk()
        {
            var matr1 = new MatrixNxN(2, 2, new Complex[] { 3, 2, 1, 0 });
            var matr2 = new MatrixNxN(2, 2, new Complex[] { 1, 2, 3, 4 });
            Assert.IsTrue(matr1 != matr2);
        }

        [TestMethod]
        public void OperatorNQFalse()
        {
            var matr1 = new MatrixNxN(2, 2, new Complex[] { 1, 2, 3, 4 });
            var matr2 = new MatrixNxN(2, 2, new Complex[] { 1, 2, 3, 4 });
            Assert.IsFalse(matr1 != matr2);
        }

        [TestMethod]
        public void OperatorImplicitConvertToArray()
        {
            var matr = new MatrixNxN(2, 2, new Complex[] { 1, 2, 3, 4 });
            Complex[] arr = matr;
            Assert.AreEqual(arr[0], matr[0, 0]);
            Assert.AreEqual(arr[1], matr[0, 1]);
            Assert.AreEqual(arr[2], matr[1, 0]);
            Assert.AreEqual(arr[3], matr[1, 1]);
        }
 
        [TestMethod]
        public void OperatorImplicitConvertToArray2()
        {
            var matr = new MatrixNxN(2, 2, new Complex[] { 1, 2, 3, 4 });
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
            MatrixNxN matr = arr;
            Assert.AreEqual(arr[0][0], matr[0, 0]);
            Assert.AreEqual(arr[0][1], matr[0, 1]);
            Assert.AreEqual(arr[1][0], matr[1, 0]);
            Assert.AreEqual(arr[1][1], matr[1, 1]);
        }

        [TestMethod]
        public void OperatorDivideByScalar()
        {
            MatrixNxN matr = new MatrixNxN(2, 2, new Complex[] { 2, 4, 3, 2 });
            MatrixNxN res = matr / 2;
            Assert.AreEqual(matr[0, 0] / 2, res[0, 0]);
            Assert.AreEqual(matr[0, 1] / 2, res[0, 1]);
            Assert.AreEqual(matr[1, 0] / 2, res[1, 0]);
            Assert.AreEqual(matr[1, 1] / 2, res[1, 1]);
        }

        [TestMethod]
        public void OperatorMultiplyByScalar()
        {
            MatrixNxN matr = new MatrixNxN(2, 2, new Complex[] { 2, 4, 3, 2 });
            MatrixNxN res = matr * 2;
            Assert.AreEqual(matr[0, 0] * 2, res[0, 0]);
            Assert.AreEqual(matr[0, 1] * 2, res[0, 1]);
            Assert.AreEqual(matr[1, 0] * 2, res[1, 0]);
            Assert.AreEqual(matr[1, 1] * 2, res[1, 1]);
        }

        [TestMethod]
        public void OperatorAddToScalar()
        {
            MatrixNxN matr = new MatrixNxN(2, 2, new Complex[] { 2, 4, 3, 2 });
            MatrixNxN res = matr + 2;
            Assert.AreEqual(matr[0, 0] + 2, res[0, 0]);
            Assert.AreEqual(matr[0, 1] + 2, res[0, 1]);
            Assert.AreEqual(matr[1, 0] + 2, res[1, 0]);
            Assert.AreEqual(matr[1, 1] + 2, res[1, 1]);
        }

        [TestMethod]
        public void OperatorSubtractScalar()
        {
            MatrixNxN matr = new MatrixNxN(2, 2, new Complex[] { 2, 4, 3, 2 });
            MatrixNxN res = matr - 2;
            Assert.AreEqual(matr[0, 0] - 2, res[0, 0]);
            Assert.AreEqual(matr[0, 1] - 2, res[0, 1]);
            Assert.AreEqual(matr[1, 0] - 2, res[1, 0]);
            Assert.AreEqual(matr[1, 1] - 2, res[1, 1]);
        }

        [TestMethod]
        public void OperatorUnaryPlus()
        {
            MatrixNxN matr = new MatrixNxN(2, 2, new Complex[] { 2, 4, 3, 2 });
            MatrixNxN res = +matr;
            Assert.AreEqual(matr[0, 0], res[0, 0]);
            Assert.AreEqual(matr[0, 1], res[0, 1]);
            Assert.AreEqual(matr[1, 0], res[1, 0]);
            Assert.AreEqual(matr[1, 1], res[1, 1]);
        }

        [TestMethod]
        public void OperatorUnaryMinus()
        {
            MatrixNxN matr = new MatrixNxN(2, 2, new Complex[] { 2, 4, 3, 2 });
            MatrixNxN res = -matr;
            Assert.AreEqual(-matr[0, 0], res[0, 0]);
            Assert.AreEqual(-matr[0, 1], res[0, 1]);
            Assert.AreEqual(-matr[1, 0], res[1, 0]);
            Assert.AreEqual(-matr[1, 1], res[1, 1]);
        }

        [TestMethod]
        public void OperatorAddToMatrix()
        {
            MatrixNxN matr1 = new MatrixNxN(2, 2, new Complex[] { 2, 4, 1, 0 });
            MatrixNxN matr2 = new MatrixNxN(2, 2, new Complex[] { 3, -1, 0, 1 });
            MatrixNxN res = matr1 + matr2;
            Assert.AreEqual(matr1[0, 0] + matr2[0, 0], res[0, 0]);
            Assert.AreEqual(matr1[0, 1] + matr2[0, 1], res[0, 1]);
            Assert.AreEqual(matr1[1, 0] + matr2[1, 0], res[1, 0]);
            Assert.AreEqual(matr1[1, 1] + matr2[1, 1], res[1, 1]);
        }

        [TestMethod]
        public void OperatorSubtractMatrix()
        {
            MatrixNxN matr1 = new MatrixNxN(2, 2, new Complex[] { 2, 4, 1, 0 });
            MatrixNxN matr2 = new MatrixNxN(2, 2, new Complex[] { 3, -1, 0, 1 });
            MatrixNxN res = matr1 - matr2;
            Assert.AreEqual(matr1[0, 0] - matr2[0, 0], res[0, 0]);
            Assert.AreEqual(matr1[0, 1] - matr2[0, 1], res[0, 1]);
            Assert.AreEqual(matr1[1, 0] - matr2[1, 0], res[1, 0]);
            Assert.AreEqual(matr1[1, 1] - matr2[1, 1], res[1, 1]);
        }

        [TestMethod]
        public void DetZeroMatrix()
        {
            MatrixNxN zero = MatrixNxN.Zero(2, 2);
            Complex det = zero.Det();
            Assert.AreEqual(Complex.Zero, det);
        }

        [TestMethod]
        public void DetIdentityMatrix()
        {
            MatrixNxN I = MatrixNxN.I(2);
            Complex det = I.Det();
            Assert.AreEqual(Complex.One, det);
        }

        [TestMethod]
        public void DetSingularMatrix()
        {
            MatrixNxN M = new MatrixNxN(2, 2, new Complex[] { 1, 2, 2, 4 } );
            Complex det = M.Det();
            Assert.AreEqual(Complex.Zero, det);
        }

        [TestMethod]
        public void DetRegularMatrix()
        {
            MatrixNxN M = new MatrixNxN(2, 2, new Complex[] { 1, 2, 1, 4 });
            Complex det = M.Det();
            Assert.AreEqual((Complex)2, det);
        }

        [TestMethod]
        public void InvertIdentityMatrix()
        {
            MatrixNxN M = MatrixNxN.I(2);
            MatrixNxN res = M.Invert();
            Assert.AreEqual(MatrixNxN.I(2), res);
        }

        [TestMethod]
        [ExpectedException(typeof(System.DivideByZeroException))]
        public void InvertZeroMatrix()
        {
            MatrixNxN M = MatrixNxN.Zero(2, 2);
            MatrixNxN res = M.Invert();
        }

        [TestMethod]
        [ExpectedException(typeof(System.DivideByZeroException))]
        public void InvertSingularMatrix()
        {
            MatrixNxN M = new MatrixNxN(2, 2, new Complex[] { 1, 2, 2, 4 });
            MatrixNxN res = M.Invert();
        }

        [TestMethod]
        public void InvertRegularMatrix()
        {
            MatrixNxN M = new MatrixNxN(2, 2, new Complex[] { 1, 2, 1, 4 });
            MatrixNxN res = M.Invert();
            Assert.AreEqual(new MatrixNxN(2, 2, new Complex[] { 2, -1, -0.5, 0.5 }), res);
            Assert.AreEqual(MatrixNxN.I(2), M * res);
            Assert.AreEqual(MatrixNxN.I(2), res * M);
        }

        [TestMethod]
        public void Multiply2Matrixies()
        {
            MatrixNxN M1 = new MatrixNxN(2, 2, new Complex[] { 1, 2, 1, 4 });
            MatrixNxN M2 = new MatrixNxN(2, 2, new Complex[] { 1, 2, 1, 4 });
            MatrixNxN res = M1 * M2;
            Assert.AreEqual(new MatrixNxN(2, 2, new Complex[] { 3, 10, 5, 18 }), res);
        }

        [TestMethod]
        public void Divide2Matrixies()
        {
            MatrixNxN M1 = new MatrixNxN(2, 2, new Complex[] { 1, 2, 1, 4 });
            MatrixNxN M2 = new MatrixNxN(2, 2, new Complex[] { 1, 2, 1, 4 });
            MatrixNxN res = M1 / M2;
            Assert.AreEqual(MatrixNxN.I(2), res);
        }

        //[TestMethod]
        //public void LeftMultiplyBra()
        //{
        //    Bra bra = new Bra(1, 2);
        //    MatrixNxN M = new MatrixNxN(1, 2, 1, 4);
        //    Bra res = bra * M;
        //    Assert.AreEqual(new Bra(3, 10), res);
        //}

        //[TestMethod]
        //public void RightMultiplyKet()
        //{
        //    Ket ket = new Ket(1, 2);
        //    MatrixNxN M = new MatrixNxN(1, 2, 1, 4);
        //    Ket res = M * ket;
        //    Assert.AreEqual(new Ket(5, 9), res);
        //}

        [TestMethod]
        public void Kron()
        {
            MatrixNxN A = new MatrixNxN(2, 2, new Complex[]{ 1, 3, 5, 7 });
            MatrixNxN B = new MatrixNxN(2, 2, new Complex[]{ 2, 4, 6, 8 });
            MatrixNxN expectedAKronB = new MatrixNxN(4, 4, new Complex[]
            {
                2, 4, 6, 12,
                6, 8, 18, 24,
                10, 20, 14, 28,
                30, 40, 42, 56
            });
            MatrixNxN expectedBKronA = new MatrixNxN(4, 4, new Complex[]
            {
                2, 6, 4, 12,
                10, 14 ,20, 28,
                6, 18, 8, 24,
                30, 42, 40, 56,
            });

            var AkronB = A.Kron(B);
            var BkronA = B.Kron(A);

            Assert.AreEqual(expectedAKronB, AkronB);
            Assert.AreEqual(expectedBKronA, BkronA);
        }
    }
}
