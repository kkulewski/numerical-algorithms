using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mushrooms
{
    public class Program
    {
        static void Main(string[] args)
        {
            // k = 3 + 4%6 == 7
            var n = 4;

            var indiceList = new List<Tuple<int, double>>
            {
                new Tuple<int, double>(-2, 0.25),
                new Tuple<int, double>(-1, 0.25),
                new Tuple<int, double>(1, 0.25),
                new Tuple<int, double>(2, 0.25)
            };

            var boardSize = 2 * n + 1;


            // GENERATE ALL POSSIBLE GAME STATES
            var gameStates = new Dictionary<int, GameState>();
            var gameStateId = 0;

            for (var player1Pos = -n; player1Pos <= n; player1Pos++)
            {
                for (var player2Pos = -n; player2Pos <= n; player2Pos++)
                {
                    if (player1Pos == 0 && player2Pos == 0)
                        continue;

                    for (var turn = 0; turn < 2; turn++)
                    {
                        var isPlayer1Turn = turn == 0;
                        var gameState = new GameState(gameStateId, player1Pos, player2Pos, isPlayer1Turn);
                        gameStates[gameStateId] = gameState;
                        gameStateId++;
                    }
                }
            }


            // ASSIGN TRANSITION LIST TO EACH STATE
            foreach (var currentState in gameStates.Values)
            {
                currentState.Transitions = new List<Tuple<int, double>>();

                bool gameFinished = currentState.Player1Position == 0 ||
                                    currentState.Player2Position == 0;
                if(gameFinished)
                    continue;

                foreach (var nextState in gameStates.Values)
                {
                    bool turnChanges = currentState.IsPlayer1Turn != nextState.IsPlayer1Turn;

                    foreach (var toss in indiceList)
                    {
                        bool isPlayer1Turn = currentState.IsPlayer1Turn;
                        bool player1Moves = CheckIfValidMove(currentState.Player1Position, nextState.Player1Position, toss.Item1, boardSize);
                        bool player2Waits = nextState.Player2Position == currentState.Player2Position;

                        bool isPlayer2Turn = !currentState.IsPlayer1Turn;
                        bool player2Moves = CheckIfValidMove(currentState.Player2Position, nextState.Player2Position, toss.Item1, boardSize);
                        bool player1Waits = nextState.Player1Position == currentState.Player1Position;

                        bool possibleTransition = isPlayer1Turn && player1Moves && player2Waits && turnChanges ||
                                                  isPlayer2Turn && player2Moves && player1Waits && turnChanges;

                        if (possibleTransition)
                            currentState.Transitions.Add(new Tuple<int, double>(nextState.GameStateId, toss.Item2));
                    }
                }
            }


            // FILL MATRIX WITH TRANSITIONS
            var size = gameStates.Count;
            var stateMatrix = new double[size, size];
            var probabilityVector = new double[size];
            for (var row = 0; row < size; row++)
            {
                stateMatrix[row, row] = 1;
                var state = gameStates[row];

                bool player1Won = state.Player1Position == 0;
                bool player2Won = state.Player2Position == 0;

                if (player1Won)
                {
                    probabilityVector[row] = 1;
                    continue;

                }
                
                if (player2Won)
                {
                    probabilityVector[row] = 0;
                    continue;
                }

                foreach (var transition in state.Transitions)
                {
                    stateMatrix[row, transition.Item1] = -transition.Item2;
                }
            }


            // PRINT MATRIX
            for (var i = 0; i < size; i++)
            {
                Console.Write("{0:D2} [ ", i);
                for (var j = 0; j < size; j++)
                {
                    Console.Write("{0:N1} ", stateMatrix[i, j]);
                }
                Console.WriteLine(" ] [ {0} ]", probabilityVector[i]);
            }


            var mymatrix = new MyMatrix<double>(stateMatrix);
            //mymatrix.Jacobi(probabilityVector, 100);
            mymatrix.GaussianReductionNoPivot(probabilityVector);

        }

        public static bool CheckIfValidMove(int startPosition, int endPosition, int toss, int boardSize)
        {
            var n = (boardSize - 1) / 2;

            bool forwardNoCross = (toss > 0)
                && (startPosition + toss <= n)
                && (endPosition == startPosition + toss);

            bool backwardNoCross = (toss < 0)
                && (startPosition - toss >= -n)
                && (endPosition == startPosition + toss);

            bool forwardCross = (toss > 0)
                && (startPosition + toss > n)
                && (endPosition == -n + (toss - 1 - (n - startPosition)));

            bool backwardCross = (toss < 0)
                && (startPosition + toss < -n)
                && (endPosition == n + (toss + 1 + (n + startPosition)));
            
            return forwardNoCross || backwardNoCross || forwardCross || backwardCross;
        }
    }
}
