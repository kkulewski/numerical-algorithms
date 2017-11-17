using System.Diagnostics;

namespace Matrix
{
    public class MyMatrixTester
    {
        public const string FileA = "a.txt";
        public const string FileB = "b.txt";
        public const string FileC = "c.txt";
        public const string FileX = "x.txt";
        public const string ResultAx = "result_ax.txt";
        public const string ResultAbcx = "result_abcx.txt";
        public const string ResultAbc = "result_abc.txt";
        public const string ResultNoPivot = "result_gauss_no_pivot.txt";
        public const string ResultPartialPivot = "result_gauss_partial_pivot.txt";
        public const string ResultFullPivot = "result_gauss_full_pivot.txt";
        public const string PrefixFraction = "fraction_";
        public const string PrefixDouble = "double_";
        public const string PrefixFloat = "float_";

        private readonly int _matrixSize;
        private readonly MyMatrixIoHandler _handler;
        private readonly Stopwatch _stopwatch;

        public MyMatrixTester(int matrixSize)
        {
            _matrixSize = matrixSize;
            _handler = new MyMatrixIoHandler();
            _stopwatch = new Stopwatch();
        }

        public void WriteMatrices()
        {
            // prepare fraction A, B, C matrix + X vector & save
            var frA = _handler.GenerateRandomFractionMatrix(_matrixSize);
            var frB = _handler.GenerateRandomFractionMatrix(_matrixSize);
            var frC = _handler.GenerateRandomFractionMatrix(_matrixSize);
            var frX = _handler.GenerateRandomFractionVector(_matrixSize);
            _handler.WriteFractionMatrixToFile(frA, PrefixFraction + FileA);
            _handler.WriteFractionMatrixToFile(frB, PrefixFraction + FileB);
            _handler.WriteFractionMatrixToFile(frC, PrefixFraction + FileC);
            _handler.WriteFractionVectorToFile(frX, PrefixFraction + FileX);

            // prepare float A, B, C matrix + X vector & save
            var fA = _handler.FloatMatrixFromFractionMatrix(frA);
            var fB = _handler.FloatMatrixFromFractionMatrix(frB);
            var fC = _handler.FloatMatrixFromFractionMatrix(frC);
            var fX = _handler.FloatVectorFromFractionVector(frX);
            _handler.WriteMatrixToFile(fA, PrefixFloat + FileA);
            _handler.WriteMatrixToFile(fB, PrefixFloat + FileC);
            _handler.WriteMatrixToFile(fC, PrefixFloat + FileB);
            _handler.WriteVectorToFile(fX, PrefixFloat + FileX);

            // prepare double A, B, C matrix + X vector & save
            var dA = _handler.DoubleMatrixFromFractionMatrix(frA);
            var dB = _handler.DoubleMatrixFromFractionMatrix(frB);
            var dC = _handler.DoubleMatrixFromFractionMatrix(frC);
            var dX = _handler.DoubleVectorFromFractionVector(frX);
            _handler.WriteMatrixToFile(dA, PrefixDouble + FileA);
            _handler.WriteMatrixToFile(dB, PrefixDouble + FileB);
            _handler.WriteMatrixToFile(dC, PrefixDouble + FileC);
            _handler.WriteVectorToFile(dX, PrefixDouble + FileX);
        }

        public void MatrixGaussianReductionNoPivotTest()
        {
            // fraction
            var frA = _handler.LoadFractionMatrix(PrefixFraction + FileA);
            var frX = _handler.LoadFractionVector(PrefixFraction + FileX);

            _stopwatch.Reset();
            _stopwatch.Start();
            frA.GaussianReductionNoPivot(frX);
            _stopwatch.Stop();
            var time = _stopwatch.Elapsed;
            _handler.WriteToFileWithTimespan(
                PrefixFraction + ResultNoPivot,
                MyMatrixFormatter.GetFormattedVector(frX),
                _matrixSize,
                time);

            // float
            var fA = _handler.LoadFloatMatrix(PrefixFloat + FileA);
            var fX = _handler.LoadFloatVector(PrefixFloat + FileX);

            _stopwatch.Reset();
            _stopwatch.Start();
            fA.GaussianReductionNoPivot(fX);
            _stopwatch.Stop();
            time = _stopwatch.Elapsed;
            _handler.WriteToFileWithTimespan(
                PrefixFloat + ResultNoPivot,
                MyMatrixFormatter.GetFormattedVector(fX),
                _matrixSize,
                time);


            // double
            var dA = _handler.LoadDoubleMatrix(PrefixDouble + FileA);
            var dX = _handler.LoadDoubleVector(PrefixDouble + FileX);

            _stopwatch.Reset();
            _stopwatch.Start();
            dA.GaussianReductionNoPivot(dX);
            _stopwatch.Stop();
            time = _stopwatch.Elapsed;
            _handler.WriteToFileWithTimespan(
                PrefixDouble + ResultNoPivot,
                MyMatrixFormatter.GetFormattedVector(dX),
                _matrixSize,
                time);
        }

