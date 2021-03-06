﻿using System.Collections.Generic;

namespace Mushrooms.GameData
{
    public class GameState
    {
        public int GameStateId;

        public int Player1Position;

        public int Player2Position;

        public bool IsPlayer1Turn;

        public Dictionary<DiceFace, GameState> Transitions;

        public GameState(int gameStateId, int player1Position, int player2Position, bool isPlayer1Turn)
        {
            GameStateId = gameStateId;
            Player1Position = player1Position;
            Player2Position = player2Position;
            IsPlayer1Turn = isPlayer1Turn;
        }

        public bool Player1Won => Player1Position == 0;

        public bool Player2Won => Player2Position == 0;
    }
}
