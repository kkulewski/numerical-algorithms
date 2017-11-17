using System;
using System.Diagnostics;

namespace Matrix
{
    public class MyMatrixTestRunner
    {
        public const string PrefixFraction = "fraction_";
        public const string PrefixDouble = "double_";
        public const string PrefixFloat = "float_";

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

        private readonly int _matrixSize;
        private readonly MyMatrixIoHandler _handler;
        private readonly Stopwatch _stopwatch;
        private readonly int _testCount;
        private TimeSpan _time;


        private MyMatrix<Fraction> _sfrA;
        private Fraction[,] SfrA => (Fraction[,]) _sfrA.Matrix.Clone();

        private MyMatrix<Fraction> _sfrB;
        private Fraction[,] SfrB => (Fraction[,]) _sfrB.Matrix.Clone();

        private MyMatrix<Fraction> _sfrC;
        private Fraction[,] SfrC => (Fraction[,]) _sfrC.Matrix.Clone();

        private Fraction[] _sfrX;
        private Fraction[] SfrX => (Fraction[]) _sfrX.Clone();

        private MyMatrix<float> _sfA;
        private float[,] SfA => (float[,]) _sfA.Matrix.Clone();

        private MyMatrix<float> _sfB;
        private float[,] SfB => (float[,]) _sfB.Matrix.Clone();

        private MyMatrix<float> _sfC;
        private float[,] SfC => (float[,]) _sfC.Matrix.Clone();

        private float[] _sfX;
        private float[] SfX => (float[]) _sfX.Clone();

        private MyMatrix<double> _sdA;
        private double[,] SdA => (double[,]) _sdA.Matrix.Clone();

        private MyMatrix<double> _sdB;
        private double[,] SdB => (double[,])_sdB.Matrix.Clone();

        private MyMatrix<double> _sdC;
        private double[,] SdC => (double[,])_sdC.Matrix.Clone();

        private double[] _sdX;
        private double[] SdX => (double[]) _sdX.Clone();


