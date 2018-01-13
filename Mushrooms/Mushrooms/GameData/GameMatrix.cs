using System.Dynamic;
using System.IO;
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
            var size = states.Count;

            var output = new StringBuilder();
            output.AppendLine($"{size}");

            for (var row = 0; row < size; row++)
            {
                output.AppendLine($"{row} {row} 1");
                foreach (var transition in states[row].Transitions)
                {
                    var column = transition.Value.GameStateId;
                    var value = -transition.Key.Probability;
                    output.AppendLine($"{row} {column} {value}");
                }
            }

            File.WriteAllText("sparse_input_matrix.txt", output.ToString());
        }
    }
}
