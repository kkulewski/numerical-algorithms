using System;
using System.IO;
using System.Text;

namespace Matrix
{
    class MyMatrixTestAnalyzer
    {
        private readonly MyMatrixIoHandler _handler = new MyMatrixIoHandler();

        public void GaussTimeComparison()
        {
            var fractionFull = _handler.LoadDoubleVector(IO.PrefixFraction + IO.ResultFullPivot, true);
            var fractionPartial = _handler.LoadDoubleVector(IO.PrefixFraction + IO.ResultPartialPivot, true);
            var fractionNo = _handler.LoadDoubleVector(IO.PrefixFraction + IO.ResultNoPivot, true);

            var doubleFull = _handler.LoadDoubleVector(IO.PrefixDouble + IO.ResultFullPivot, true);
            var doublePartial = _handler.LoadDoubleVector(IO.PrefixDouble + IO.ResultPartialPivot, true);
            var doubleNo = _handler.LoadDoubleVector(IO.PrefixDouble + IO.ResultNoPivot, true);

            var floatFull = _handler.LoadDoubleVector(IO.PrefixFloat + IO.ResultFullPivot, true);
            var floatPartial = _handler.LoadDoubleVector(IO.PrefixFloat + IO.ResultPartialPivot, true);
            var floatNo = _handler.LoadDoubleVector(IO.PrefixFloat + IO.ResultNoPivot, true);

            var edoubleFull = _handler.LoadDoubleVector(IO.PrefixEigen + IO.PrefixDouble + IO.ResultFullPivot, true);
            var edoublePartial = _handler.LoadDoubleVector(IO.PrefixEigen + IO.PrefixDouble + IO.ResultPartialPivot, true);

            var efloatFull = _handler.LoadDoubleVector(IO.PrefixEigen + IO.PrefixFloat + IO.ResultFullPivot, true);
            var efloatPartial = _handler.LoadDoubleVector(IO.PrefixEigen + IO.PrefixFloat + IO.ResultPartialPivot, true);

            var timeHeader = string.Format
            (
                "{0};{1};{2};{3}",
                "typ",
                "Gauss-Full-Pivot",
                "Gauss-Partial-Pivot",
                "Gauss-No-Pivot"
            );

            var timeFraction = string.Format
            (
                "{0};{1};{2};{3}",
                "csh-fraction",
                fractionFull.Item2,
                fractionPartial.Item2,
                fractionNo.Item2
            );

            var timeDouble = string.Format
            (
                "{0};{1};{2};{3}",
                "csh-double",
                doubleFull.Item2,
                doublePartial.Item2,
                doubleNo.Item2
            );

            var timeFloat = string.Format
            (
                "{0};{1};{2};{3}",
                "csh-float",
                floatFull.Item2,
                floatPartial.Item2,
                floatNo.Item2
            );

            var timeEigenDouble = string.Format
            (
                "{0};{1};{2};{3}",
                "eig-double",
                edoubleFull.Item2,
                edoublePartial.Item2,
                string.Empty
            );

            var timeEigenFloat = string.Format
            (
                "{0};{1};{2};{3}",
                "eig-float",
                efloatFull.Item2,
                efloatPartial.Item2,
                string.Empty
            );

            var sb = new StringBuilder();
            sb.AppendLine(timeHeader);
            sb.AppendLine(timeEigenDouble);
            sb.AppendLine(timeEigenFloat);
            sb.AppendLine(timeDouble);
            sb.AppendLine(timeFloat);
            sb.AppendLine(timeFraction);

            File.WriteAllText(IO.SummaryTimeGauss, sb.ToString());
        }

