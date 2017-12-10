using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mushrooms
{
    class Game
    {
        public int BoardSize;

        public List<Tuple<int, double>> Dice;

        public Dictionary<int, GameState> GameStates;

        public int Player1InitialPosition;

        public int Player2InitialPosition;

        public int BoardBound => (BoardSize - 1) / 2;

        public void GeneratePossibleStates()
        {
            GameStates = new Dictionary<int, GameState>();
            var gameStateId = 0;

            for (var player1Pos = -BoardBound; player1Pos <= BoardBound; player1Pos++)
            {
                for (var player2Pos = -BoardBound; player2Pos <= BoardBound; player2Pos++)
                {
                    if (player1Pos == 0 && player2Pos == 0)
                        continue;

                    for (var turn = 0; turn < 2; turn++)
                    {
                        var isPlayer1Turn = turn == 0;
                        var gameState = new GameState(gameStateId, player1Pos, player2Pos, isPlayer1Turn);
                        GameStates[gameStateId] = gameState;
                        gameStateId++;
                    }
                }
            }
        }

        public void GeneratePossibleTransitions()
        {
            // ASSIGN TRANSITION LIST TO EACH STATE
            foreach (var currentState in GameStates.Values)
            {
                currentState.Transitions = new List<Tuple<int, double>>();

                bool gameFinished = currentState.Player1Position == 0
                                    || currentState.Player2Position == 0;
                if (gameFinished)
                    continue;

                foreach (var nextState in GameStates.Values)
                {
                    bool turnChanges = currentState.IsPlayer1Turn != nextState.IsPlayer1Turn;

                    foreach (var toss in Dice)
                    {
                        bool isPlayer1Turn = currentState.IsPlayer1Turn;
                        bool player1Moves = IsValidMove(currentState.Player1Position, nextState.Player1Position, toss.Item1);
                        bool player2Waits = nextState.Player2Position == currentState.Player2Position;

                        bool isPlayer2Turn = !currentState.IsPlayer1Turn;
                        bool player2Moves = IsValidMove(currentState.Player2Position, nextState.Player2Position, toss.Item1);
                        bool player1Waits = nextState.Player1Position == currentState.Player1Position;

                        bool possibleTransition = isPlayer1Turn && player1Moves && player2Waits && turnChanges
                                                  || isPlayer2Turn && player2Moves && player1Waits && turnChanges;

                        bool alreadyInList = currentState.Transitions.Select(s => s.Item1).Contains(nextState.GameStateId);

                        if (possibleTransition && !alreadyInList)
                        {
                            currentState.Transitions.Add(new Tuple<int, double>(nextState.GameStateId, toss.Item2));
                        }
                    }
                }
            }
        }

        public bool IsValidMove(int startPosition, int endPosition, int toss)
        {
            bool forwardNoCross = (toss > 0)
                                  && (startPosition + toss <= BoardBound)
                                  && (endPosition == startPosition + toss);

            bool backwardNoCross = (toss < 0)
                                   && (startPosition - toss >= -BoardBound)
                                   && (endPosition == startPosition + toss);

            bool forwardCross = (toss > 0)
                                && (startPosition + toss > BoardBound)
                                && (endPosition == -BoardBound + (toss - 1 - (BoardBound - startPosition)));

            bool backwardCross = (toss < 0)
                                 && (startPosition + toss < -BoardBound)
                                 && (endPosition == BoardBound + (toss + 1 + (BoardBound + startPosition)));

            return forwardNoCross || backwardNoCross || forwardCross || backwardCross;
        }

        public Tuple<double[,], double[]> GetGameMatrixAndProbabilityVector()
        {
            var size = GameStates.Count;
            var stateMatrix = new double[size, size];
            var probabilityVector = new double[size];

            for (var row = 0; row < size; row++)
            {
                stateMatrix[row, row] = 1;
                var state = GameStates[row];

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

            return new Tuple<double[,], double[]>(stateMatrix, probabilityVector);
        }
    }
}
