using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mushrooms.Helpers
{
    public class ApproximationFunction
    {
        private readonly double[] _polynomial;

        public ApproximationFunction(double[] polynomial)
        {
            _polynomial = polynomial;
        }

        public double GetResult(double argument)
        {
            var result = 0.0;
            for (var i = 0; i < _polynomial.Length; i++)
            {
                result += _polynomial[i] * Math.Pow(argument, i);
            }

            return result;
        }
    }
}
