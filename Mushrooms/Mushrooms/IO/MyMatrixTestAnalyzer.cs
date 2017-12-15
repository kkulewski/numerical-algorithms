using System;
using System.IO;
using System.Text;

namespace Mushrooms.IO
{
    public class MyMatrixTestAnalyzer
    {
        public static void TimeSummary()
        {
            var cshGaussSeidel = MyMatrixIoHandler.LoadDoubleVector(IoConsts.CsharpGaussSeidel, true);
            var cshJacobi = MyMatrixIoHandler.LoadDoubleVector(IoConsts.CsharpJacobi, true);
            var cshGaussPartial = MyMatrixIoHandler.LoadDoubleVector(IoConsts.CsharpGaussPartialPivot, true);
            var cshGaussPartialSparse = MyMatrixIoHandler.LoadDoubleVector(IoConsts.CsharpGaussPartialPivotSparse, true);
            var eigenGaussPartial = MyMatrixIoHandler.LoadDoubleVector(IoConsts.EigenGaussPartialPivot, true);
            var eigenGaussPartialSparse = MyMatrixIoHandler.LoadDoubleVector(IoConsts.EigenGaussPartialPivotSparse, true);

            var sb = new StringBuilder();
            sb.AppendLine(GetRow("op", "time"));
            sb.AppendLine(GetRow("csh-gauss-seidel", cshGaussSeidel.Item2));
            sb.AppendLine(GetRow("csh-jacobi", cshJacobi.Item2));
            sb.AppendLine(GetRow("csh-gauss-partial", cshGaussPartial.Item2));
            sb.AppendLine(GetRow("csh-gauss-partial-sparse", cshGaussPartialSparse.Item2));
            sb.AppendLine(GetRow("eig-gauss-partial", eigenGaussPartial.Item2));
            sb.AppendLine(GetRow("eig-gauss-partial-sparse", eigenGaussPartialSparse.Item2));
        }

        public static void NormSummary()
        {
            var cshGaussSeidel = MyMatrixIoHandler.LoadDoubleVector(IoConsts.CsharpGaussSeidel, true);
            var cshJacobi = MyMatrixIoHandler.LoadDoubleVector(IoConsts.CsharpJacobi, true);
            var cshGaussPartial = MyMatrixIoHandler.LoadDoubleVector(IoConsts.CsharpGaussPartialPivot, true);
            var cshGaussPartialSparse = MyMatrixIoHandler.LoadDoubleVector(IoConsts.CsharpGaussPartialPivotSparse, true);
            var eigenGaussPartial = MyMatrixIoHandler.LoadDoubleVector(IoConsts.EigenGaussPartialPivot, true);
            var eigenGaussPartialSparse = MyMatrixIoHandler.LoadDoubleVector(IoConsts.EigenGaussPartialPivotSparse, true);

            var sb = new StringBuilder();
            sb.AppendLine(GetRow("op", "norm"));
            sb.AppendLine(GetRow("csh-gauss-seidel", MyMatrix<double>.VectorNorm(cshGaussSeidel.Item1, eigenGaussPartial.Item1)));
            sb.AppendLine(GetRow("csh-jacobi", MyMatrix<double>.VectorNorm(cshJacobi.Item1, eigenGaussPartial.Item1)));
            sb.AppendLine(GetRow("csh-gauss-partial", MyMatrix<double>.VectorNorm(cshGaussPartial.Item1, eigenGaussPartial.Item1)));
            sb.AppendLine(GetRow("csh-gauss-partial-sparse", MyMatrix<double>.VectorNorm(cshGaussPartialSparse.Item1, eigenGaussPartial.Item1)));
            sb.AppendLine(GetRow("eig-gauss-partial", MyMatrix<double>.VectorNorm(eigenGaussPartial.Item1, eigenGaussPartial.Item1)));
            sb.AppendLine(GetRow("eig-gauss-partial-sparse", MyMatrix<double>.VectorNorm(eigenGaussPartialSparse.Item1, eigenGaussPartial.Item1)));

            File.WriteAllText(IoConsts.SummaryNorm, sb.ToString());
        }

