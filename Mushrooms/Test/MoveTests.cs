using System;
using System.Collections.Generic;
using System.Linq;
using Mushrooms;
using Xunit;

namespace Test
{
    public class MoveTests
    {
        public Game Game;

        public Game GetN4D2Game()
        {
            const int n = 4;
            const int boardSize = 2 * n + 1;
            const int player1Position = 4;
            const int player2Position = -4;
            const List<Tuple<int, double>> dice = null; 
            return new Game(boardSize, player1Position, player2Position, dice);
        }

        [Fact]
        public void ValidMoves_When_N4_D2_NoCross()
        {
            Game = GetN4D2Game();

            const int startPosition = 2;
            var endPositionForGivenToss = new Dictionary<int, int>
            {
                [-2] = 0,
                [-1] = 1,
                [1] = 3,
                [2] = 4
            };

            Assert.True(ValidMovesAccepted(startPosition, endPositionForGivenToss));
            Assert.True(InvalidMovesRejected(startPosition, endPositionForGivenToss));
        }

        [Fact]
        public void ValidMoves_When_N4_D2_Cross_N_Forward()
        {
            Game = GetN4D2Game();

            const int startPosition = 4;
            var endPositionForGivenToss = new Dictionary<int, int>
            {
                [-2] = 2,
                [-1] = 3,
                [1] = -4,
                [2] = -3
            };
            
            Assert.True(ValidMovesAccepted(startPosition, endPositionForGivenToss));
            Assert.True(InvalidMovesRejected(startPosition, endPositionForGivenToss));
        }

        [Fact]
        public void ValidMoves_When_N4_D2_Cross_N_Backward()
        {
            Game = GetN4D2Game();

            const int startPosition = -3;
            var endPositionForGivenToss = new Dictionary<int, int>
            {
                [-2] = 4,
                [-1] = -4,
                [1] = -2,
                [2] = -1
            };

            Assert.True(ValidMovesAccepted(startPosition, endPositionForGivenToss));
            Assert.True(InvalidMovesRejected(startPosition, endPositionForGivenToss));
        }

        [Fact]
        public void ValidMoves_When_N4_D2_Cross_0_Forward()
        {
            Game = GetN4D2Game();

            const int startPosition = -1;
            var endPositionForGivenToss = new Dictionary<int, int>
            {
                [-2] = -3,
                [-1] = -2,
                [1] = 0,
                [2] = 1
            };

            Assert.True(ValidMovesAccepted(startPosition, endPositionForGivenToss));
            Assert.True(InvalidMovesRejected(startPosition, endPositionForGivenToss));
        }

        [Fact]
        public void ValidMoves_When_N4_D2_Cross_0_Backward()
        {
            Game = GetN4D2Game();

            const int startPosition = 1;
            var endPositionForGivenToss = new Dictionary<int, int>
            {
                [-2] = -1,
                [-1] = 0,
                [1] = 2,
                [2] = 3
            };

            Assert.True(ValidMovesAccepted(startPosition, endPositionForGivenToss));
            Assert.True(InvalidMovesRejected(startPosition, endPositionForGivenToss));
        }

        [Fact]
        public void ValidMoves_When_N4_D2_In_0()
        {
            Game = GetN4D2Game();

            const int startPosition = 0;
            var endPositionForGivenToss = new Dictionary<int, int>
            {
                [-2] = -2,
                [-1] = -1,
                [1] = 1,
                [2] = 2
            };

            Assert.True(ValidMovesAccepted(startPosition, endPositionForGivenToss));
            Assert.True(InvalidMovesRejected(startPosition, endPositionForGivenToss));
        }

        private bool ValidMovesAccepted(int startPosition, Dictionary<int, int> endPositionForGivenToss)
        {
            var allValidMovesMatch =
             endPositionForGivenToss
                    .All(x => Game.IsValidMove(startPosition, x.Value, x.Key));

            return allValidMovesMatch;
        }

        private bool InvalidMovesRejected(int startPosition, Dictionary<int, int> endPositionForGivenToss)
        {
            var anyValidMovePlusOneMatch =
                endPositionForGivenToss
                    .Any(x => Game.IsValidMove(startPosition, x.Value + 1, x.Key));

            var anyValidMoveMinusOneMatch =
                endPositionForGivenToss
                    .Any(x => Game.IsValidMove(startPosition, x.Value - 1, x.Key));

            return !anyValidMovePlusOneMatch && !anyValidMoveMinusOneMatch;
        }
    }
}
