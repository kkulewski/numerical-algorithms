using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mushrooms
{
    public class Game
    {
        public int BoardSize;

        public Dice Dice;

        public Dictionary<int, GameState> GameStates;

        public int BoardBound => (BoardSize - 1) / 2;

        public int InitialStateIndex;

        private readonly int _player1InitialPosition;

        private readonly int _player2InitialPosition;

        public Game(int boardSize, int player1Position, int player2Position, Dice dice)
        {
            BoardSize = boardSize;
            _player1InitialPosition = player1Position;
            _player2InitialPosition = player2Position;
            Dice = dice;
        }

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

                        var isInitialState = isPlayer1Turn
                                             && player1Pos == _player1InitialPosition
                                             && player2Pos == _player2InitialPosition;
                        if (isInitialState)
                            InitialStateIndex = gameStateId;

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

                bool gameFinished = Player1Won(currentState) || Player2Won(currentState);
                if (gameFinished)
                    continue;

                foreach (var nextState in GameStates.Values)
                {
                    bool turnChanges = currentState.IsPlayer1Turn != nextState.IsPlayer1Turn;

                    foreach (var toss in Dice)
                    {
                        bool isPlayer1Turn = currentState.IsPlayer1Turn;
                        bool player1Moves = IsValidMove(currentState.Player1Position, nextState.Player1Position, toss.Value);
                        bool player2Waits = nextState.Player2Position == currentState.Player2Position;

                        bool isPlayer2Turn = !currentState.IsPlayer1Turn;
                        bool player2Moves = IsValidMove(currentState.Player2Position, nextState.Player2Position, toss.Value);
                        bool player1Waits = nextState.Player1Position == currentState.Player1Position;

                        bool possibleTransition = isPlayer1Turn && player1Moves && player2Waits && turnChanges
                                                  || isPlayer2Turn && player2Moves && player1Waits && turnChanges;

                        bool alreadyInList = currentState.Transitions.Select(s => s.Item1).Contains(nextState.GameStateId);

                        if (possibleTransition && !alreadyInList)
                        {
                            currentState.Transitions.Add(new Tuple<int, double>(nextState.GameStateId, toss.Probability));
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

                if (Player1Won(state))
                {
                    probabilityVector[row] = 1;
                    continue;
                }

                if (Player2Won(state))
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

        private static bool Player1Won(GameState state)
        {
            return state.Player1Position == 0;
        }

        private static bool Player2Won(GameState state)
        {
            return state.Player2Position == 0;
        }
    }
}
