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

            var indiceList = new List<Tuple<int, double>>
            {
                new Tuple<int, double>(-1, 0.5),
                new Tuple<int, double>(1, 0.5)
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

                        bool player1ForwardNoCross = (currentState.Player1Position + toss.Item1 < n) && (nextState.Player1Position == currentState.Player1Position + toss.Item1);
                        bool player1BackwardNoCross = (currentState.Player1Position - toss.Item1 > 0) && (nextState.Player1Position == currentState.Player1Position - toss.Item1);
                        bool player1ForwardCross = toss.Item1 > 0 && (currentState.Player1Position + toss.Item1 > n) &&
                                            (nextState.Player1Position ==
                                             -n + (currentState.Player1Position - toss.Item1));
                        bool player1BackwardCross = toss.Item1 < 0 && (currentState.Player1Position + toss.Item1 < -n) &&
                                             (nextState.Player1Position ==
                                              n + (currentState.Player1Position - toss.Item1));

                        bool isPlayer1Turn = currentState.IsPlayer1Turn;
                        bool player1Moves = player1ForwardNoCross || player1BackwardNoCross || player1ForwardCross || player1BackwardCross;
                        bool player2Waits = nextState.Player2Position == currentState.Player2Position;

                        bool player2ForwardNoCross = (currentState.Player2Position + toss.Item1 < n) && (nextState.Player2Position == currentState.Player2Position + toss.Item1);
                        bool player2BackwardNoCross = (currentState.Player2Position - toss.Item1 > 0) && (nextState.Player2Position == currentState.Player2Position - toss.Item1);
                        bool player2ForwardCross = toss.Item1 > 0 && (currentState.Player2Position + toss.Item1 > n) &&
                                                   (nextState.Player2Position ==
                                                    -n + (currentState.Player2Position - toss.Item1));
                        bool player2BackwardCross = toss.Item1 < 0 && (currentState.Player2Position + toss.Item1 < -n) &&
                                                    (nextState.Player2Position ==
                                                     n + (currentState.Player2Position - toss.Item1));

                        bool isPlayer2Turn = !currentState.IsPlayer1Turn;
                        bool player2Moves = player2ForwardNoCross || player2BackwardNoCross || player2ForwardCross || player2BackwardCross;
                        bool player1Waits = nextState.Player1Position == currentState.Player1Position;

                        bool possibleTransition = isPlayer1Turn && player1Moves && player2Waits && turnChanges ||
                                                  isPlayer2Turn && player2Moves && player1Waits && turnChanges;

                        if (possibleTransition)
                            currentState.Transitions.Add(new Tuple<int, double>(nextState.GameStateId, toss.Item2));

                        // bool backwardNoCross = current.Pos - toss > 0 && next.Pos == current.Pos - toss)

                        // or
                        // bool forwardCross = toss > 0 && (current.pos + toss > n) && (next.Pos = -n + (current.Pos - toss))
                        // bool backwardCross = toss < 0 && (current.pos + toss < -n) && (next.Pos = n + (current.Pos - toss) 
                        //
                        // bool  pos1 + toss > n => pos2 = -n + toss-pos1
                    }
                    // hard-coded 1 is not a viable solution - it only works for dice with 2 indices (-1 and 1)
                    // for cubic dice (ie. -3 .. 3) there is need for separate branch for each number
                    // TODO: find a better way to handle it
                    //bool player1Waits = nextState.Player1Position == currentState.Player1Position;
                    //bool player1Moves = nextState.Player1Position == (currentState.Player1Position + 1) ||
                    //                    nextState.Player1Position == (currentState.Player1Position - 1) ||
                    //                    nextState.Player1Position == -n && currentState.Player1Position == n ||
                    //                    nextState.Player1Position == n && currentState.Player1Position == -n;

                    //bool player2Waits = nextState.Player2Position == currentState.Player2Position;
                    //bool player2Moves = nextState.Player2Position == (currentState.Player2Position + 1) ||
                    //                    nextState.Player2Position == (currentState.Player2Position - 1) ||
                    //                    nextState.Player2Position == -n && currentState.Player2Position == n ||
                    //                    nextState.Player2Position == n && currentState.Player2Position == -n;

                    //bool isPlayer1Turn = currentState.IsPlayer1Turn;
                    //bool isPlayer2Turn = !isPlayer1Turn;

                    //var possibleTransition = (isPlayer1Turn && player1Moves && player2Waits && turnChanges) ||
                    //                          (isPlayer2Turn && player2Moves && player1Waits && turnChanges);
                    
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
    }
}
