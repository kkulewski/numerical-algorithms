using System;

namespace Mushrooms.Helpers
{
    public class ApproximationFunction
    {
        public readonly double[] Polynomial;

        public ApproximationFunction(double[] polynomial)
        {
            Polynomial = polynomial;
        }

        public double GetResult(double argument)
        {
            var result = 0.0;
            for (var i = 0; i < Polynomial.Length; i++)
            {
                result += Polynomial[i] * Math.Pow(argument, i);
            }

            return result;
        }

        public string GetFunctionString()
        {
            var output = string.Empty;
            for (var i = 0; i < Polynomial.Length; i++)
            {
                output += $"{Polynomial[i]}*x^{i} + ";
            }

            return output;
        }
    }
}