        public void MatrixGaussianReductionPartialPivotTest()
        {
            // fraction
            var frA = _handler.LoadFractionMatrix(PrefixFraction + FileA);
            var frX = _handler.LoadFractionVector(PrefixFraction + FileX);

            _stopwatch.Reset();
            _stopwatch.Start();
            frA.GaussianReductionPartialPivot(frX);
            _stopwatch.Stop();
            var time = _stopwatch.Elapsed;
            _handler.WriteToFileWithTimespan(
                PrefixFraction + ResultPartialPivot,
                MyMatrixFormatter.GetFormattedVector(frX),
                _matrixSize,
                time);

            // float
            var fA = _handler.LoadFloatMatrix(PrefixFloat + FileA);
            var fX = _handler.LoadFloatVector(PrefixFloat + FileX);

            _stopwatch.Reset();
            _stopwatch.Start();
            fA.GaussianReductionPartialPivot(fX);
            _stopwatch.Stop();
            time = _stopwatch.Elapsed;
            _handler.WriteToFileWithTimespan(
                PrefixFloat + ResultPartialPivot,
                MyMatrixFormatter.GetFormattedVector(fX),
                _matrixSize,
                time);


            // double
            var dA = _handler.LoadDoubleMatrix(PrefixDouble + FileA);
            var dX = _handler.LoadDoubleVector(PrefixDouble + FileX);

            _stopwatch.Reset();
            _stopwatch.Start();
            dA.GaussianReductionPartialPivot(dX);
            _stopwatch.Stop();
            time = _stopwatch.Elapsed;
            _handler.WriteToFileWithTimespan(
                PrefixDouble + ResultPartialPivot,
                MyMatrixFormatter.GetFormattedVector(dX),
                _matrixSize,
                time);
        }

        public void MatrixGaussianReductionFullPivotTest()
        {
            // fraction
            var frA = _handler.LoadFractionMatrix(PrefixFraction + FileA);
            var frX = _handler.LoadFractionVector(PrefixFraction + FileX);

            _stopwatch.Reset();
            _stopwatch.Start();
            frA.GaussianReductionFullPivot(frX);
            _stopwatch.Stop();
            var time = _stopwatch.Elapsed;
            _handler.WriteToFileWithTimespan(
                PrefixFraction + ResultFullPivot,
                MyMatrixFormatter.GetFormattedVector(frX),
                _matrixSize,
                time);

            // float
            var fA = _handler.LoadFloatMatrix(PrefixFloat + FileA);
            var fX = _handler.LoadFloatVector(PrefixFloat + FileX);

            _stopwatch.Reset();
            _stopwatch.Start();
            fA.GaussianReductionFullPivot(fX);
            _stopwatch.Stop();
            time = _stopwatch.Elapsed;
            _handler.WriteToFileWithTimespan(
                PrefixFloat + ResultFullPivot,
                MyMatrixFormatter.GetFormattedVector(fX),
                _matrixSize,
                time);


            // double
            var dA = _handler.LoadDoubleMatrix(PrefixDouble + FileA);
            var dX = _handler.LoadDoubleVector(PrefixDouble + FileX);

            _stopwatch.Reset();
            _stopwatch.Start();
            dA.GaussianReductionFullPivot(dX);
            _stopwatch.Stop();
            time = _stopwatch.Elapsed;
            _handler.WriteToFileWithTimespan(
                PrefixDouble + ResultFullPivot,
                MyMatrixFormatter.GetFormattedVector(dX),
                _matrixSize,
                time);
        }

        public void MatrixMulVectorTest()
        {
            // fraction A * X
            var frA = _handler.LoadFractionMatrix(PrefixFraction + FileA);
            var frX = _handler.LoadFractionVector(PrefixFraction + FileX);

            _stopwatch.Reset();
            _stopwatch.Start();
            var frResult = frA * frX;
            _stopwatch.Stop();
            var time = _stopwatch.Elapsed;
            _handler.WriteToFileWithTimespan(
                PrefixFraction + ResultAx,
                MyMatrixFormatter.GetFormattedVector(frResult), 
                _matrixSize, 
                time);

            // float A * X
            var fA = _handler.LoadFloatMatrix(PrefixFloat + FileA);
            var fX = _handler.LoadFloatVector(PrefixFloat + FileX);

            _stopwatch.Reset();
            _stopwatch.Start();
            var fResult = fA * fX;
            _stopwatch.Stop();
            time = _stopwatch.Elapsed;
            _handler.WriteToFileWithTimespan(
                PrefixFloat + ResultAx,
                MyMatrixFormatter.GetFormattedVector(fResult),
                _matrixSize,
                time);


            // double A * X
            var dA = _handler.LoadDoubleMatrix(PrefixDouble + FileA);
            var dX = _handler.LoadDoubleVector(PrefixDouble + FileX);

            _stopwatch.Reset();
            _stopwatch.Start();
            var dResult = dA * dX;
            _stopwatch.Stop();
            time = _stopwatch.Elapsed;
            _handler.WriteToFileWithTimespan(
                PrefixDouble + ResultAx,
                MyMatrixFormatter.GetFormattedVector(dResult),
                _matrixSize,
                time);
        }