        public void GaussNormComparison()
        {
            var fractionFull = _handler.LoadDoubleVector(IO.PrefixFraction + IO.ResultFullPivot, true);
            var fractionPartial = _handler.LoadDoubleVector(IO.PrefixFraction + IO.ResultPartialPivot, true);
            var fractionNo = _handler.LoadDoubleVector(IO.PrefixFraction + IO.ResultNoPivot, true);

            var doubleFull = _handler.LoadDoubleVector(IO.PrefixDouble + IO.ResultFullPivot, true);
            var doublePartial = _handler.LoadDoubleVector(IO.PrefixDouble + IO.ResultPartialPivot, true);
            var doubleNo = _handler.LoadDoubleVector(IO.PrefixDouble + IO.ResultNoPivot, true);

            var floatFull = _handler.LoadDoubleVector(IO.PrefixFloat + IO.ResultFullPivot, true);
            var floatPartial = _handler.LoadDoubleVector(IO.PrefixFloat + IO.ResultPartialPivot, true);
            var floatNo = _handler.LoadDoubleVector(IO.PrefixFloat + IO.ResultNoPivot, true);

            var edoubleFull = _handler.LoadDoubleVector(IO.PrefixEigen + IO.PrefixDouble + IO.ResultFullPivot, true);
            var edoublePartial = _handler.LoadDoubleVector(IO.PrefixEigen + IO.PrefixDouble + IO.ResultPartialPivot, true);

            var efloatFull = _handler.LoadDoubleVector(IO.PrefixEigen + IO.PrefixFloat + IO.ResultFullPivot, true);
            var efloatPartial = _handler.LoadDoubleVector(IO.PrefixEigen + IO.PrefixFloat + IO.ResultPartialPivot, true);

            var normHeader = string.Format
            (
                "{0};{1};{2};{3}",
                "typ",
                "Gauss-Full-Pivot",
                "Gauss-Partial-Pivot",
                "Gauss-No-Pivot"
            );

            var normDouble = string.Format
            (
                "{0};{1};{2};{3}",
                "csh-double",
                VectorNorm(doubleFull.Item1, fractionFull.Item1),
                VectorNorm(doublePartial.Item1, fractionPartial.Item1),
                VectorNorm(doubleNo.Item1, fractionNo.Item1)
            );

            var normFloat = string.Format
            (
                "{0};{1};{2};{3}",
                "csh-float",
                VectorNorm(floatFull.Item1, fractionFull.Item1),
                VectorNorm(floatPartial.Item1, fractionPartial.Item1),
                VectorNorm(floatNo.Item1, fractionNo.Item1)
            );

            var normEigenDouble = string.Format
            (
                "{0};{1};{2};{3}",
                "eig-double",
                VectorNorm(edoubleFull.Item1, fractionFull.Item1),
                VectorNorm(edoublePartial.Item1, fractionPartial.Item1),
                string.Empty
            );

            var normEigenFloat = string.Format
            (
                "{0};{1};{2};{3}",
                "eig-float",
                VectorNorm(efloatFull.Item1, fractionFull.Item1),
                VectorNorm(efloatPartial.Item1, fractionPartial.Item1),
                string.Empty
            );

            var sb = new StringBuilder();
            sb.AppendLine(normHeader);
            sb.AppendLine(normEigenDouble);
            sb.AppendLine(normEigenFloat);
            sb.AppendLine(normDouble);
            sb.AppendLine(normFloat);

            File.WriteAllText(IO.SummaryNormGauss, sb.ToString());
        }

        public void ElementaryOperationsTimeComparison()
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

            var timeHeader = string.Format
            (
                "{0};{1};{2};{3}",
                "typ",
                "A * X",
                "(A + B + C) * X",
                "A * (B * C)"
            );

            var timeFraction = string.Format
            (
                "{0};{1};{2};{3}",
                "csh-fraction",
                fractionAx.Item2,
                fractionAbcx.Item2,
                fractionAbc.Item2
            );

            var timeDouble = string.Format
            (
                "{0};{1};{2};{3}",
                "csh-double",
                doubleAx.Item2,
                doubleAbcx.Item2,
                doubleAbc.Item2
            );

            var timeFloat = string.Format
            (
                "{0};{1};{2};{3}",
                "csh-float",
                floatAx.Item2,
                floatAbcx.Item2,
                floatAbc.Item2
            );

            var timeEigenDouble = string.Format
            (
                "{0};{1};{2};{3}",
                "eig-double",
                edoubleAx.Item2,
                edoubleAbcx.Item2,
                edoubleAbc.Item2
            );

            var timeEigenFloat = string.Format
            (
                "{0};{1};{2};{3}",
                "eig-float",
                efloatAx.Item2,
                efloatAbcx.Item2,
                efloatAbc.Item2
            );

            var sb = new StringBuilder();
            sb.AppendLine(timeHeader);
            sb.AppendLine(timeEigenDouble);
            sb.AppendLine(timeEigenFloat);
            sb.AppendLine(timeDouble);
            sb.AppendLine(timeFloat);
            sb.AppendLine(timeFraction);

            File.WriteAllText(IO.SummaryTimeElementary, sb.ToString());
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
                "typ",
                "A * X",
                "(A + B + C) * X",
                "A * (B * C)"
            );

            var normDouble = string.Format
            (
                "{0};{1};{2};{3}",
                "csh-double",
                VectorNorm(doubleAx.Item1, fractionAx.Item1),
                VectorNorm(doubleAbcx.Item1, fractionAbcx.Item1),
                MatrixNorm(doubleAbc.Item1, fractionAbc.Item1)
            );

            var normFloat = string.Format
            (
                "{0};{1};{2};{3}",
                "csh-float",
                VectorNorm(floatAx.Item1, fractionAx.Item1),
                VectorNorm(floatAbcx.Item1, fractionAbcx.Item1),
                MatrixNorm(floatAbc.Item1, fractionAbc.Item1)
            );

            var normEigenDouble = string.Format
            (
                "{0};{1};{2};{3}",
                "eig-double",
                VectorNorm(edoubleAx.Item1, fractionAx.Item1),
                VectorNorm(edoubleAbcx.Item1, fractionAbcx.Item1),
                MatrixNorm(edoubleAbc.Item1, fractionAbc.Item1)
            );

            var normEigenFloat = string.Format
            (
                "{0};{1};{2};{3}",
                "eig-float",
                VectorNorm(efloatAx.Item1, fractionAx.Item1),
                VectorNorm(efloatAbcx.Item1, fractionAbcx.Item1),
                MatrixNorm(efloatAbc.Item1, fractionAbc.Item1)
            );

            var sb = new StringBuilder();
            sb.AppendLine(normHeader);
            sb.AppendLine(normEigenDouble);
            sb.AppendLine(normEigenFloat);
            sb.AppendLine(normDouble);
            sb.AppendLine(normFloat);

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
