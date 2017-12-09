using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mushrooms
{
    public class GameState
    {
        public int GameStateId;

        public int Player1Position;

        public int Player2Position;

        public bool IsPlayer1Turn;

        public List<int> PossibleGameStates = new List<int>();

        public GameState(int gameStateId, int player1Position, int player2Position, bool isPlayer1Turn)
        {
            GameStateId = gameStateId;
            Player1Position = player1Position;
            Player2Position = player2Position;
            IsPlayer1Turn = isPlayer1Turn;
        }
    }
}
