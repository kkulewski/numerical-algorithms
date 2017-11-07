using System;

namespace Matrix
{
    public class MyMatrix<T> where T: new()
    {
        private readonly T[,] _matrix;

        public MyMatrix(T[,] matrix)
        {
            _matrix = matrix;
        }

        public int Rows => _matrix.GetLength(0);

        public int Cols => _matrix.GetLength(1);

        public T this[int row, int col]
        {
            get => _matrix[row, col];
            set => _matrix[row, col] = value;
        }

        public static MyMatrix<T> operator +(MyMatrix<T> a, MyMatrix<T> b)
        {
            if(a.Rows != b.Rows || a.Cols != b.Cols)
                throw new ArgumentException("Matrix sizes are not equal.");

            var output = new T[a.Rows, a.Cols];
            for (var i = 0; i < a.Rows; i++)
            {
                for (var j = 0; j < a.Cols; j++)
                {
                    output[i, j] = (dynamic)a[i, j] + (dynamic)b[i, j];
                }
            }

            return new MyMatrix<T>(output);
        }

        public static MyMatrix<T> operator *(MyMatrix<T> a, MyMatrix<T> b)
        {
            if (a.Cols != b.Rows)
                throw new ArgumentException("Matrix sizes are not equal.");

            var output = new T[a.Rows, b.Cols];
            for (var row = 0; row < a.Rows; row++)
            {
                for (var col = 0; col < b.Cols; col++)
                {
                    var sum = new T();
                    for (var k = 0; k < a.Cols; k++)
                    {
                        sum += (dynamic) a[row, k] * (dynamic) b[k, col];
                    }
                    
                    output[row, col] = sum;
                }
            }

            return new MyMatrix<T>(output);
        }

        public static void GaussReduction(MyMatrix<T> m, T[] v)
        {
            // select row used to reset rows below it
            for (int i = 0; i < m.Cols-1; i++)
            {
                if (m[i, i] == (dynamic)new T())
                    throw new ArgumentException("Matrix diagonal contains zero!");

                // loop on each row below selected row
                for (int j = i+1; j < m.Cols; j++)
                {
                    // if current row has leading num is not 0
                    if (m[j, i] != (dynamic) new T())
                    {
                        // get scalar for current row
                        var scalar = m[j, i] / (dynamic) m[i, i];

                        // substract selected row (multiplied by scalar) from current row
                        for (int k = 0; k < m.Cols; k++)
                        {
                            // substract each column
                            m[j, k] -= m[i, k] * scalar;
                        }

                        // substract selected vector row (multiplied by scalar) from current row
                        v[j] -= v[i] * scalar;
                    }
                }
            }
        }

        public static void GaussReductionCoefficents(MyMatrix<T> m, T[] v)
        {
            for (int i = m.Cols-1; i >= 1; i--)
            {
                for (int j = i - 1; j >= 0; j--)
                {
                    if (m[j, i] != (dynamic) new T())
                    {
                        var scalar = m[j, i] / (dynamic) m[i, i];

                        for (int k = 0; k < m.Cols; k++)
                        {
                            m[j, k] -= m[i, k] * scalar;
                        }

                        v[j] -= v[i] * scalar;
                    }
                }
            }
        }

        public static void GaussReductionIdentityMatrix(MyMatrix<T> m, T[] v)
        {
            for (int i = 0; i < m.Cols; i++)
            {
                v[i] = v[i] / (dynamic) m[i, i];
                m[i, i] = m[i, i] / (dynamic) m[i, i];
            }
        }
    }
}
