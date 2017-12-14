using System;
using System.Globalization;
using System.Threading;
using Mushrooms.IO;

namespace Mushrooms
{
    public class Program
    {
        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");

            if (args.Length < 1)
            {
                DisplayHelp();
                return;
            }

            try
            {
                Run(args);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static void Run(string[] args)
        {
            var tr = new MyMatrixTestRunner();

            switch (args[0])
            {
                case "-p":
                    var configFileName = args.Length > 1 && args[1] != null ? args[1] : IoConsts.GameConfig;
                    var monteCarloIterations = args.Length > 2 && args[2] != null ? int.Parse(args[2]) : 1000000;
                    var config = new GameConfig(configFileName);
                    tr.CreateGame(config);
                    tr.RunMonteCarlo(monteCarloIterations);
                    break;

                case "-t":
                    var tests = args.Length > 1 && args[1] != null ? int.Parse(args[1]) : 1;
                    var iterations = args.Length > 2 && args[2] != null ? int.Parse(args[2]) : 100;
                    tr.SolveGame(tests, iterations);
                    break;

                case "-c":
                    MyMatrixTestAnalyzer.TimeComparison();
                    MyMatrixTestAnalyzer.NormComparison();
                    break;

                default:
                    DisplayHelp();
                    break;
            }
        }

        public static void DisplayHelp()
        {
            Console.WriteLine("Invalid option!");
            Console.WriteLine("-p  CONFIG_FILE M_CARLO_ITERATIONS   -- prepare game + run Monte-Carlo");
            Console.WriteLine("-t  TEST_COUNT  ITERATIONS           -- run tests (solve game matrix)");
            Console.WriteLine("-c                                   -- create summary");
        }
    }
}
