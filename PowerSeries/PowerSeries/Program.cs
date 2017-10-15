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

            Console.WriteLine(GetArctanPowerSeries(1, 0, 10000).Sum());
        }

        public static double GetArctanUsingMathLibrary(double x)
        {
            return Math.Atan(x);
        }

        public static IEnumerable<double> GetArctanPowerSeries(double x, int first, int last)
        {
            return Enumerable.Range(first, last).Select(r => Math.Pow(-1, r) * Math.Pow(x, 2 * r + 1) / (2 * r + 1));
        }
    }
}
