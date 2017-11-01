using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrix
{
    public class MyMatrix<T>
    {
        private readonly T[,] _matrix;

        public MyMatrix(T[,] matrix)
        {
            _matrix = matrix;
        }

        public int Cols => _matrix.GetLength(0);

        public int Rows => _matrix.GetLength(1);

        public T this[int col, int row]
        {
            get => _matrix[col, row];
            set => _matrix[col, row] = value;
        }

        public static T operator +(MyMatrix<T> a, MyMatrix<T> b)
        {
            throw new NotImplementedException();
        }

        public static T operator *(MyMatrix<T> a, MyMatrix<T> b)
        {
            throw new NotImplementedException();
        }
    }
}
