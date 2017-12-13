namespace Mushrooms
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
                    stateMatrix[row, transition.Value.GameStateId] = -transition.Key.Probability;
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
    }
}
