using QBits.QBits;
using System;
using System.Numerics;

namespace QbitLib.Matricies
{
    public static class Matricies
    {
        public static T[] Allocate<T>(int rows, int columns)
        {
            return new T[rows * columns];
        }

        public static T GetItem<T>(T[] A, int rows, int columns, int r, int c)
        {
            return A[r * columns + c];
        }

        public static void SetItem<T>(T[] A, int rows, int columns, int r, int c, T value)
        {
            A[r * columns + c] = value;
        }

        public static T[] Clone<T>(T[] A, int rows, int columns)
        {
            return (T[])A.Clone();
        }

        public static void Copy<T>(T[] dest, T[] src, int rows, int columns)
        {
            for(int r = 0; r < rows; r++)
            {
                for(int c = 0; c < columns; c++)
                {
                    dest[r * columns + c] = src[r * columns + c];
                }
            }
        }

        public static void GetIdentity(Complex[] I, int order)
        {
            for (int r = 0; r < order; r++)
            {
                for (int c = 0; c < order; c++)
                {
                    if (r == c)
                    {
                        SetItem(I, order, order, r, c, Complex.One);
                    }
                    else
                    {
                        SetItem(I, order, order, r, c, Complex.Zero);
                    }
                }
            }
        }

        public static void GetZero(Complex[] M, int rows, int columns)
        {
            SetAll(M, rows, columns, Complex.Zero);
        }

