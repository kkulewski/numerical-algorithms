using System;
using System.Collections.Generic;

namespace Mushrooms
{
    public class GameState
    {
        public int Player1Position;

        public int Player2Position;

        public bool IsPlayer1Turn;

        public List<Tuple<GameState, DiceFace>> Transitions;

        public GameState(int player1Position, int player2Position, bool isPlayer1Turn)
        {
            Player1Position = player1Position;
            Player2Position = player2Position;
            IsPlayer1Turn = isPlayer1Turn;
        }
    }
}
