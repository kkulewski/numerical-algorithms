﻿using System;
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
                Console.WriteLine(e.Message);
            }
        }

        public static void Run(string[] args)
        {
            const int defaultMonteCarloIterations = 1000000;
            const int defaultTestCount = 1;
            const int defaultSolveIterations = 100;
            var gtr = new GameTestRunner();
            int tests;

            switch (args[0])
            {
                case "-p":
                    var monteCarloIterations = args.Length > 1 && args[1] != null
                        ? int.Parse(args[1])
                        : defaultMonteCarloIterations;

                    var configFileName = args.Length > 2 && args[2] != null 
                        ? args[2] 
                        : IoConsts.GameConfig;

                    var config = new GameConfig(configFileName);
                    gtr.CreateGame(config);
                    break;

                case "-g":
                    tests = args.Length > 1 && args[1] != null 
                        ? int.Parse(args[1]) 
                        : defaultTestCount;

                    gtr.SolveGameGauss(tests);
                    break;

                case "-i":
                    tests = args.Length > 1 && args[1] != null
                        ? int.Parse(args[1])
                        : defaultTestCount;

                    var iterations = args.Length > 2 && args[2] != null
                        ? int.Parse(args[2])
                        : defaultSolveIterations;

                    gtr.SolveGameIterative(tests, iterations);
                    break;

                case "-s":
                    MyMatrixTestAnalyzer.WinChanceTimeSummary();
                    MyMatrixTestAnalyzer.WinChanceErrorSummary();
                    MyMatrixTestAnalyzer.TimeSummary();
                    MyMatrixTestAnalyzer.NormSummary();
                    break;

                default:
                    DisplayHelp();
                    break;
            }
        }

        public static void DisplayHelp()
        {
            Console.WriteLine("Invalid option!");
            Console.WriteLine("-p  M_CARLO_ITERATIONS  CONFIG_FILE  -- prepare game + run Monte-Carlo");
            Console.WriteLine("-g  TEST_COUNT                       -- solve game matrix (gauss)");
            Console.WriteLine("-i  TEST_COUNT  ITERATIONS           -- solve game matrix (iterative)");
            Console.WriteLine("-s                                   -- create summary");
        }
    }
}
