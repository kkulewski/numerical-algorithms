using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mushrooms
{
    public static class GameMatrix
    {
        public static Tuple<MyMatrix<double>, double[]> GetGameMatrixAndProbabilityVector(Game game)
        {
            var size = game.GameStates.Count;
            var stateMatrix = new double[size, size];
            var probabilityVector = new double[size];

            for (var row = 0; row < size; row++)
            {
                stateMatrix[row, row] = 1;
                var state = game.GameStates[row];

                if (state.Player1Won)
                {
                    probabilityVector[row] = 1;
                    continue;
                }

                if (state.Player2Won)
                {
                    probabilityVector[row] = 0;
                    continue;
                }

                foreach (var transition in state.Transitions)
                {
                    stateMatrix[row, transition.Value.GameStateId] = -transition.Key.Probability;
                }
            }

            return new Tuple<MyMatrix<double>, double[]>(new MyMatrix<double>(stateMatrix), probabilityVector);
        }
    }
}
