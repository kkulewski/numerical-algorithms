using System;
using System.Collections.Generic;
using Mushrooms.IO;

namespace Mushrooms
{
    public class Program
    {
        static void Main(string[] args)
        {
            var n = 4;
            var boardSize = 2 * n + 1;

            var p1Pos = n;
            var p2Pos = -n;

            var dice = new Dice
            (
                new Dictionary<int, double>
                {
                    [-2] = 0.25,
                    [-1] = 0.25,
                    [1] = 0.25,
                    [2] = 0.25
                }
            );

            var game = new Game(boardSize, p1Pos, p2Pos, dice);
            game.GeneratePossibleStates();
            game.GeneratePossibleTransitions();


            // SOLVE
            var mv = game.GetGameMatrixAndProbabilityVector();
            var stateMatrix = new MyMatrix<double>(mv.Item1);
            var probabilityVector = mv.Item2;
            var iterations = 100;

            // write generated matrix + vector
            MyMatrixIoHandler.WriteMatrixToFile(stateMatrix, "m.txt");
            MyMatrixIoHandler.WriteVectorToFile(probabilityVector, "v.txt");

            //stateMatrix.GaussianReductionPartialPivot(probabilityVector);
            //stateMatrix.Jacobi(probabilityVector, iterations);
            stateMatrix.GaussSeidel(probabilityVector, iterations);

            Console.WriteLine("[P1_POS: {0}, P2_POS: {1}, P1_TURN]: {2} win chance",
                p1Pos,
                p2Pos,
                probabilityVector[game.InitialStateIndex]);

            // write solved probability vector
            MyMatrixIoHandler.WriteVectorToFile(probabilityVector, "v-cs.txt");
        }
    }
}