        public static void WinChanceTimeSummary()
        {
            var cshGaussSeidel = MyMatrixIoHandler.LoadDoubleVector(IoConsts.PrefixWinChance + IoConsts.CsharpGaussSeidel, true);
            var cshJacobi = MyMatrixIoHandler.LoadDoubleVector(IoConsts.PrefixWinChance + IoConsts.CsharpJacobi, true);
            var cshGaussPartial = MyMatrixIoHandler.LoadDoubleVector(IoConsts.PrefixWinChance + IoConsts.CsharpGaussPartialPivot, true);
            var cshGaussPartialSparse = MyMatrixIoHandler.LoadDoubleVector(IoConsts.PrefixWinChance + IoConsts.CsharpGaussPartialPivotSparse, true);
            var eigenGaussPartial = MyMatrixIoHandler.LoadDoubleVector(IoConsts.PrefixWinChance + IoConsts.EigenGaussPartialPivot, true);
            var eigenGaussPartialSparse = MyMatrixIoHandler.LoadDoubleVector(IoConsts.PrefixWinChance + IoConsts.EigenGaussPartialPivotSparse, true);

            var sb = new StringBuilder();
            sb.AppendLine(GetRow("op", "time"));
            sb.AppendLine(GetRow("csh-gauss-seidel", cshGaussSeidel.Item2));
            sb.AppendLine(GetRow("csh-jacobi", cshJacobi.Item2));
            sb.AppendLine(GetRow("csh-gauss-partial", cshGaussPartial.Item2));
            sb.AppendLine(GetRow("csh-gauss-partial-sparse", cshGaussPartialSparse.Item2));
            sb.AppendLine(GetRow("eig-gauss-partial", eigenGaussPartial.Item2));
            sb.AppendLine(GetRow("eig-gauss-partial-sparse", eigenGaussPartialSparse.Item2));

            File.WriteAllText(IoConsts.SummaryWinChanceTime, sb.ToString());
        }

        public static void WinChanceErrorSummary()
        {
            var cshGaussSeidel = MyMatrixIoHandler.LoadDoubleVector(IoConsts.PrefixWinChance + IoConsts.CsharpGaussSeidel, true);
            var cshJacobi = MyMatrixIoHandler.LoadDoubleVector(IoConsts.PrefixWinChance + IoConsts.CsharpJacobi, true);
            var cshGaussPartial = MyMatrixIoHandler.LoadDoubleVector(IoConsts.PrefixWinChance + IoConsts.CsharpGaussPartialPivot, true);
            var cshGaussPartialSparse = MyMatrixIoHandler.LoadDoubleVector(IoConsts.PrefixWinChance + IoConsts.CsharpGaussPartialPivotSparse, true);
            var eigenGaussPartial = MyMatrixIoHandler.LoadDoubleVector(IoConsts.PrefixWinChance + IoConsts.EigenGaussPartialPivot, true);
            var eigenGaussPartialSparse = MyMatrixIoHandler.LoadDoubleVector(IoConsts.PrefixWinChance + IoConsts.EigenGaussPartialPivotSparse, true);

            var sb = new StringBuilder();
            sb.AppendLine(GetRow("op", "relative_error"));
            sb.AppendLine(GetRow("csh-gauss-seidel", GetRelativeError(cshGaussSeidel.Item1[0], eigenGaussPartial.Item1[0])));
            sb.AppendLine(GetRow("csh-jacobi", GetRelativeError(cshJacobi.Item1[0], eigenGaussPartial.Item1[0])));
            sb.AppendLine(GetRow("csh-gauss-partial", GetRelativeError(cshGaussPartial.Item1[0], eigenGaussPartial.Item1[0])));
            sb.AppendLine(GetRow("csh-gauss-partial-sparse", GetRelativeError(cshGaussPartialSparse.Item1[0], eigenGaussPartial.Item1[0])));
            sb.AppendLine(GetRow("eig-gauss-partial", GetRelativeError(eigenGaussPartial.Item1[0], eigenGaussPartial.Item1[0])));
            sb.AppendLine(GetRow("eig-gauss-partial-sparse", GetRelativeError(eigenGaussPartialSparse.Item1[0], eigenGaussPartial.Item1[0])));

            File.WriteAllText(IoConsts.SummaryWinChanceError, sb.ToString());
        }

        public static string GetRow(string col1, string col2)
        {
            return $"{col1};{col2}";
        }

        public static string GetRow(string col1, double col2)
        {
            return $"{col1};{col2}";
        }

        public static double GetRelativeError(double value, double referenceValue)
        {
            return Math.Abs((value - referenceValue) / referenceValue);
        }
    }
}
