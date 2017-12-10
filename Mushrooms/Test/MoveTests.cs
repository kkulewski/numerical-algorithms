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
        public void ValidMoves_When_N4_D2_No0Cross_NoNCross()
        {
            var n = 4;
            var boardSize = 2 * n + 1;
            int startPosition = 2;
            var endPositionForGivenToss = new Dictionary<int, int>
            {
                [-2] = 0,
                [-1] = 1,
                [1] = 3,
                [2] = 4
            };

            var validMoves =
                endPositionForGivenToss
                .All(x => Program.CheckIfValidMove(startPosition, x.Value, x.Key, boardSize));
            Assert.True(validMoves);

            var invalidMovesPlus =
                endPositionForGivenToss
                .Any(x => Program.CheckIfValidMove(startPosition, x.Value + 1, x.Key, boardSize));
            Assert.False(invalidMovesPlus);

            var invalidMovesMinus =
                endPositionForGivenToss
                .Any(x => Program.CheckIfValidMove(startPosition, x.Value - 1, x.Key, boardSize));
            Assert.False(invalidMovesMinus);
        }

        [Fact]
        public void ValidMoves_When_N4_D2_No0Cross_NCross()
        {
            var n = 4;
            var boardSize = 2 * n + 1;
            int startPosition = 4;
            var endPositionForGivenToss = new Dictionary<int, int>
            {
                [-2] = 2,
                [-1] = 3,
                [1] = -4,
                [2] = -3
            };

            var validMoves =
                endPositionForGivenToss
                    .All(x => Program.CheckIfValidMove(startPosition, x.Value, x.Key, boardSize));
            Assert.True(validMoves);

            var invalidMovesPlusOne =
                endPositionForGivenToss
                    .Any(x => Program.CheckIfValidMove(startPosition, x.Value + 1, x.Key, boardSize));
            Assert.False(invalidMovesPlusOne);

            var invalidMovesMinusOne =
                endPositionForGivenToss
                    .Any(x => Program.CheckIfValidMove(startPosition, x.Value - 1, x.Key, boardSize));
            Assert.False(invalidMovesMinusOne);
        }
    }
}
