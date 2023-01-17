using Microsoft.VisualStudio.TestTools.UnitTesting;
using QBits.QBits;

namespace QbitLib.Tests
{
    [TestClass]
    public class Matrix4x4Tests
    {
        [TestMethod]
        public void DefaultConstructor()
        {
            var matr = new Matrix4x4();
            Assert.AreEqual(4, matr.Rows);
            Assert.AreEqual(4, matr.Columns);
        }
         

        [TestMethod]
        public void Det()
        {
            var A = new Matrix4x4(
                 2,    4,     6,    12,
                 6,    8,    18,    24,
                10,    20,    14,    28,
                30,    40,    42,    56);
            var det = A.Det();
            var expectedDet = (System.Numerics.Complex)4096;
            Assert.AreEqual(expectedDet, det);
        }

        [TestMethod]
        public void DetSingular()
        {
            var A = new Matrix4x4(
                 2, 4, 6, 12,
                 4, 8, 12, 24,
                10, 20, 14, 28,
                30, 40, 42, 56);
            var det = A.Det();
            var expectedDet = (System.Numerics.Complex)0;
            Assert.AreEqual(expectedDet, det);
        }

        [TestMethod]
        public void Transpose()
        {
            Matrix4x4 A = new Matrix4x4(
                        1, 2, 3, 4,
                        1, 2, 3, 4,
                        1, 2, 3, 4,
                        1, 2, 3, 4
                        );
            Matrix4x4 expectedA_ = new Matrix4x4(
                        1, 1, 1, 1,
                        2, 2, 2, 2,
                        3, 3, 3, 3,
                        4, 4, 4, 4
                        );
            Matrix4x4 A_ = A.Transpose();
            Assert.AreEqual(expectedA_, A_);
        }

        [TestMethod]
        public void Operator_UnaryPlus()
        {
            Matrix4x4 A = new Matrix4x4(
                        1, 2, 3, 4,
                        1, 2, 3, 4,
                        1, 2, 3, 4,
                        1, 2, 3, 4
                        );
            Matrix4x4 B = +A;
            Assert.AreEqual(A, B);
        }

        [TestMethod]
        public void Operator_UnaryMinus()
        {
            Matrix4x4 A = new Matrix4x4(
                        1, 2, 3, 4,
                        1, 2, 3, 4,
                        1, 2, 3, 4,
                        1, 2, 3, 4
                        );
            Matrix4x4 B = -A;
            Matrix4x4 expectedB = new Matrix4x4(
                        -1, -2, -3, -4,
                        -1, -2, -3, -4,
                        -1, -2, -3, -4,
                        -1, -2, -3, -4
                        );
            Assert.AreEqual(expectedB, B);
        }

        [TestMethod]
        public void Operator_Add()
        {
            Matrix4x4 A = new Matrix4x4(
                        1, 2, 3, 4,
                        1, 2, 3, 4,
                        1, 2, 3, 4,
                        1, 2, 3, 4
                        );
            Matrix4x4 B = new Matrix4x4(
                        -1, -2, -3, -4,
                        1,-2, 3, 4,
                        1, 2, -3, 4,
                        1, 2, 3, -4
                        );
            Matrix4x4 C = A + B;

            Matrix4x4 expectedC = new Matrix4x4(
                        0, 0, 0, 0,
                        2, 0, 6, 8,
                        2, 4, 0, 8,
                        2, 4, 6, 0
                        );
            Assert.AreEqual(expectedC, C);
        }

        [TestMethod]
        public void Operator_Sub()
        {
            Matrix4x4 A = new Matrix4x4(
                        1, 2, 3, 4,
                        1, 2, 3, 4,
                        1, 2, 3, 4,
                        1, 2, 3, 4
                        );
            Matrix4x4 B = new Matrix4x4(
                        -1, -2, -3, -4,
                        1, -2, 3, 4,
                        1, 2, -3, 4,
                        1, 2, 3, -4
                        );
            Matrix4x4 C = A - B;

            Matrix4x4 expectedC = new Matrix4x4(
                        2, 4, 6, 8,
                        0, 4, 0, 0,
                        0, 0, 6, 0,
                        0, 0, 0, 8
                        );
            Assert.AreEqual(expectedC, C);
        }

        [TestMethod]
        public void Operator_Mul()
        {
            Matrix4x4 A = new Matrix4x4(
                        2, 4, 6, 12,
                         6, 8, 18, 24,
                        10, 20, 14, 28,
                        30, 40, 42, 56
                        );
            Matrix4x4 B = new Matrix4x4(
                       0.875000000000001, -0.437500000000000, -0.375000000000000, 0.187500000000000,
                      -0.656250000000000, 0.218750000000000, 0.281250000000000, -0.093750000000000,
                      -0.625000000000000, 0.312500000000000, 0.125000000000000, -0.062500000000000,
                       0.468750000000000, -0.156250000000000, -0.093750000000000, 0.031250000000000
                        );
            Assert.AreEqual(Matrix4x4.I, A * B);
        }

        [TestMethod]
        public void Operator_Div()
        {
            Matrix4x4 A = new Matrix4x4(
                        1, 2, 3, 4,
                        1, 2, 3, 4,
                        1, 2, 3, 4,
                        1, 2, 3, 4
                        );
            Matrix4x4 I = Matrix4x4.I;
            Assert.AreEqual(A, A / I);
        }

        [TestMethod]
        public void Invert()
        {
            Matrix4x4 A = new Matrix4x4(
                        2, 4, 6, 12,
                         6, 8, 18, 24,
                        10, 20, 14, 28,
                        30, 40, 42, 56
                        );
            Matrix4x4 A1 = A.Invert();
            Matrix4x4 expectedA1 = new Matrix4x4(
                       0.875000000000001, -0.437500000000000, -0.375000000000000,  0.187500000000000,
                      -0.656250000000000,  0.218750000000000,  0.281250000000000, -0.093750000000000,
                      -0.625000000000000,  0.312500000000000,  0.125000000000000, -0.062500000000000,
                       0.468750000000000,- 0.156250000000000, -0.093750000000000,  0.031250000000000
                        );
            Assert.AreEqual(expectedA1, A1);
            Assert.AreEqual(Matrix4x4.I, A1 * A);
            Assert.AreEqual(Matrix4x4.I, A * A1);
        }

        
    }
}
