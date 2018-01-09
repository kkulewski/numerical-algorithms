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

            _stopwatch.Reset();
            _stopwatch.Start();

            _game = new Game(config.BoardSize, config.Player1Position, config.Player2Position, _dice);
            _game.GeneratePossibleStates();
            _game.GeneratePossibleTransitions();

            _stopwatch.Stop();
            var time = _stopwatch.Elapsed;

            WriteGameMatrix(_game, time.TotalMilliseconds);
            WriteProbabilityVector(_game);
            WriteInitialStateIndex(_game);
        }

        public void RunMonteCarlo(int iterations)
        {
            var p1Wins = 0;
            
            _time = new TimeSpan();
            for (var i = 0; i < iterations; i++)
            {
                _stopwatch.Reset();
                _stopwatch.Start();

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
                
                _stopwatch.Stop();
                _time += _stopwatch.Elapsed;
            }

            var winChance = (double) p1Wins / iterations;
            var time = _time.TotalMilliseconds;
            MyMatrixIoHandler.WriteToFileWithTimespan(IoConsts.PrefixWinChance + IoConsts.CsharpMonteCarlo,
                MyMatrixFormatter.GetFormattedVector(new[] { winChance }),
                1,
                time);
        }

        public void SolveGameIterative(int testCount, int iterations)
        {
            LoadMatrices();
            SolveJacobi(testCount, iterations);
            SolveGaussSeidel(testCount, iterations);
        }

        public void SolveGameGauss(int testCount)
        {
            LoadMatrices();
            SolveGaussPartialPivot(testCount);
            SolveGaussPartialPivotSparse(testCount);
        }

        private void WriteGameMatrix(Game game, double totalMiliseconds)
        {
            var matrix = GameMatrix.GetStateMatrix(game);
            MyMatrixIoHandler.WriteMatrixToFile(matrix, IoConsts.Matrix);
            MyMatrixIoHandler.WriteTimespanToFile(totalMiliseconds, IoConsts.MatrixGenerationTime);
        }

        private void WriteProbabilityVector(Game game)
        {
            var vector = GameMatrix.GetProbabilityVector(game);
            MyMatrixIoHandler.WriteVectorToFile(vector, IoConsts.Vector);
        }

        private void WriteInitialStateIndex(Game game)
        {
            var index = game.InitialStateIndex;
            File.WriteAllText(IoConsts.InitialStateIndex, index.ToString());
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

            var time = _time.TotalMilliseconds / testCount;
            MyMatrixIoHandler.WriteToFileWithTimespan(IoConsts.CsharpJacobi,
                MyMatrixFormatter.GetFormattedVector(vector),
                CurrentMatrixSize,
                time
                );

            MyMatrixIoHandler.WriteToFileWithTimespan(IoConsts.PrefixWinChance + IoConsts.CsharpJacobi,
                MyMatrixFormatter.GetFormattedVector(new[]{vector[GetInitialStateIndex()] }),
                1,
                time);
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

            var time = _time.TotalMilliseconds / testCount;
            MyMatrixIoHandler.WriteToFileWithTimespan(IoConsts.CsharpGaussSeidel,
                MyMatrixFormatter.GetFormattedVector(vector),
                CurrentMatrixSize,
                time);

            MyMatrixIoHandler.WriteToFileWithTimespan(IoConsts.PrefixWinChance + IoConsts.CsharpGaussSeidel,
                MyMatrixFormatter.GetFormattedVector(new[] { vector[GetInitialStateIndex()] }),
                1,
                time);
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

            var time = _time.TotalMilliseconds / testCount;
            MyMatrixIoHandler.WriteToFileWithTimespan(IoConsts.CsharpGaussPartialPivot,
                MyMatrixFormatter.GetFormattedVector(vector),
                CurrentMatrixSize,
                time);

            MyMatrixIoHandler.WriteToFileWithTimespan(IoConsts.PrefixWinChance + IoConsts.CsharpGaussPartialPivot,
                MyMatrixFormatter.GetFormattedVector(new[] { vector[GetInitialStateIndex()] }),
                1,
                time);
        }

        private void SolveGaussPartialPivotSparse(int testCount)
        {
            _time = new TimeSpan();
            var vector = Vector;

            for (var i = 0; i < testCount; i++)
            {
                var matrix = new MyMatrix<double>(Matrix);
                vector = Vector;

                _stopwatch.Reset();
                _stopwatch.Start();
                matrix.GaussianReductionPartialPivotSparse(vector);
                _stopwatch.Stop();
                _time += _stopwatch.Elapsed;
            }

            var time = _time.TotalMilliseconds / testCount;
            MyMatrixIoHandler.WriteToFileWithTimespan(IoConsts.CsharpGaussPartialPivotSparse,
                MyMatrixFormatter.GetFormattedVector(vector),
                CurrentMatrixSize,
                time);

            MyMatrixIoHandler.WriteToFileWithTimespan(IoConsts.PrefixWinChance + IoConsts.CsharpGaussPartialPivotSparse,
                MyMatrixFormatter.GetFormattedVector(new[] { vector[GetInitialStateIndex()] }),
                1,
                time);
        }

        private int GetInitialStateIndex()
        {
            var index = File.ReadAllText(IoConsts.InitialStateIndex);
            return int.Parse(index);
        }
    }
}
