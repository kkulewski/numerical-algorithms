using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrix
{
    public class MyMatrix<T>
    {
        private T[,] _matrix;

        public MyMatrix(T[,] matrix)
        {
            _matrix = matrix;
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
