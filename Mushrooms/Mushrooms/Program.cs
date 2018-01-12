﻿using System;
using System.Globalization;
using System.Threading;
using Mushrooms.IO;
using Mushrooms.Helpers;
using System.Diagnostics;

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
            const double defaultIterativeAccuracy = 1e-10;
            const int defaultTestCount = 1;
            const int defaultBoardStartSize = 3;
            const int defaultBoardEndSize = 10;
            const string eigenExecutableName = "eig.exe";

            var gtr = new GameTestRunner();
            switch (args[0])
            {
                case "-p":
                    var config = new GameConfig(IoConsts.GameConfig);
                    gtr.CreateGame(config);
                    break;

                case "-g":
                    gtr.SolveGameGaussPartial(defaultTestCount);
                    gtr.SolveGameGaussPartialSparse(defaultTestCount);
                    break;

                case "-i":
                    gtr.SolveGameGaussSeidel(defaultTestCount, defaultIterativeAccuracy);
                    break;

                case "-s":
                    Summarizer.WriteHeader();
                    Summarizer.WriteTimes();
                    break;

                case "-a":
                    const int padding = 40;
                    var startSize = args.Length > 1 && args[1] != null
                        ? int.Parse(args[1])
                        : defaultBoardStartSize;

                    var endSize = args.Length > 2 && args[2] != null
                        ? int.Parse(args[2])
                        : defaultBoardEndSize;

                    var gameConfig = new GameConfig(IoConsts.GameConfig);

                    Summarizer.WriteHeader();
                    for (var i = startSize; i <= endSize; i++)
                    {
                        Console.WriteLine(Environment.NewLine + "## Board size: " + i);
                        gameConfig.BoardBound = i;

                        Console.Write("# Creating game...".PadRight(padding));
                        gtr.CreateGame(gameConfig);
                        Console.WriteLine("done");

                        Console.Write("# Solving with Eigen...".PadRight(padding));
                        Process.Start(eigenExecutableName)?.WaitForExit();
                        Console.WriteLine("done");

                        Console.Write("# Solving with C#...".PadRight(padding));
                        gtr.SolveGameGaussPartial(defaultTestCount);
                        gtr.SolveGameGaussPartialSparse(defaultTestCount);
                        gtr.SolveGameGaussSeidel(defaultTestCount, defaultIterativeAccuracy);
                        Console.WriteLine("done");

                        Console.Write("# Writing time summary...".PadRight(padding));
                        Summarizer.WriteTimes();
                        Console.WriteLine("done");
                    }

                    Console.WriteLine(Environment.NewLine + "## Summaries");
                    Console.Write("# Writing time per method...".PadRight(padding));
                    Summarizer.WriteTimePerMethod();
                    Console.WriteLine("done");
                    Console.Write("# Writing approximation functions...".PadRight(padding));
                    Summarizer.WriteApproximationFunctions();
                    Console.WriteLine("done");
                    break;

                default:
                    DisplayHelp();
                    break;
            }
        }

        public static void DisplayHelp()
        {
            Console.WriteLine("Invalid option!");
            Console.WriteLine("-a START_SIZE  END_SIZE   -- run complete procedure");
            Console.WriteLine("-g                        -- solve game matrix (gauss)");
            Console.WriteLine("-i                        -- solve game matrix (iterative)");
            Console.WriteLine("-s                        -- create summary");
        }
    }
}
