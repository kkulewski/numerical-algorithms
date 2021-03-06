﻿using System;
using System.Diagnostics;
using System.IO;
using Mushrooms.GameData;
using Mushrooms.IO;

namespace Mushrooms.Helpers
{
    public class GameTestRunner
    {
        private readonly Stopwatch _stopwatch;
        private TimeSpan _time;

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

        public void CreateGameSparse(GameConfig config)
        {
            _dice = Dice.GetDice(config.DiceFaces);
            _game = new Game(config.BoardSize, config.Player1Position, config.Player2Position, _dice);
            _game.GeneratePossibleStates();
            _game.GeneratePossibleTransitions();
            GameMatrix.SaveEquationsToSparseMatrix(_game);
            WriteProbabilityVector(_game);
            WriteInitialStateIndex(_game);
        }

        public void SolveGameGaussSeidel(int testCount, double accuracy)
        {
            LoadMatrices();
            SolveGaussSeidel(testCount, accuracy);
        }

        public void SolveGameGaussPartial(int testCount)
        {
            LoadMatrices();
            SolveGaussPartialPivot(testCount);
        }

        public void SolveGameGaussPartialSparse(int testCount)
        {
            LoadMatrices();
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
                MyMatrixFormatter.GetFormattedVector(new[] { vector[GetInitialStateIndex()] }),
                1,
                time);
        }

        private void SolveGaussSeidel(int testCount, double accuracy)
        {
            _time = new TimeSpan();
            var vector = Vector;

            for (var i = 0; i < testCount; i++)
            {
                var matrix = new MyMatrix<double>(Matrix);
                vector = Vector;

                _stopwatch.Reset();
                _stopwatch.Start();
                matrix.GaussSeidel(vector, accuracy);
                _stopwatch.Stop();
                _time += _stopwatch.Elapsed;
            }

            var time = _time.TotalMilliseconds / testCount;
            MyMatrixIoHandler.WriteToFileWithTimespan(IoConsts.CsharpGaussSeidel,
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
