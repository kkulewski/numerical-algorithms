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
                var transitions = new List<int>();
                foreach (var nextState in gameStates.Values)
                {
                    var player1Waits = nextState.Player1Position == currentState.Player1Position;
                    var player1Moves = nextState.Player1Position == (currentState.Player1Position + 1) ||
                                        nextState.Player1Position == (currentState.Player1Position - 1);

                    var player2Waits = nextState.Player2Position == currentState.Player2Position;
                    var player2Moves = nextState.Player2Position == (currentState.Player2Position + 1) ||
                                        nextState.Player2Position == (currentState.Player2Position - 1);

                    var turnChanges = nextState.IsPlayer1Turn != currentState.IsPlayer1Turn;
                    var isPlayer1Turn = currentState.IsPlayer1Turn;

                    var possibleTransition = (isPlayer1Turn && player1Moves && player2Waits && turnChanges) ||
                                              (!isPlayer1Turn && player1Waits && player2Moves && turnChanges);

                    if (possibleTransition)
                        transitions.Add(nextState.GameStateId);
                }

                currentState.Transitions = transitions;
            }

            // FILL MATRIX WITH TRANSITIONS
            var size = gameStates.Count;
            var stateMatrix = new double[size, size];
            for (var row = 0; row < size; row++)
            {
                var state = gameStates[row];
                foreach (var transition in state.Transitions)
                {
                    stateMatrix[row, transition] = 1.0 / indices;
                }

                stateMatrix[row, row] = 1;
            }

            // PRINT MATRIX
            for (var i = 0; i < size; i++)
            {
                Console.Write("{0:D2} [ ", i);
                for (var j = 0; j < size; j++)
                {
                    Console.Write("{0:N1} ", stateMatrix[i, j]);
                }
                Console.WriteLine(" ]");
            }
        }
    }
}