        public static void SetAll<T>(T[] M, int rows, int columns, T value)
        {
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    SetItem(M, rows, columns, r, c, value);                     
                }
            }
        }

        public static void GetTranspose<T>(T[] M, int rows, int columns, T[] ret)
        {
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    SetItem(ret, columns, rows, c, r, GetItem(M, rows, columns, r, c));
                }
            } 
        }

        public static void MultiplyByScalar(Complex[] A, int rows, int columns, Complex value) 
        {
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    A[r * columns + c] = A[r * columns + c] * value;
                }
            }
        }

        public static void DivideByScalar(Complex[] A, int rows, int columns, Complex value)
        {
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    A[r * columns + c] = A[r * columns + c] / value;
                }
            }
        }

        public static void AddToScalar(Complex[] A, int rows, int columns, Complex value)
        {
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    A[r * columns + c] = A[r * columns + c] + value;
                }
            }
        }

        public static void SubstractScalar(Complex[] A, int rows, int columns, Complex value)
        {
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    A[r * columns + c] = A[r * columns + c] - value;
                }
            }
        }
 
        public static void GetOpposite(Complex[] M, int rows, int columns)
        {
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    SetItem(M, rows, columns, r, c, -GetItem(M, rows, columns, r, c));
                }
            }
        }

        public static void AddMatrix(Complex[] A, Complex[] B, int rows, int columns)
        {
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    SetItem(A, rows, columns, r, c, GetItem(A, rows, columns, r, c) + GetItem(B, rows, columns, r, c));
                }
            }
        }

        public static void SubtractMatrix(Complex[] A, Complex[] B, int rows, int columns)
        {
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    SetItem(A, rows, columns, r, c, GetItem(A, rows, columns, r, c) - GetItem(B, rows, columns, r, c));
                }
            }
        }

        public static void MatrixProduct(Complex[] C, Complex[] A, Complex[] B, int rowsA, int columnsA, int columnsB)
        {
            for (int r = 0; r < rowsA; r++)
            {
                for (int c = 0; c < columnsB; c++)
                {
                    Complex Crc = Complex.Zero;
                    for (int k = 0; k < columnsA; k++)
                    {
                        Crc = Crc + GetItem(A, rowsA, columnsA, r, k) * GetItem(B, columnsA, columnsB, k, c);
                    }
                    SetItem(C, rowsA, columnsB, r, c, Crc);
                }
            }
        }

        public static void KronekerProduct(Complex[] C, Complex[] A, Complex[] B, int rowsA, int columnsA, int rowsB, int columnsB)
        {
            int rowsC = rowsA * rowsB;
            int columnsC = columnsA * columnsB;
            for (int rA = 0; rA < rowsA; rA++)
            {
                for (int cA = 0; cA < columnsA; cA++)
                {
                    Complex Arc = GetItem(A, rowsA, columnsA, rA, cA);
                    for(int rB = 0; rB < rowsB; rB ++)
                    {
                        for(int cB = 0; cB < columnsB; cB++)
                        {
                            Complex Brc = GetItem(B, rowsB, columnsB, rB, cB);
                            int rC = rA * rowsB + rB;
                            int cC = cA * columnsB + cB;
                            SetItem(C, rowsC, columnsC, rC, cC, Arc * Brc);
                        }                        
                    }
                }
            }
        }

        public static Complex Det(Complex[] A, int rows, int columns)
        {
            if (rows != columns)
            {
                throw new Exception("Not a square matrix");
            }
            int order = rows;
            try
            {
                var s = GaussReduce(A, null, order, order);
                for (int r = 0; r < order; r++)
                {
                    s = s * GetItem(A, order, order, r, r);
                }
                return s;
            }
            catch (DivideByZeroException)
            {
                return Complex.Zero;
            }
        }

        public static Complex GaussReduce(Complex[] M, Complex[] M1, int rows, int columns)
        {
            Complex ret = 1;
            for (int r = 0; r < rows; r++)
            {
                int rPivot = FindRigaPivot(M, rows, columns, r);
                if (rPivot < 0) throw new System.DivideByZeroException();
                if (rPivot != r)
                {
                    ScambiaRighe(M, rows, columns, r, rPivot);
                    if (M1 is object)
                    {
                        ScambiaRighe(M1, rows, columns, r, rPivot);
                    }
                    ret = -ret;
                }
                AzzeraElementiSotto(M, M1, rows, columns, r);
            }
            return ret;
        }

        private static int FindRigaPivot(Complex[] M, int rows, int columns, int r)
        {
            int r1 = r;
            while (r1 < rows)
            {
                if (!EqualityHelper.AreEqual(GetItem(M, rows, rows, r1, r), Complex.Zero))
                {
                    return r1;
                }
                r1 += 1;
            }
            return -1;
        }

        public static void ScambiaRighe(Complex[] M, int rows, int columns, int r1, int r2)
        {
            for (int c = 0; c < columns; c++)
            {
                Complex tmp = GetItem(M, rows, columns, r1, c);
                SetItem(M, rows, columns, r1, c, GetItem(M, rows, columns, r2, c));
                SetItem(M, rows, columns, r2, c, tmp);
            }
        }

        private static void AzzeraElementiSotto(Complex[] M, Complex[] M1, int rows, int columns, int r)
        {
            Complex pivot = GetItem(M, rows, columns, r, r);
            for (int r1 = r + 1; r1 < rows; r1++)
            {
                if (!EqualityHelper.AreEqual(GetItem(M, rows, columns, r1, r), Complex.Zero))
                {
                    Complex factor = GetItem(M, rows, columns, r1, r) / pivot;
                    for (int c = r; c < columns; c++)
                    {
                        SetItem(M, rows, columns, r1, c, GetItem(M, rows, columns, r1, c) - factor * GetItem(M, rows, columns, r, c));
                    }
                    if (M1 is object)
                    {
                        for (int c = 0; c < columns; c++)
                        {
                            SetItem(M1, rows, columns, r1, c, GetItem(M1, rows, columns, r1, c) - factor * GetItem(M1, rows, columns, r, c));
                        }
                    }
                }
            }
        }

        public static void GetInverse(Complex[] A, Complex[] M1, int rows, int columns)
        {
            var d = Det(A, rows, columns);
            if (EqualityHelper.AreEqual(d, Complex.Zero))
            {
                throw new DivideByZeroException("Singular matrix");
            }
            int order = rows;
            GetIdentity(M1, order);
            GaussReduce(A, M1, order, order);
            Diagonalize(A, M1, order);
            Normalize(A, M1, order);
        }

        private static void Diagonalize(Complex[] M, Complex[] M1, int order)
        {
            for (int r = order - 1; r >= 0; r--)
            {
                AzzeraElementiSopra(M, M1, order, r);
            }
        }

        private static void AzzeraElementiSopra(Complex[] M, Complex[] M1, int order, int r)
        {
            Complex pivot = GetItem(M, order, order, r, r);
            for (int r1 = r - 1; r1 >= 0; r1--)
            {
                if (!EqualityHelper.AreEqual(GetItem(M, order, order, r1, r), Complex.Zero))
                {
                    Complex factor = GetItem(M, order, order, r1, r) / pivot;
                    for (int c = r; c < order; c++)
                    {
                        SetItem(M, order, order, r1 , c, GetItem(M, order, order, r1, c) - factor * GetItem(M, order, order, r, c));
                    }
                    for (int c = 0; c < order; c++)
                    {
                        SetItem(M1, order, order, r1, c, GetItem(M1, order, order, r1, c) - factor * GetItem(M1, order, order, r, c));
                    }
                }
            }
        }

        private static void Normalize(Complex[] M, Complex[] M1, int order)
        {
            for (int r = 0; r < order; r++)
            {
                if (!EqualityHelper.AreEqual(GetItem(M, order, order, r, r), Complex.One))
                {
                    Complex factor = Complex.One / GetItem(M, order, order, r, r);
                    for (int c = 0; c < order; c++)
                    {
                        SetItem(M, order, order, r, c, GetItem(M, order, order, r, c) * factor);
                    }
                    for (int c = 0; c < order; c++)
                    {
                        SetItem(M1, order, order, r , c, GetItem(M1, order, order, r , c) * factor);
                    }
                }
            }
        }

    }
}
