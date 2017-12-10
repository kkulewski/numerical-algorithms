using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mushrooms;
using Xunit;

namespace Test
{
    public class MoveTests
    {
        [Fact]
        public void ValidMoves_When_N4_D2_NoCross()
        {
            const int n = 4;
            const int boardSize = 2 * n + 1;

            const int startPosition = 2;
            var endPositionForGivenToss = new Dictionary<int, int>
            {
                [-2] = 0,
                [-1] = 1,
                [1] = 3,
                [2] = 4
            };

            Assert.True(ValidMovesAccepted(startPosition, endPositionForGivenToss, boardSize));
            Assert.True(InvalidMovesRejected(startPosition, endPositionForGivenToss, boardSize));
        }

        [Fact]
        public void ValidMoves_When_N4_D2_Cross_N_Forward()
        {
            const int n = 4;
            const int boardSize = 2 * n + 1;

            const int startPosition = 4;
            var endPositionForGivenToss = new Dictionary<int, int>
            {
                [-2] = 2,
                [-1] = 3,
                [1] = -4,
                [2] = -3
            };
            
            Assert.True(ValidMovesAccepted(startPosition, endPositionForGivenToss, boardSize));
            Assert.True(InvalidMovesRejected(startPosition, endPositionForGivenToss, boardSize));
        }

        [Fact]
        public void ValidMoves_When_N4_D2_Cross_0_Forward()
        {
            const int n = 4;
            const int boardSize = 2 * n + 1;

            const int startPosition = -1;
            var endPositionForGivenToss = new Dictionary<int, int>
            {
                [-2] = -3,
                [-1] = -2,
                [1] = 0,
                [2] = 1
            };

            Assert.True(ValidMovesAccepted(startPosition, endPositionForGivenToss, boardSize));
            Assert.True(InvalidMovesRejected(startPosition, endPositionForGivenToss, boardSize));
        }

        private static bool ValidMovesAccepted(int startPosition, Dictionary<int, int> endPositionForGivenToss, int boardSize)
        {
            var allValidMovesMatch =
             endPositionForGivenToss
                    .All(x => Program.CheckIfValidMove(startPosition, x.Value, x.Key, boardSize));

            return allValidMovesMatch;
        }

        private static bool InvalidMovesRejected(int startPosition, Dictionary<int, int> endPositionForGivenToss, int boardSize)
        {
            var anyValidMovePlusOneMatch =
                endPositionForGivenToss
                    .Any(x => Program.CheckIfValidMove(startPosition, x.Value + 1, x.Key, boardSize));

            var anyValidMoveMinusOneMatch =
                endPositionForGivenToss
                    .Any(x => Program.CheckIfValidMove(startPosition, x.Value - 1, x.Key, boardSize));

            return !anyValidMovePlusOneMatch && !anyValidMoveMinusOneMatch;
        }
    }
}
