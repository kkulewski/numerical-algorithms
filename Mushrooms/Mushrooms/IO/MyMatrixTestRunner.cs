using System;
using System.Collections.Generic;
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

        public void CreateGame(GameConfig config)
        {
            var dice = Dice.GetDice(config.DiceFaces);
            var game = new Game(config.BoardSize, config.Player1Position, config.Player2Position, dice);
            game.GeneratePossibleStates();
            game.GeneratePossibleTransitions();

            WriteGameMatrix(game);
            WriteProbabilityVector(game);

            // MONTE CARLO
            const int runs = 10000;
            int p1Wins = 0, p2Wins = 0;
            for (var i = 0; i < runs; i++)
            {
                var currentState = game.GameStates[game.InitialStateIndex];
                while (!currentState.Player1Won && !currentState.Player2Won)
                {
                    var toss = dice.Toss();
                    currentState = currentState.Transitions[toss];
                }

                if (currentState.Player1Won)
                {
                    p1Wins++;
                }

                if (currentState.Player2Won)
                {
                    p2Wins++;
                }
            }

            Console.WriteLine("P1-WINS: " + p1Wins);
            Console.WriteLine("P2-WINS: " + p2Wins);
        }

        public void SolveGameMatrix(int testCount, int iterations)
        {
            LoadMatrices();

            JacobiTest(testCount, iterations);
            GaussSeidelTest(testCount, iterations);
            GaussianReductionPartialPivotTest(testCount);
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

        public void GaussSeidelTest(int testCount, int iterations)
        {
            _time = new TimeSpan();
            var vector = Vector;

            for (var i = 0; i < testCount; i++)
            {
                var matrix = new MyMatrix<double>(Matrix);
                vector = Vector;

                _stopwatch.Reset();
                _stopwatch.Start();
                matrix.GaussSeidel(vector, iterations);
                _stopwatch.Stop();
                _time += _stopwatch.Elapsed;
            }

            MyMatrixIoHandler.WriteToFileWithTimespan(IO.CsharpGaussSeidel,
                MyMatrixFormatter.GetFormattedVector(vector),
                CurrentMatrixSize,
                _time.TotalMilliseconds / testCount);
        }
        
        public void GaussianReductionPartialPivotTest(int testCount)
        {
            _time = new TimeSpan();
            var vector = Vector;

            for (var i = 0; i < testCount; i++)
            {
                var matrix = new MyMatrix<double>(Matrix);
                vector = Vector;

                _stopwatch.Reset();
                _stopwatch.Start();
                matrix.GaussianReductionPartialPivot(vector);
                _stopwatch.Stop();
                _time += _stopwatch.Elapsed;
            }

            MyMatrixIoHandler.WriteToFileWithTimespan(IO.CsharpGaussPartialPivot,
                MyMatrixFormatter.GetFormattedVector(vector),
                CurrentMatrixSize,
                _time.TotalMilliseconds / testCount);
        }
    }
}
