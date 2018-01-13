using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;

namespace Mushrooms.GameData
{
    public static class GameMatrix
    {
        public static MyMatrix<double> GetStateMatrix(Game game)
        {
            var states = game.GameStates;
            var size = states.Count;
            var stateMatrix = new double[size, size];

            for (var row = 0; row < size; row++)
            {
                stateMatrix[row, row] = 1;
                foreach (var transition in states[row].Transitions)
                {
                    stateMatrix[row, transition.Value.GameStateId] += -transition.Key.Probability;
                }
            }

            return new MyMatrix<double>(stateMatrix);
        }

        public static double[] GetProbabilityVector(Game game)
        {
            var states = game.GameStates;
            var size = states.Count;
            var probabilityVector = new double[size];

            for (var row = 0; row < size; row++)
            {
                if (states[row].Player1Won)
                {
                    probabilityVector[row] = 1;
                }
            }

            return probabilityVector;
        }

        public static void SaveEquationsToSparseMatrix(Game game)
        {
            var states = game.GameStates;
            var totalRows = states.Count;
            var nonZeroValuesInRow = new int[totalRows];

            var sparseMatrix = new StringBuilder();
            sparseMatrix.AppendLine($"{totalRows}");

            for (var row = 0; row < totalRows; row++)
            {
                nonZeroValuesInRow[row] += 1;
                sparseMatrix.AppendLine($"{row} {row} 1");

                foreach (var transition in states[row].Transitions)
                {
                    nonZeroValuesInRow[row] += 1;
                    var column = transition.Value.GameStateId;
                    var value = -transition.Key.Probability;
                    sparseMatrix.AppendLine($"{row} {column} {value}");
                }
            }

            File.WriteAllText("input_sparse_matrix.txt", sparseMatrix.ToString());


            var sparseMatrixDensity = new StringBuilder();
            sparseMatrixDensity.AppendLine($"{totalRows}");

            for (var row = 0; row < totalRows; row++)
            {
                sparseMatrixDensity.AppendLine($"{nonZeroValuesInRow[row]}");
            }

            File.WriteAllText("input_sparse_matrix_density.txt", sparseMatrixDensity.ToString());
        }
    }
}