        public MyMatrixTestRunner(int matrixSize, int testCount)
        {
            _matrixSize = matrixSize;
            _handler = new MyMatrixIoHandler();
            _stopwatch = new Stopwatch();
            _testCount = testCount;
            _time = new TimeSpan();
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

        public void LoadMatrices()
        {
            // fraction
            _sfrA = _handler.LoadFractionMatrix(PrefixFraction + FileA);
            _sfrB = _handler.LoadFractionMatrix(PrefixFraction + FileB);
            _sfrC = _handler.LoadFractionMatrix(PrefixFraction + FileC);
            _sfrX = _handler.LoadFractionVector(PrefixFraction + FileX);

            // float
            _sfA = _handler.LoadFloatMatrix(PrefixFloat + FileA);
            _sfB = _handler.LoadFloatMatrix(PrefixFloat + FileB);
            _sfC = _handler.LoadFloatMatrix(PrefixFloat + FileC);
            _sfX = _handler.LoadFloatVector(PrefixFloat + FileX);

            // double
            _sdA = _handler.LoadDoubleMatrix(PrefixDouble + FileA);
            _sdB = _handler.LoadDoubleMatrix(PrefixDouble + FileB);
            _sdC = _handler.LoadDoubleMatrix(PrefixDouble + FileC);
            _sdX = _handler.LoadDoubleVector(PrefixDouble + FileX);
        }

        public void MatrixGaussianReductionNoPivotTest()
        {
            // ---------------------------------------------------------
            // fraction
            // ---------------------------------------------------------
            _time = new TimeSpan();

            var frA = new MyMatrix<Fraction>(SfrA);
            var frX = (Fraction[]) SfrX.Clone();

            for (var i = 0; i < _testCount; i++)
            {
                frA = new MyMatrix<Fraction>(SfrA);
                frX = (Fraction[]) SfrX.Clone();

                _stopwatch.Reset();
                _stopwatch.Start();
                frA.GaussianReductionNoPivot(frX);
                _stopwatch.Stop();
                _time += _stopwatch.Elapsed;
            }
            
            _handler.WriteToFileWithTimespan(
                PrefixFraction + ResultNoPivot,
                MyMatrixFormatter.GetFormattedVector(frX),
                _matrixSize,
                _time.TotalMilliseconds / _testCount);


            // ---------------------------------------------------------
            // float
            // ---------------------------------------------------------
            _time = new TimeSpan();

            var fA = new MyMatrix<float>(SfA);
            var fX = (float[]) SfX.Clone();

            for (var i = 0; i < _testCount; i++)
            {
                fA = new MyMatrix<float>(SfA);
                fX = (float[]) SfX.Clone();

                _stopwatch.Reset();
                _stopwatch.Start();
                fA.GaussianReductionNoPivot(fX);
                _stopwatch.Stop();
                _time += _stopwatch.Elapsed;
            }

            _handler.WriteToFileWithTimespan(
                PrefixFloat + ResultNoPivot,
                MyMatrixFormatter.GetFormattedVector(fX),
                _matrixSize,
                _time.TotalMilliseconds / _testCount);


            // ---------------------------------------------------------
            // double
            // ---------------------------------------------------------
            _time = new TimeSpan();

            var dA = new MyMatrix<double>(SdA);
            var dX = (double[]) SdX.Clone();

            for (var i = 0; i < _testCount; i++)
            {
                dA = new MyMatrix<double>(SdA);
                dX = (double[]) SdX.Clone();

                _stopwatch.Reset();
                _stopwatch.Start();
                dA.GaussianReductionNoPivot(dX);
                _stopwatch.Stop();
                _time += _stopwatch.Elapsed;
            }

            _handler.WriteToFileWithTimespan(
                PrefixDouble + ResultNoPivot,
                MyMatrixFormatter.GetFormattedVector(dX),
                _matrixSize,
                _time.TotalMilliseconds / _testCount);
        }

        public void MatrixGaussianReductionPartialPivotTest()
        {
            // ---------------------------------------------------------
            // fraction
            // ---------------------------------------------------------
            _time = new TimeSpan();

            var frA = new MyMatrix<Fraction>(SfrA);
            var frX = (Fraction[])SfrX.Clone();

            for (var i = 0; i < _testCount; i++)
            {
                frA = new MyMatrix<Fraction>(SfrA);
                frX = (Fraction[])SfrX.Clone();

                _stopwatch.Reset();
                _stopwatch.Start();
                frA.GaussianReductionPartialPivot(frX);
                _stopwatch.Stop();
                _time += _stopwatch.Elapsed;
            }

            _handler.WriteToFileWithTimespan(
                PrefixFraction + ResultPartialPivot,
                MyMatrixFormatter.GetFormattedVector(frX),
                _matrixSize,
                _time.TotalMilliseconds / _testCount);


            // ---------------------------------------------------------
            // float
            // ---------------------------------------------------------
            _time = new TimeSpan();

            var fA = new MyMatrix<float>(SfA);
            var fX = (float[])SfX.Clone();

            for (var i = 0; i < _testCount; i++)
            {
                fA = new MyMatrix<float>(SfA);
                fX = (float[])SfX.Clone();

                _stopwatch.Reset();
                _stopwatch.Start();
                fA.GaussianReductionPartialPivot(fX);
                _stopwatch.Stop();
                _time += _stopwatch.Elapsed;
            }

            _handler.WriteToFileWithTimespan(
                PrefixFloat + ResultPartialPivot,
                MyMatrixFormatter.GetFormattedVector(fX),
                _matrixSize,
                _time.TotalMilliseconds / _testCount);


            // ---------------------------------------------------------
            // double
            // ---------------------------------------------------------
            _time = new TimeSpan();

            var dA = new MyMatrix<double>(SdA);
            var dX = (double[])SdX.Clone();

            for (var i = 0; i < _testCount; i++)
            {
                dA = new MyMatrix<double>(SdA);
                dX = (double[])SdX.Clone();

                _stopwatch.Reset();
                _stopwatch.Start();
                dA.GaussianReductionPartialPivot(dX);
                _stopwatch.Stop();
                _time += _stopwatch.Elapsed;
            }

            _handler.WriteToFileWithTimespan(
                PrefixDouble + ResultPartialPivot,
                MyMatrixFormatter.GetFormattedVector(dX),
                _matrixSize,
                _time.TotalMilliseconds / _testCount);
        }

        public void MatrixGaussianReductionFullPivotTest()
        {
            // ---------------------------------------------------------
            // fraction
            // ---------------------------------------------------------
            _time = new TimeSpan();

            var frA = new MyMatrix<Fraction>(SfrA);
            var frX = (Fraction[])SfrX.Clone();

            for (var i = 0; i < _testCount; i++)
            {
                frA = new MyMatrix<Fraction>(SfrA);
                frX = (Fraction[])SfrX.Clone();

                _stopwatch.Reset();
                _stopwatch.Start();
                frA.GaussianReductionFullPivot(frX);
                _stopwatch.Stop();
                _time += _stopwatch.Elapsed;
            }

            _handler.WriteToFileWithTimespan(
                PrefixFraction + ResultFullPivot,
                MyMatrixFormatter.GetFormattedVector(frX),
                _matrixSize,
                _time.TotalMilliseconds / _testCount);


            // ---------------------------------------------------------
            // float
            // ---------------------------------------------------------
            _time = new TimeSpan();

            var fA = new MyMatrix<float>(SfA);
            var fX = (float[])SfX.Clone();

            for (var i = 0; i < _testCount; i++)
            {
                fA = new MyMatrix<float>(SfA);
                fX = (float[])SfX.Clone();

                _stopwatch.Reset();
                _stopwatch.Start();
                fA.GaussianReductionFullPivot(fX);
                _stopwatch.Stop();
                _time += _stopwatch.Elapsed;
            }

            _handler.WriteToFileWithTimespan(
                PrefixFloat + ResultFullPivot,
                MyMatrixFormatter.GetFormattedVector(fX),
                _matrixSize,
                _time.TotalMilliseconds / _testCount);


            // ---------------------------------------------------------
            // double
            // ---------------------------------------------------------
            _time = new TimeSpan();

            var dA = new MyMatrix<double>(SdA);
            var dX = (double[])SdX.Clone();

            for (var i = 0; i < _testCount; i++)
            {
                dA = new MyMatrix<double>(SdA);
                dX = (double[])SdX.Clone();

                _stopwatch.Reset();
                _stopwatch.Start();
                dA.GaussianReductionFullPivot(dX);
                _stopwatch.Stop();
                _time += _stopwatch.Elapsed;
            }

            _handler.WriteToFileWithTimespan(
                PrefixDouble + ResultFullPivot,
                MyMatrixFormatter.GetFormattedVector(dX),
                _matrixSize,
                _time.TotalMilliseconds / _testCount);
        }

        public void MatrixMulVectorTest()
        {
            // ---------------------------------------------------------
            // fraction
            // ---------------------------------------------------------
            _time = new TimeSpan();

            var frA = new MyMatrix<Fraction>(SfrA);
            var frX = (Fraction[])SfrX.Clone();
            var frResult = new Fraction[0];

            for (var i = 0; i < _testCount; i++)
            {
                frA = new MyMatrix<Fraction>(SfrA);
                frX = (Fraction[])SfrX.Clone();

                _stopwatch.Reset();
                _stopwatch.Start();
                frResult = frA * frX;
                _stopwatch.Stop();
                _time += _stopwatch.Elapsed;
            }

            _handler.WriteToFileWithTimespan(
                PrefixFraction + ResultAx,
                MyMatrixFormatter.GetFormattedVector(frResult), 
                _matrixSize, 
                _time.TotalMilliseconds / _testCount);


            // ---------------------------------------------------------
            // float
            // ---------------------------------------------------------
            _time = new TimeSpan();

            var fA = new MyMatrix<float>(SfA);
            var fX = (float[])SfX.Clone();
            var fResult = new float[0];

            for (var i = 0; i < _testCount; i++)
            {
                fA = new MyMatrix<float>(SfA);
                fX = (float[])SfX.Clone();

                _stopwatch.Reset();
                _stopwatch.Start();
                fResult = fA * fX;
                _stopwatch.Stop();
                _time += _stopwatch.Elapsed;
            }

            _handler.WriteToFileWithTimespan(
                PrefixFloat + ResultAx,
                MyMatrixFormatter.GetFormattedVector(fResult),
                _matrixSize,
                _time.TotalMilliseconds / _testCount);


            // ---------------------------------------------------------
            // double
            // ---------------------------------------------------------
            _time = new TimeSpan();

            var dA = new MyMatrix<double>(SdA);
            var dX = (double[])SdX.Clone();
            var dResult = new double[0];

            for (var i = 0; i < _testCount; i++)
            {
                dA = new MyMatrix<double>(SdA);
                dX = (double[]) SdX.Clone();

                _stopwatch.Reset();
                _stopwatch.Start();
                dResult = dA * dX;
                _stopwatch.Stop();
                _time += _stopwatch.Elapsed;
            }

            _handler.WriteToFileWithTimespan(
                PrefixDouble + ResultAx,
                MyMatrixFormatter.GetFormattedVector(dResult),
                _matrixSize,
                _time.TotalMilliseconds / _testCount);
        }

        public void MatrixAddMatrixMulVectorTest()
        {
            // ---------------------------------------------------------
            // fraction
            // ---------------------------------------------------------
            _time = new TimeSpan();

            var frA = new MyMatrix<Fraction>(SfrA);
            var frB = new MyMatrix<Fraction>(SfrB);
            var frC = new MyMatrix<Fraction>(SfrC);
            var frX = (Fraction[])SfrX.Clone();
            var frResult = new Fraction[0];

            for (var i = 0; i < _testCount; i++)
            {
                frA = new MyMatrix<Fraction>(SfrA);
                frB = new MyMatrix<Fraction>(SfrB);
                frC = new MyMatrix<Fraction>(SfrC);
                frX = (Fraction[]) SfrX.Clone();

                _stopwatch.Reset();
                _stopwatch.Start();
                frResult = (frA + frB + frC) * frX;
                _stopwatch.Stop();
                _time += _stopwatch.Elapsed;
            }

            _handler.WriteToFileWithTimespan(
                PrefixFraction + ResultAbcx,
                MyMatrixFormatter.GetFormattedVector(frResult),
                _matrixSize,
                _time.TotalMilliseconds / _testCount);


            // ---------------------------------------------------------
            // float
            // ---------------------------------------------------------
            _time = new TimeSpan();

            var fA = new MyMatrix<float>(SfA);
            var fB = new MyMatrix<float>(SfB);
            var fC = new MyMatrix<float>(SfC);
            var fX = (float[]) SfX.Clone();
            var fResult = new float[0];

            for (var i = 0; i < _testCount; i++)
            {
                fA = new MyMatrix<float>(SfA);
                fB = new MyMatrix<float>(SfB);
                fC = new MyMatrix<float>(SfC);
                fX = (float[]) SfX.Clone();

                _stopwatch.Reset();
                _stopwatch.Start();
                fResult = (fA + fB + fC) * fX;
                _stopwatch.Stop();
                _time += _stopwatch.Elapsed;
            }

            _handler.WriteToFileWithTimespan(
                PrefixFloat + ResultAbcx,
                MyMatrixFormatter.GetFormattedVector(fResult),
                _matrixSize,
                _time.TotalMilliseconds / _testCount);


            // ---------------------------------------------------------
            // double
            // ---------------------------------------------------------
            _time = new TimeSpan();

            var dA = new MyMatrix<double>(SdA);
            var dB = new MyMatrix<double>(SdB);
            var dC = new MyMatrix<double>(SdC);
            var dX = (double[]) SdX.Clone();
            var dResult = new double[0];

            for (var i = 0; i < _testCount; i++)
            {
                dA = new MyMatrix<double>(SdA);
                dB = new MyMatrix<double>(SdB);
                dC = new MyMatrix<double>(SdC);
                dX = (double[]) SdX.Clone();

                _stopwatch.Reset();
                _stopwatch.Start();
                dResult = (dA + dB + dC) * dX;
                _stopwatch.Stop();
                _time += _stopwatch.Elapsed;
            }

            _handler.WriteToFileWithTimespan(
                PrefixDouble + ResultAbcx,
                MyMatrixFormatter.GetFormattedVector(dResult),
                _matrixSize,
                _time.TotalMilliseconds / _testCount);
        }

        public void MatrixMulMatrixTest()
        {
            // ---------------------------------------------------------
            // fraction
            // ---------------------------------------------------------
            _time = new TimeSpan();

            var frA = new MyMatrix<Fraction>(SfrA);
            var frB = new MyMatrix<Fraction>(SfrB);
            var frC = new MyMatrix<Fraction>(SfrC);
            var frResult = new MyMatrix<Fraction>(1, 1);

            for (var i = 0; i < _testCount; i++)
            {
                frA = new MyMatrix<Fraction>(SfrA);
                frB = new MyMatrix<Fraction>(SfrB);
                frC = new MyMatrix<Fraction>(SfrC);

                _stopwatch.Reset();
                _stopwatch.Start();
                frResult = frA * (frB * frC);
                _stopwatch.Stop();
                _time += _stopwatch.Elapsed;
            }

            _handler.WriteToFileWithTimespan(
                PrefixFraction + ResultAbc,
                MyMatrixFormatter.GetFormattedMatrix(frResult),
                _matrixSize,
                _time.TotalMilliseconds / _testCount);


            // ---------------------------------------------------------
            // float
            // ---------------------------------------------------------
            _time = new TimeSpan();

            var fA = new MyMatrix<float>(SfA);
            var fB = new MyMatrix<float>(SfB);
            var fC = new MyMatrix<float>(SfC);
            var fResult = new MyMatrix<float>(1, 1);

            for (var i = 0; i < _testCount; i++)
            {
                fA = new MyMatrix<float>(SfA);
                fB = new MyMatrix<float>(SfB);
                fC = new MyMatrix<float>(SfC);

                _stopwatch.Reset();
                _stopwatch.Start();
                fResult = fA * (fB * fC);
                _stopwatch.Stop();
                _time += _stopwatch.Elapsed;
            }

            _handler.WriteToFileWithTimespan(
                PrefixFloat + ResultAbc,
                MyMatrixFormatter.GetFormattedMatrix(fResult),
                _matrixSize,
                _time.TotalMilliseconds / _testCount);

            // ---------------------------------------------------------
            // double
            // ---------------------------------------------------------
            _time = new TimeSpan();

            var dA = new MyMatrix<double>(SdA);
            var dB = new MyMatrix<double>(SdB);
            var dC = new MyMatrix<double>(SdC);
            var dResult = new MyMatrix<double>(1, 1);

            for (var i = 0; i < _testCount; i++)
            {
                dA = new MyMatrix<double>(SdA);
                dB = new MyMatrix<double>(SdB);
                dC = new MyMatrix<double>(SdC);

                _stopwatch.Reset();
                _stopwatch.Start();
                dResult = dA * (dB * dC);
                _stopwatch.Stop();
                _time += _stopwatch.Elapsed;
            }

            _handler.WriteToFileWithTimespan(
                PrefixDouble + ResultAbc,
                MyMatrixFormatter.GetFormattedMatrix(dResult),
                _matrixSize,
                _time.TotalMilliseconds / _testCount);
        }
    }
}
