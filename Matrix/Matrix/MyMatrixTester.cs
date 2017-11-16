using System;
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
                MyMatrixFormatter.GetFormattedMatrix(frResult), 
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
                MyMatrixFormatter.GetFormattedMatrix(fResult),
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
                MyMatrixFormatter.GetFormattedMatrix(dResult),
                _matrixSize,
                time);
        }
    }
}
