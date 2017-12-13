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

            var dice = Dice.GetDice
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
            var stateMatrix = GameMatrix.GetStateMatrix(game);
            var probabilityVector = GameMatrix.GetProbabilityVector(game);
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
                    p1Wins++;

                if (currentState.Player2Won)
                    p2Wins++;

            }

            Console.WriteLine("P1-WINS: " + p1Wins);
            Console.WriteLine("P2-WINS: " + p2Wins);
        }
    }
}
