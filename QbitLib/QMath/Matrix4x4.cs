using System;
using System.Numerics;

namespace QBits.QBits
{
    public class Matrix4x4
    {
        public static Matrix4x4 I = new Complex[] {
                                                  1, 0, 0, 0,
                                                  0, 1, 0, 0,
                                                  0, 0, 1, 0,
                                                  0, 0, 0, 1
                                            };
        public static Matrix4x4 Zero = new Complex[]
                                        {
                                                0, 0, 0, 0,
                                                0, 0, 0, 0,
                                                0, 0, 0, 0,
                                                0, 0, 0, 0
                                        };

        private Complex[] arr = new Complex[16];
        
        public int Rows => 4;
        public int Columns => 4;

        public Matrix4x4() { }
        public Matrix4x4(params Complex[] arr) {
            if (arr.Length != 16) throw new ArgumentException("Arr length must be 16");
            this.arr = (Complex[])arr.Clone();
        }
        public Matrix4x4(Matrix2x2 A, Matrix2x2 B, Matrix2x2 C, Matrix2x2 D)
        {
            this.arr = new Complex[] {
                A[0, 0], A[0, 1], B[0, 0], B[0, 1],
                A[1, 0], A[1, 1], B[1, 0], B[1, 1],
                C[0, 0], C[0, 1], D[0, 0], D[0, 1],
                C[1, 0], C[1, 1], D[1, 0], D[1, 1],
            };
        }

        public Complex this[int r, int c]
        {
            get
            {
                return arr[r * 4 + c];
            }
        }

        public Complex Det()
        {
            try
            {
                Matrix4x4 M = this.arr;
                Complex s = Reduce(M);
                return s * M[0, 0] * M[1, 1] * M[2, 2] * M[3, 3];
            } 
            catch (DivideByZeroException)
            {
                return Complex.Zero;
            }
        }

        private static Complex Reduce(Matrix4x4 M, Matrix4x4 M1 = null)
        {
            Complex ret = 1;
            for (int r = 0; r < 4; r++)
            {
                int rPivot = FindRigaPivot(M, r);
                if (rPivot < 0) throw new System.DivideByZeroException();
                if (rPivot != r)
                {
                    ScambiaRighe(M, M1, r, rPivot);
                    ret = -ret;
                }
                AzzeraElementiSotto(M, M1, r);
            }
            return ret;
        }

        private static int FindRigaPivot(Matrix4x4 M, int r)
        {
            int r1 = r;
            while (r1 < 4)
            {
                if (!EqualityHelper.AreEqual(M[r1, r], Complex.Zero))
                {
                    return r1;
                }
                r1 += 1;
            }
            return -1;
        }

        private static void ScambiaRighe(Matrix4x4 M, Matrix4x4 M1, int r1, int r2)
        {
            for(int c = 0; c < 4; c++)
            {
                Complex tmp = M[r1, c];
                M.arr[r1 * 4 + c] = M[r2, c];
                M.arr[r2 * 4 + c] = tmp;
            }
            if (M1 is object)
            {
                for (int c = 0; c < 4; c++)
                {
                    Complex tmp = M1[r1, c];
                    M1.arr[r1 * 4 + c] = M1[r2, c];
                    M1.arr[r2 * 4 + c] = tmp;
                }
            }
        }

        private static void AzzeraElementiSotto(Matrix4x4 M, Matrix4x4 M1, int r)
        {
            Complex pivot = M[r, r];
            for (int r1 = r + 1; r1 < 4; r1++)
            {
                if (!EqualityHelper.AreEqual(M[r1, r], Complex.Zero))
                {
                    Complex factor = M[r1, r] / pivot;
                    for (int c = r; c < 4; c++)
                    {
                        M.arr[r1 * 4 + c] = M[r1, c] - factor * M[r, c];
                    }
                    if (M1 is object)
                    {
                        for(int c = 0; c < 4; c++)
                        {
                            M1.arr[r1 * 4 + c] = M1[r1, c] - factor * M1[r, c];
                        }
                    }
                }
            }
        }

        public Matrix4x4 Transpose()
        {
            return new Complex[]{
                    this[0, 0], this[1, 0], this[2, 0], this[3, 0],
                    this[0, 1], this[1, 1], this[2, 1], this[3, 1],
                    this[0, 2], this[1, 2], this[2, 2], this[3, 2],
                    this[0, 3], this[1, 3], this[2, 3], this[3, 3],
                    };
        }

