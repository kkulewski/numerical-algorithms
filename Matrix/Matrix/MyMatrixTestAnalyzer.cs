using System;

namespace Matrix
{
    class MyMatrixTestAnalyzer
    {
        private MyMatrixIoHandler _handler = new MyMatrixIoHandler();

        public void CompareGaussNoPivot()
        {
            var fractionTime = _handler.LoadDoubleVector(IO.PrefixFraction + IO.ResultNoPivot, true).Item2;
            var doubleTime = _handler.LoadDoubleVector(IO.PrefixDouble + IO.ResultNoPivot, true).Item2;
            var floatTime = _handler.LoadFloatVector(IO.PrefixFloat + IO.ResultNoPivot, true).Item2;

            Console.WriteLine("fr: {0}\ndo: {1}\nfl: {2}", 
                fractionTime, doubleTime, floatTime);
        }

        public void CompareGaussPartialPivot()
        {

            var fr = _handler.LoadDoubleVector(IO.PrefixFraction + IO.ResultPartialPivot, true);

            var d = _handler.LoadDoubleVector(IO.PrefixDouble + IO.ResultPartialPivot, true);
            var f = _handler.LoadFloatVector(IO.PrefixFloat + IO.ResultPartialPivot, true);

            var ed = _handler.LoadDoubleVector(IO.PrefixEigen + IO.PrefixDouble + IO.ResultPartialPivot, true);
            var ef = _handler.LoadFloatVector(IO.PrefixEigen + IO.PrefixFloat + IO.ResultPartialPivot, true);
        }

        private static double VectorNorm<T1, T2>(T1[] vector, T2[] refVector)
        {
            var sum = 0.0;
            for (var i = 0; i < vector.Length; i++)
            {
                sum += (refVector[i] - (dynamic) vector[i]) * (refVector[i] - (dynamic) vector[i]);
            }

            return Math.Sqrt(sum);
        }

        private static double MatrixNorm<T1, T2>(MyMatrix<T1> matrix, MyMatrix<T2> refMatrix) where T1: new() where T2 : new()
        {
            var sum = 0.0;
            for (var i = 0; i < matrix.Rows; i++)
            {
                for (var j = 0; j < matrix.Cols; j++)
                {
                    sum += (refMatrix[i, j] - (dynamic) matrix[i, j]) * (refMatrix[i, j] - (dynamic) matrix[i, j]);
                }
            }

            return Math.Sqrt(sum);
        }
    }
}
