using System;
using System.IO;
using System.Text;

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


            // COMPARE NORMS
            var dNorm = VectorNorm(d.Item1, fr.Item1);
            var fNorm = VectorNorm(f.Item1, fr.Item1);
            var edNorm = VectorNorm(ed.Item1, fr.Item1);
            var efNorm = VectorNorm(ef.Item1, fr.Item1);

            Console.WriteLine("csharp double norm: " + dNorm);
            Console.WriteLine("cshrp  float  norm: " + fNorm);
            Console.WriteLine("eigen  double norm: " + edNorm);
            Console.WriteLine("eigen  float  norm: " + efNorm);

            // COMPARE DURATION
            Console.WriteLine("csharp double time: " + d.Item2);
            Console.WriteLine("cshrp  float  time: " + f.Item2);
            Console.WriteLine("eigen  double time: " + ed.Item2);
            Console.WriteLine("eigen  float  time: " + ef.Item2);
        }

        public void ElementaryOperationsNormComparison()
        {
            var fractionAx = _handler.LoadDoubleVector(IO.PrefixFraction + IO.ResultAx, true);
            var fractionAbcx = _handler.LoadDoubleVector(IO.PrefixFraction + IO.ResultAbcx, true);
            var fractionAbc = _handler.LoadDoubleMatrix(IO.PrefixFraction + IO.ResultAbc, true);

            var doubleAx = _handler.LoadDoubleVector(IO.PrefixDouble + IO.ResultAx, true);
            var doubleAbcx = _handler.LoadDoubleVector(IO.PrefixDouble + IO.ResultAbcx, true);
            var doubleAbc = _handler.LoadDoubleMatrix(IO.PrefixDouble + IO.ResultAbc, true);

            var floatAx = _handler.LoadDoubleVector(IO.PrefixFloat + IO.ResultAx, true);
            var floatAbcx = _handler.LoadDoubleVector(IO.PrefixFloat + IO.ResultAbcx, true);
            var floatAbc = _handler.LoadDoubleMatrix(IO.PrefixFloat + IO.ResultAbc, true);

            var edoubleAx = _handler.LoadDoubleVector(IO.PrefixEigen + IO.PrefixDouble + IO.ResultAx, true);
            var edoubleAbcx = _handler.LoadDoubleVector(IO.PrefixEigen + IO.PrefixDouble + IO.ResultAbcx, true);
            var edoubleAbc = _handler.LoadDoubleMatrix(IO.PrefixEigen + IO.PrefixDouble + IO.ResultAbc, true);

            var efloatAx = _handler.LoadDoubleVector(IO.PrefixEigen + IO.PrefixFloat + IO.ResultAx, true);
            var efloatAbcx = _handler.LoadDoubleVector(IO.PrefixEigen + IO.PrefixFloat + IO.ResultAbcx, true);
            var efloatAbc = _handler.LoadDoubleMatrix(IO.PrefixEigen + IO.PrefixFloat + IO.ResultAbc, true);

            var normHeader = string.Format
            (
                "{0};{1};{2};{3}",
                "type",
                "A-mul-X",
                "(A-add-B-add-C)-mul-X",
                "A-mul-(B-mul-C)"
            );

            var normDouble = string.Format
            (
                "{0};{1};{2};{3}",
                "csh_double",
                VectorNorm(doubleAx.Item1, fractionAx.Item1),
                VectorNorm(doubleAbcx.Item1, fractionAbcx.Item1),
                MatrixNorm(doubleAbc.Item1, fractionAbc.Item1)
            );

            var normFloat = string.Format
            (
                "{0};{1};{2};{3}",
                "csh_float",
                VectorNorm(floatAx.Item1, fractionAx.Item1),
                VectorNorm(floatAbcx.Item1, fractionAbcx.Item1),
                MatrixNorm(floatAbc.Item1, fractionAbc.Item1)
            );

            var normEigenDouble = string.Format
            (
                "{0};{1};{2};{3}",
                "eig_double",
                VectorNorm(edoubleAx.Item1, fractionAx.Item1),
                VectorNorm(edoubleAbcx.Item1, fractionAbcx.Item1),
                MatrixNorm(edoubleAbc.Item1, fractionAbc.Item1)
            );

            var normEigenFloat = string.Format
            (
                "{0};{1};{2};{3}",
                "eig_float",
                VectorNorm(efloatAx.Item1, fractionAx.Item1),
                VectorNorm(efloatAbcx.Item1, fractionAbcx.Item1),
                MatrixNorm(efloatAbc.Item1, fractionAbc.Item1)
            );

            var sb = new StringBuilder();
            sb.AppendLine(normHeader);
            sb.AppendLine(normDouble);
            sb.AppendLine(normEigenDouble);
            sb.AppendLine(normFloat);
            sb.AppendLine(normEigenFloat);

            File.WriteAllText(IO.SummaryNormElementary, sb.ToString());
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