        public Matrix4x4 Invert()
        {
            var d = Det();
            if (EqualityHelper.AreEqual(d, Complex.Zero))
            {
                throw new DivideByZeroException("Singular matrix");
            }
            Matrix4x4 M = this.arr;
            Matrix4x4 M1 = I.arr;
            Reduce(M, M1);
            Diagonalize(M, M1);
            Normalize(M, M1);
            return M1;
        }

        private static void Diagonalize(Matrix4x4 M, Matrix4x4 M1)
        {
            for (int r = 4 - 1; r >= 0; r--)
            {
                AzzeraElementiSopra(M, M1, r);
            }
        }

        private static void AzzeraElementiSopra(Matrix4x4 M, Matrix4x4 M1, int r)
        {
            Complex pivot = M[r, r];
            for (int r1 = r - 1; r1 >= 0; r1--)
            {
                if (!EqualityHelper.AreEqual(M[r1, r], Complex.Zero))
                {
                    Complex factor = M[r1, r] / pivot;
                    for (int c = r; c < 4; c++)
                    {
                        M.arr[r1 * 4 + c] = M[r1, c] - factor * M[r, c];
                    }
                    for (int c = 0; c < 4; c++)
                    {
                        M1.arr[r1 * 4 + c] = M1[r1, c] - factor * M1[r, c];
                    }
                }
            }
        }

        private static void Normalize(Matrix4x4 M, Matrix4x4 M1)
        {
            for(int r = 0; r < 4; r++)
            {
                if (!EqualityHelper.AreEqual(M[r, r], Complex.One))
                {
                    Complex factor = Complex.One / M[r, r];
                    for(int c = 0; c < 4; c++)
                    {
                        M1.arr[r * 4 + c] = M1.arr[r * 4 + c] * factor;
                    }
                }
            }
        }

        public override bool Equals(object obj)
        {
            return (obj is Matrix4x4 M) && EqualityHelper.AreEqual(arr, M.arr);
        }

        public override string ToString()
        {
            return 
            $@"(
            {this[0, 0]}, {this[0, 1]}, {this[0, 2]}, {this[0, 3]},
            {this[1, 0]}, {this[1, 1]}, {this[1, 2]}, {this[1, 3]},
            {this[2, 0]}, {this[2, 1]}, {this[2, 2]}, {this[2, 3]},
            {this[3, 0]}, {this[3, 1]}, {this[3, 2]}, {this[3, 3]},
            )";
        }

        public override int GetHashCode()
        {
            return HashCalculatorHelper.Calculate(arr);
        }

        public static bool operator ==(Matrix4x4 A, Matrix4x4 B)
        {
            return A.Equals(B);
        }

        public static bool operator !=(Matrix4x4 A, Matrix4x4 B)
        {
            return !A.Equals(B);
        }

        public static implicit operator Complex[][] (Matrix4x4 m)
        {
            return new Complex[][] { 
                        new Complex[] { m[0, 0], m[0, 1], m[0, 2], m[0, 3] },
                        new Complex[] { m[1, 0], m[1, 1], m[1, 2], m[1, 3] },
                        new Complex[] { m[2, 0], m[2, 1], m[2, 2], m[2, 3] },
                        new Complex[] { m[3, 0], m[3, 1], m[3, 2], m[3, 3] },
                    };
        }

        public static implicit operator Complex[](Matrix4x4 m)
        {
            return (Complex[])m.arr.Clone();
        }

        public static implicit operator Matrix4x4 (Complex[][] m)
        {
            return new Complex[] {
                            m[0][0],m[0][1],m[0][2],m[0][3],
                            m[1][0],m[1][1],m[1][2],m[1][3],
                            m[2][0],m[2][1],m[2][2],m[2][3],
                            m[3][0],m[3][1],m[3][2],m[3][3],
                        };
        }

        public static implicit operator Matrix4x4 (Complex[] m)
        {
            return new Matrix4x4 { arr = (Complex[])m.Clone() };
        }

        public static Matrix4x4 operator /(Matrix4x4 m, Complex v)
        {
            return new Complex[] { 
                            m[0, 0] / v, m[0, 1] / v, m[0, 2] / v, m[0, 3] / v,
                            m[1, 0] / v, m[1, 1] / v, m[1, 2] / v, m[1, 3] / v,
                            m[2, 0] / v, m[2, 1] / v, m[2, 2] / v, m[2, 3] / v,
                            m[3, 0] / v, m[3, 1] / v, m[3, 2] / v, m[3, 3] / v,
            };
        }

