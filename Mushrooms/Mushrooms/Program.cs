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
            var stateMatrix = mv.Item1;
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


            // MONTE CARLO
            int p1Wins = 0, p2Wins = 0;
            for (var i = 0; i < 1000; i++)
            {
                var currentState = game.GameStates[game.InitialStateIndex];
                while (currentState.Player1Position != 0 && currentState.Player2Position != 0)
                {
                    var toss = dice.Toss();
                    currentState = currentState.Transitions.Find(p => p.Item2.Value == toss.Value).Item1;
                }

                if (currentState.Player1Position == 0)
                    p1Wins++;

                if (currentState.Player2Position == 0)
                    p2Wins++;

            }

            Console.WriteLine("P1-WINS: " + p1Wins);
            Console.WriteLine("P2-WINS: " + p2Wins);
        }
    }
}
