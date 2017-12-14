using System.IO;
using System.Text;

namespace Mushrooms.IO
{
    public class MyMatrixTestAnalyzer
    {
        public static void TimeComparison()
        {
            var cshGaussSeidel = MyMatrixIoHandler.LoadDoubleVector(IoConsts.CsharpGaussSeidel, true);
            var cshJacobi = MyMatrixIoHandler.LoadDoubleVector(IoConsts.CsharpJacobi, true);
            var cshGaussPartial = MyMatrixIoHandler.LoadDoubleVector(IoConsts.CsharpGaussPartialPivot, true);
            //var gaussPartialSparse = MyMatrixIoHandler.LoadDoubleVector(IoConsts., true);

            var eigenGaussPartial = MyMatrixIoHandler.LoadDoubleVector(IoConsts.EigenGaussPartialPivot, true);
            //var eigenSparse = MyMatrixIoHandler.LoadDoubleVector(IoConsts.., true);

            var timeHeader = string.Format
            (
                "{0};{1}",
                "op",
                "time"
            );

            var timeCshGaussSeidel = string.Format
            (
                "{0};{1}",
                "csh-gauss-seidel",
                cshGaussSeidel.Item2
            );

            var timeCshJacobi = string.Format
            (
                "{0};{1}",
                "csh-jacobi",
                cshJacobi.Item2
            );

            var timeCshGaussPartial = string.Format
            (
                "{0};{1}",
                "csh-gauss-partial",
                cshGaussPartial.Item2
            );

            var timeEigGaussPartial = string.Format
            (
                "{0};{1}",
                "eig-gauss-partial",
                eigenGaussPartial.Item2
            );

            var sb = new StringBuilder();
            sb.AppendLine(timeHeader);
            sb.AppendLine(timeCshGaussSeidel);
            sb.AppendLine(timeCshJacobi);
            sb.AppendLine(timeCshGaussPartial);
            sb.AppendLine(timeEigGaussPartial);

            File.WriteAllText(IoConsts.SummaryTime, sb.ToString());
        }

        public static void NormComparison()
        {
            var cshGaussSeidel = MyMatrixIoHandler.LoadDoubleVector(IoConsts.CsharpGaussSeidel, true);
            var cshJacobi = MyMatrixIoHandler.LoadDoubleVector(IoConsts.CsharpJacobi, true);
            var cshGaussPartial = MyMatrixIoHandler.LoadDoubleVector(IoConsts.CsharpGaussPartialPivot, true);
            //var gaussPartialSparse = MyMatrixIoHandler.LoadDoubleVector(IoConsts., true);

            var eigenGaussPartial = MyMatrixIoHandler.LoadDoubleVector(IoConsts.EigenGaussPartialPivot, true);
            //var eigenSparse = MyMatrixIoHandler.LoadDoubleVector(IoConsts.., true);

            var normHeader = string.Format
            (
                "{0};{1}",
                "op",
                "norm"
            );

            var normCshGaussSeidel = string.Format
            (
                "{0};{1}",
                "csh-gauss-seidel",
                MyMatrix<double>.VectorNorm(cshGaussSeidel.Item1, eigenGaussPartial.Item1)
            );

            var normCshJacobi = string.Format
            (
                "{0};{1}",
                "csh-jacobi",
                MyMatrix<double>.VectorNorm(cshJacobi.Item1, eigenGaussPartial.Item1)
            );

            var normCshGaussPartial = string.Format
            (
                "{0};{1}",
                "csh-gauss-partial",
                MyMatrix<double>.VectorNorm(cshGaussPartial.Item1, eigenGaussPartial.Item1)
            );

            var normEigGaussPartial = string.Format
            (
                "{0};{1}",
                "eig-gauss-partial",
                MyMatrix<double>.VectorNorm(eigenGaussPartial.Item1, eigenGaussPartial.Item1)
            );

            var sb = new StringBuilder();
            sb.AppendLine(normHeader);
            sb.AppendLine(normCshGaussSeidel);
            sb.AppendLine(normCshJacobi);
            sb.AppendLine(normCshGaussPartial);
            sb.AppendLine(normEigGaussPartial);

            File.WriteAllText(IoConsts.SummaryNorm, sb.ToString());
        }
    }
}
