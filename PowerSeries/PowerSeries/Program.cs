using System;

namespace PowerSeries
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(GetArctanUsingMathLibrary(1));
        }

        public static double GetArctanUsingMathLibrary(int x)
        {
            return Math.Atan(x);
        }
    }
}
