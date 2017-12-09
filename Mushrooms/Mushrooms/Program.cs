using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mushrooms
{
    class Program
    {
        static void Main(string[] args)
        {
            // k = 3 + 4%6 == 7
            var n = 1;
            var indices = 2;
            var indicesStart = -1;
            var indicesEnd = 1;

            var boardSize = 2 * n + 1;

            // GENERATE ALL POSSIBLE GAME STATES
            var gameStates = new Dictionary<int, GameState>();
            var gameStateId = 0;

            for (var player1Pos = -n; player1Pos <= n; player1Pos++)
            {
                for (var player2Pos = -n; player2Pos <= n; player2Pos++)
                {
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
                currentState.Transitions = new List<int>();
                foreach (var nextState in gameStates.Values)
                {
                    // hard-coded 1 is not a viable solution - it only works for dice with 2 indices (-1 and 1)
                    // for cubic dice (ie. -3 .. 3) there is need for separate branch for each number
                    // TODO: find a better way to handle it
                    bool player1Waits = nextState.Player1Position == currentState.Player1Position;
                    bool player1Moves = nextState.Player1Position == (currentState.Player1Position + 1) ||
                                        nextState.Player1Position == (currentState.Player1Position - 1) ||
                                        nextState.Player1Position == -n && currentState.Player1Position == n ||
                                        nextState.Player1Position == n && currentState.Player1Position == -n;

                    bool player2Waits = nextState.Player2Position == currentState.Player2Position;
                    bool player2Moves = nextState.Player2Position == (currentState.Player2Position + 1) ||
                                        nextState.Player2Position == (currentState.Player2Position - 1) ||
                                        nextState.Player2Position == -n && currentState.Player2Position == n ||
                                        nextState.Player2Position == n && currentState.Player2Position == -n;

                    bool isPlayer1Turn = currentState.IsPlayer1Turn;
                    bool isPlayer2Turn = !isPlayer1Turn;
                    bool turnChanges = currentState.IsPlayer1Turn != nextState.IsPlayer1Turn;

                    var possibleTransition = (isPlayer1Turn && player1Moves && player2Waits && turnChanges) ||
                                              (isPlayer2Turn && player2Moves && player1Waits && turnChanges);

                    if (possibleTransition)
                        currentState.Transitions.Add(nextState.GameStateId);
                }
            }

            // FILL MATRIX WITH TRANSITIONS
            var size = gameStates.Count;
            var stateMatrix = new double[size, size];
            var probabilityVector = new double[size];
            for (var row = 0; row < size; row++)
            {
                // TODO: exclude win (pos==0) or lose situations
                var state = gameStates[row];
                var probability = 0.0;
                foreach (var transition in state.Transitions)
                {

                    // TODO: check if condition makes sense
                    if (gameStates[transition].Player2Position != 0)
                    {
                        stateMatrix[row, transition] = -1.0 / indices;
                        probability += -1.0 / indices;
                    }

                }

                stateMatrix[row, row] = 1;
                probability += 1;

                probabilityVector[row] = probability;
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

            //var mymatrix = new MyMatrix<double>(stateMatrix);
            //mymatrix.Jacobi(probabilityVector, 100);

        }
    }
}
