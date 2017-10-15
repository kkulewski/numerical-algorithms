using System;
using System.Collections.Generic;
using System.Linq;

namespace PowerSeries
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(GetArctanUsingMathLibrary(1));


            Console.WriteLine(GetArctanPowerSeries(1, 10000).Sum());
        }

        public static double GetArctanUsingMathLibrary(double x)
        {
            return Math.Atan(x);
        }

        public static IEnumerable<double> GetArctanPowerSeries(double argument, int lastIndex)
        {
            // (-1)^r * x^(2r+1) / (2r+1)
            Func<int, double> arctanFunc = r => Math.Pow(-1, r) * Math.Pow(argument, 2 * r + 1) / (2 * r + 1);
            return GetFunctionPowerSeries(arctanFunc, 0, lastIndex);
        }

        public static IEnumerable<double> GetFunctionPowerSeries(Func<int, double> function, int firstIndex, int lastIndex)
        {
            return Enumerable.Range(firstIndex, lastIndex).Select(function);
        }
    }
}