        public static Matrix4x4 operator *(Matrix4x4 m, Complex v)
        {
            return new Complex[] {
                            m[0, 0] * v, m[0, 1] * v, m[0, 2] * v, m[0, 3] * v,
                            m[1, 0] * v, m[1, 1] * v, m[1, 2] * v, m[1, 3] * v,
                            m[2, 0] * v, m[2, 1] * v, m[2, 2] * v, m[2, 3] * v,
                            m[3, 0] * v, m[3, 1] * v, m[3, 2] * v, m[3, 3] * v,
            };
        }

        public static Matrix4x4 operator +(Matrix4x4 m, Complex v)
        {
            return new Complex[] {
                            m[0, 0] + v, m[0, 1] + v, m[0, 2] + v, m[0, 3] + v,
                            m[1, 0] + v, m[1, 1] + v, m[1, 2] + v, m[1, 3] + v,
                            m[2, 0] + v, m[2, 1] + v, m[2, 2] + v, m[2, 3] + v,
                            m[3, 0] + v, m[3, 1] + v, m[3, 2] + v, m[3, 3] + v,
            };
        }

        public static Matrix4x4 operator -(Matrix4x4 m, Complex v)
        {
            return new Complex[] {
                            m[0, 0] - v, m[0, 1] - v, m[0, 2] - v, m[0, 3] - v,
                            m[1, 0] - v, m[1, 1] - v, m[1, 2] - v, m[1, 3] - v,
                            m[2, 0] - v, m[2, 1] - v, m[2, 2] - v, m[2, 3] - v,
                            m[3, 0] - v, m[3, 1] - v, m[3, 2] - v, m[3, 3] - v,
            };
        }

        public static Matrix4x4 operator +(Matrix4x4 m)
        {
            return m;
        }

        public static Matrix4x4 operator -(Matrix4x4 m)
        {
            return new Complex[] {
                            -m[0, 0], -m[0, 1], -m[0, 2], -m[0, 3],
                            -m[1, 0], -m[1, 1], -m[1, 2], -m[1, 3],
                            -m[2, 0], -m[2, 1], -m[2, 2], -m[2, 3],
                            -m[3, 0], -m[3, 1], -m[3, 2], -m[3, 3],
            };
        }

        public static Matrix4x4 operator +(Matrix4x4 a, Matrix4x4 b)
        {
            return new Complex[] {
                            a[0, 0] + b[0, 0], a[0, 1] + b[0, 1], a[0, 2] + b[0, 2], a[0, 3] + b[0, 3],
                            a[1, 0] + b[1, 0], a[1, 1] + b[1, 1], a[1, 2] + b[1, 2], a[1, 3] + b[1, 3],
                            a[2, 0] + b[2, 0], a[2, 1] + b[2, 1], a[2, 2] + b[2, 2], a[2, 3] + b[2, 3],
                            a[3, 0] + b[3, 0], a[3, 1] + b[3, 1], a[3, 2] + b[3, 2], a[3, 3] + b[3, 3],
            };
        }

        public static Matrix4x4 operator -(Matrix4x4 a, Matrix4x4 b)
        {
            return new Complex[] {
                            a[0, 0] - b[0, 0], a[0, 1] - b[0, 1], a[0, 2] - b[0, 2], a[0, 3] - b[0, 3],
                            a[1, 0] - b[1, 0], a[1, 1] - b[1, 1], a[1, 2] - b[1, 2], a[1, 3] - b[1, 3],
                            a[2, 0] - b[2, 0], a[2, 1] - b[2, 1], a[2, 2] - b[2, 2], a[2, 3] - b[2, 3],
                            a[3, 0] - b[3, 0], a[3, 1] - b[3, 1], a[3, 2] - b[3, 2], a[3, 3] - b[3, 3],
            };
        }

        public static Matrix4x4 operator *(Matrix4x4 A, Matrix4x4 B)
        {
            Matrix4x4 C = new Matrix4x4();
            for (int r = 0; r < 4; r++)
            {
                for (int c = 0; c < 4; c++)
                {
                    Complex Crc = Complex.Zero;
                    for (int k = 0; k < 4; k++)
                    {
                        Crc = Crc + A[r, k] * B[k, c];
                    }
                    C.arr[r * 4 + c] = Crc;
                }
            }
            return C;
        }

        public static Matrix4x4 operator /(Matrix4x4 a, Matrix4x4 b)
        {
            return a * b.Invert();
        }
         
    }

}