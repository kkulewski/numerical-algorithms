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
            var n = 2;
            var indices = 2;
            var indicesStart = -1;
            var indicesEnd = 1;

            var boardSize = 2 * n + 1;

            var gameStates = new Dictionary<int, GameState>();
            var gameStateId = 0;
            for (var turn = 0; turn < 2; turn++)
            {
                for (var player1Pos = -n; player1Pos <= n; player1Pos++)
                {
                    for (var player2Pos = -n; player2Pos <= n; player2Pos++)
                    {
                        gameStateId++;
                        var isPlayer1Turn = turn == 0;
                        var gameState = new GameState(gameStateId, player1Pos, player2Pos, isPlayer1Turn);
                        gameStates.Add(gameStateId, gameState);
                    }
                }
            }

            foreach (var currentState in gameStates.Values)
            {
                var transitions = new List<int>();
                foreach (var nextState in gameStates.Values)
                {
                    var player1Waited = nextState.Player1Position == currentState.Player1Position;
                    var player1Moved = nextState.Player1Position == currentState.Player1Position + 1 ||
                                        nextState.Player1Position == currentState.Player1Position + -1;

                    var player2Waited = nextState.Player2Position == currentState.Player2Position;
                    var player2Moved = nextState.Player2Position == currentState.Player2Position + 1 ||
                                        nextState.Player2Position == currentState.Player2Position + -1;

                    var turnChanged = nextState.IsPlayer1Turn != currentState.IsPlayer1Turn;

                    var possibleTransition = turnChanged && player1Moved && player2Waited ||
                                              turnChanged && player1Waited && player2Moved;

                    if (possibleTransition)
                        transitions.Add(nextState.GameStateId);
                }

                currentState.Transitions = transitions;
            }
        }
    }
}
