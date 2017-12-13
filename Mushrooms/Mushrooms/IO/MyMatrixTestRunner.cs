using System;
using System.Diagnostics;

namespace Mushrooms.IO
{
    public class MyMatrixTestRunner
    {
        private readonly Stopwatch _stopwatch;
        private TimeSpan _time;

        private int CurrentMatrixSize => _matrix.Rows;

        private MyMatrix<double> _matrix;
        private double[,] Matrix => (double[,]) _matrix.Matrix.Clone();

        private double[] _vector;
        private double[] Vector => (double[]) _vector.Clone();


        public MyMatrixTestRunner()
        {
            _stopwatch = new Stopwatch();
            _time = new TimeSpan();
        }

        public void WriteGameMatrix(Game game)
        {
            var matrix = GameMatrix.GetStateMatrix(game);
            MyMatrixIoHandler.WriteMatrixToFile(matrix, IO.Matrix);
        }

        public void WriteProbabilityVector(Game game)
        {
            var vector = GameMatrix.GetProbabilityVector(game);
            MyMatrixIoHandler.WriteVectorToFile(vector, IO.Vector);
        }

        public void LoadMatrices()
        {
            _matrix = MyMatrixIoHandler.LoadDoubleMatrix(IO.Matrix, false).Item1;
            _vector = MyMatrixIoHandler.LoadDoubleVector(IO.Vector, false).Item1;
        }

        public void JacobiTest(int testCount, int iterations)
        {
            _time = new TimeSpan();
            var vector = Vector;

            for (var i = 0; i < testCount; i++)
            {
                var matrix = new MyMatrix<double>(Matrix);
                vector = Vector;

                _stopwatch.Reset();
                _stopwatch.Start();
                matrix.Jacobi(vector, iterations);
                _stopwatch.Stop();
                _time += _stopwatch.Elapsed;
            }

            MyMatrixIoHandler.WriteToFileWithTimespan(IO.CsharpJacobi,
                MyMatrixFormatter.GetFormattedVector(vector),
                CurrentMatrixSize,
                _time.TotalMilliseconds / testCount);
        }
    }
}