        public void MatrixAddMatrixMulVectorTest()
        {
            // fraction
            var frA = _handler.LoadFractionMatrix(PrefixFraction + FileA);
            var frB = _handler.LoadFractionMatrix(PrefixFraction + FileB);
            var frC = _handler.LoadFractionMatrix(PrefixFraction + FileC);
            var frX = _handler.LoadFractionVector(PrefixFraction + FileX);

            _stopwatch.Reset();
            _stopwatch.Start();
            var frResult = (frA + frB + frC) * frX;
            _stopwatch.Stop();
            var time = _stopwatch.Elapsed;
            _handler.WriteToFileWithTimespan(
                PrefixFraction + ResultAbcx,
                MyMatrixFormatter.GetFormattedVector(frResult),
                _matrixSize,
                time);

            // float
            var fA = _handler.LoadFloatMatrix(PrefixFloat + FileA);
            var fB = _handler.LoadFloatMatrix(PrefixFloat + FileB);
            var fC = _handler.LoadFloatMatrix(PrefixFloat + FileC);
            var fX = _handler.LoadFloatVector(PrefixFloat + FileX);

            _stopwatch.Reset();
            _stopwatch.Start();
            var fResult = (fA + fB + fC) * fX;
            _stopwatch.Stop();
            time = _stopwatch.Elapsed;
            _handler.WriteToFileWithTimespan(
                PrefixFloat + ResultAbcx,
                MyMatrixFormatter.GetFormattedVector(fResult),
                _matrixSize,
                time);


            // double
            var dA = _handler.LoadDoubleMatrix(PrefixDouble + FileA);
            var dB = _handler.LoadDoubleMatrix(PrefixDouble + FileB);
            var dC = _handler.LoadDoubleMatrix(PrefixDouble + FileC);
            var dX = _handler.LoadDoubleVector(PrefixDouble + FileX);

            _stopwatch.Reset();
            _stopwatch.Start();
            var dResult = (dA + dB + dC) * dX;
            _stopwatch.Stop();
            time = _stopwatch.Elapsed;
            _handler.WriteToFileWithTimespan(
                PrefixDouble + ResultAbcx,
                MyMatrixFormatter.GetFormattedVector(dResult),
                _matrixSize,
                time);
        }

        public void MatrixMulMatrixTest()
        {
            // fraction
            var frA = _handler.LoadFractionMatrix(PrefixFraction + FileA);
            var frB = _handler.LoadFractionMatrix(PrefixFraction + FileB);
            var frC = _handler.LoadFractionMatrix(PrefixFraction + FileC);

            _stopwatch.Reset();
            _stopwatch.Start();
            var frResult = frA * (frB * frC);
            _stopwatch.Stop();
            var time = _stopwatch.Elapsed;
            _handler.WriteToFileWithTimespan(
                PrefixFraction + ResultAbc,
                MyMatrixFormatter.GetFormattedMatrix(frResult),
                _matrixSize,
                time);

            // float
            var fA = _handler.LoadFloatMatrix(PrefixFloat + FileA);
            var fB = _handler.LoadFloatMatrix(PrefixFloat + FileB);
            var fC = _handler.LoadFloatMatrix(PrefixFloat + FileC);

            _stopwatch.Reset();
            _stopwatch.Start();
            var fResult = fA * (fB * fC);
            _stopwatch.Stop();
            time = _stopwatch.Elapsed;
            _handler.WriteToFileWithTimespan(
                PrefixFloat + ResultAbc,
                MyMatrixFormatter.GetFormattedMatrix(fResult),
                _matrixSize,
                time);


            // double
            var dA = _handler.LoadDoubleMatrix(PrefixDouble + FileA);
            var dB = _handler.LoadDoubleMatrix(PrefixDouble + FileB);
            var dC = _handler.LoadDoubleMatrix(PrefixDouble + FileC);

            _stopwatch.Reset();
            _stopwatch.Start();
            var dResult = dA * (dB + dC);
            _stopwatch.Stop();
            time = _stopwatch.Elapsed;
            _handler.WriteToFileWithTimespan(
                PrefixDouble + ResultAbc,
                MyMatrixFormatter.GetFormattedMatrix(dResult),
                _matrixSize,
                time);
        }
    }
}
