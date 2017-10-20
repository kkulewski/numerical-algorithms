using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace PowerSeries
{
    class Program
    {
        static void Main(string[] args)
        {
            var arg = 1.0;
            var seriesLastElementIndex = 10;

            Console.WriteLine("Using library function:");
            Console.WriteLine(GetArctanUsingMathLibrary(arg));

            Console.WriteLine("Power series summation (first to last):");
            Console.WriteLine(GetArctanPowerSeries(arg, seriesLastElementIndex).Sum());

            Console.WriteLine("Power series summation (last to first):");
            Console.WriteLine(GetArctanPowerSeries(arg, seriesLastElementIndex).Reverse().Sum());

            Console.WriteLine("Sum each element basing on previous:");
            Console.WriteLine(GetArctanUsingPrevious(arg, seriesLastElementIndex));

            Console.WriteLine("Sum each element basing on previous inverse:");
            Console.WriteLine(GetArctanUsingPrevious(arg, seriesLastElementIndex));


            Console.WriteLine("Arctan using formula:");
            Console.WriteLine(GetArctanUsingFormula(arg, seriesLastElementIndex));
        }

        public static double GetArctanUsingMathLibrary(double x)
        {
            return Math.Atan(x);
        }

        public static IEnumerable<double> GetArctanPowerSeries(double argument, int lastIndex)
        {
            // (-1)^r * x^(2r+1) / (2r+1)
            Func<int, double> arctanFunc;
            if (argument > 1.0)
                argument = 1.0 / argument;

            arctanFunc = r => (Math.Pow(-1, r) * Math.Pow(argument, 2 * r + 1) / (2 * r + 1));

            return GetFunctionPowerSeries(arctanFunc, 0, lastIndex);
        }

        public static IEnumerable<double> GetFunctionPowerSeries(Func<int, double> function, int firstIndex, int lastIndex)
        {
            return Enumerable.Range(firstIndex, lastIndex).Select(function);
        }

        public static double GetArctanUsingPrevious(double arg, int lastIndex)
        {
            double sum = 1.0;
            double lastElement = 1.0;

            for (int i = 1; i < lastIndex; i++)
            {
                lastElement = lastElement * (-1) * (arg * arg) * ((2.0 * i + 1) / (2.0 * i + 3));
                sum += lastElement;
            }

            return sum;
        }

        public static double GetArctanUsingPreviousInv(double arg, int lastIndex)
        {
            double sum = 1.0;
            double lastElement = 1.0;

            for (int i = lastIndex; i > 0; i--)
            {
                lastElement = lastElement * (-1) * (arg * arg) * ((2.0 * i + 1) / (2.0 * i + 3));
                sum += lastElement;
            }

            return sum;
        }

        public static double GetArctanUsingFormula(double arg, int lastIndex)
        {
            double sum = 0;
            if (arg > 1.0)
                arg = 1 / arg;

            for (int i = 0; i < lastIndex; i++)
            {
                sum += Math.Pow(-1, i) * Math.Pow(arg, 2 * i + 1) / (2 * i + 1);
            }

            if (arg > 1.0)
            {
                return 3.14 / 2 - sum;
            }

            return 3.14 / 2 - sum;
        }
    }
}
