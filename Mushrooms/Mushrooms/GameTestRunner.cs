using System;
using System.Diagnostics;
using System.IO;
using Mushrooms.IO;

namespace Mushrooms
{
    public class GameTestRunner
    {
        private readonly Stopwatch _stopwatch;
        private TimeSpan _time;

        private int CurrentMatrixSize => _matrix.Rows;

        private MyMatrix<double> _matrix;
        private double[,] Matrix => (double[,]) _matrix.Matrix.Clone();

        private double[] _vector;
        private double[] Vector => (double[]) _vector.Clone();

        private Game _game;
        private Dice _dice;


        public GameTestRunner()
        {
            _stopwatch = new Stopwatch();
            _time = new TimeSpan();
        }

        public void CreateGame(GameConfig config)
        {
            _dice = Dice.GetDice(config.DiceFaces);
            _game = new Game(config.BoardSize, config.Player1Position, config.Player2Position, _dice);
            _game.GeneratePossibleStates();
            _game.GeneratePossibleTransitions();

            WriteGameMatrix(_game);
            WriteProbabilityVector(_game);
        }

        public void RunMonteCarlo(int iterations)
        {
            var p1Wins = 0;
            for (var i = 0; i < iterations; i++)
            {
                var currentState = _game.GameStates[_game.InitialStateIndex];
                while (!currentState.Player1Won && !currentState.Player2Won)
                {
                    var toss = _dice.Toss();
                    currentState = currentState.Transitions[toss];
                }

                if (currentState.Player1Won)
                {
                    p1Wins++;
                }
            }

            var winChance = (double) p1Wins / iterations;
            var output = string.Format("{0}", winChance);
            File.WriteAllText(IoConsts.MonteCarlo, output);
        }

        public void SolveGame(int testCount, int iterations)
        {
            LoadMatrices();

            SolveJacobi(testCount, iterations);
            SolveGaussSeidel(testCount, iterations);
            SolveGaussPartialPivot(testCount);
        }

        private void WriteGameMatrix(Game game)
        {
            var matrix = GameMatrix.GetStateMatrix(game);
            MyMatrixIoHandler.WriteMatrixToFile(matrix, IoConsts.Matrix);
        }

        private void WriteProbabilityVector(Game game)
        {
            var vector = GameMatrix.GetProbabilityVector(game);
            MyMatrixIoHandler.WriteVectorToFile(vector, IoConsts.Vector);
        }

        private void LoadMatrices()
        {
            _matrix = MyMatrixIoHandler.LoadDoubleMatrix(IoConsts.Matrix, false).Item1;
            _vector = MyMatrixIoHandler.LoadDoubleVector(IoConsts.Vector, false).Item1;
        }

        private void SolveJacobi(int testCount, int iterations)
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

            MyMatrixIoHandler.WriteToFileWithTimespan(IoConsts.CsharpJacobi,
                MyMatrixFormatter.GetFormattedVector(vector),
                CurrentMatrixSize,
                _time.TotalMilliseconds / testCount);
        }

        private void SolveGaussSeidel(int testCount, int iterations)
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

            MyMatrixIoHandler.WriteToFileWithTimespan(IoConsts.CsharpGaussSeidel,
                MyMatrixFormatter.GetFormattedVector(vector),
                CurrentMatrixSize,
                _time.TotalMilliseconds / testCount);
        }

        private void SolveGaussPartialPivot(int testCount)
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

            MyMatrixIoHandler.WriteToFileWithTimespan(IoConsts.CsharpGaussPartialPivot,
                MyMatrixFormatter.GetFormattedVector(vector),
                CurrentMatrixSize,
                _time.TotalMilliseconds / testCount);
        }
    }
}
