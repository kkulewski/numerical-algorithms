﻿using System;
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

        public void GeneratePossibleStates()
        {
            // GENERATE ALL POSSIBLE GAME STATES
            GameStates = new Dictionary<int, GameState>();
            var n = (BoardSize - 1) / 2;
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
                        GameStates[gameStateId] = gameState;
                        gameStateId++;
                    }
                }
            }
        }
    }
}
