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

        public void ReduceLeftBottomTriangle(T[] v)
        {
            // select row that will be used to reduce rows below it
            for (var i = 0; i < Cols - 1; i++)
            {
                if (this[i, i] == (dynamic) new T())
                    throw new ArgumentException("Matrix diagonal contains zero!");

                // loop on each row below selected row
                for (var j = i + 1; j < Rows; j++)
                {
                    ReduceRow(v, i, j);
                }
            }
        }

        public void ReduceRightTopTriangle(T[] v)
        {
            // select last row that will be used to reduce rows above it
            for (var i = Cols - 1; i >= 1; i--)
            {
                if (this[i, i] == (dynamic) new T())
                    throw new ArgumentException("Matrix diagonal contains zero!");

                // loop on each row above selected row
                for (var j = i - 1; j >= 0; j--)
                {
                    ReduceRow(v, i, j);
                }
            }
        }

        private void ReduceRow(T[] v, int i, int j)
        {
            // if current row is already reduced (leading 0) => return
            if (this[j, i] == (dynamic) new T())
                return;

            // get scalar for current row
            var scalar = this[j, i] / (dynamic) this[i, i];

            // substract selected row (multiplied by scalar) from current row
            for (var k = 0; k < Cols; k++)
            {
                // substract each column
                this[j, k] -= this[i, k] * scalar;
            }

            // substract selected vector row (multiplied by scalar) from current row
            v[j] -= v[i] * scalar;
        }

        public void ToIdentityMatrix(T[] v)
        {
            for (var i = 0; i < Cols; i++)
            {
                v[i] = v[i] / (dynamic) this[i, i];
                this[i, i] = this[i, i] / (dynamic) this[i, i];
            }
        }
    }
}
