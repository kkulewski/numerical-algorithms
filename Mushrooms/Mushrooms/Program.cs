using System;
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

                case "-a":
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
                        Console.Write("Board size: " + i);
                        gameConfig.BoardBound = i;
                        gtr.CreateGame(gameConfig);

                        Process.Start("eig.exe").WaitForExit();
                        gtr.SolveGameGaussPartial(defaultTestCount);
                        gtr.SolveGameGaussPartialSparse(defaultTestCount);
                        gtr.SolveGameGaussSeidel(defaultTestCount, defaultIterativeAccuracy);

                        Summarizer.WriteTimes();
                        Console.WriteLine(" ...finished");
                    }
                    break;

                case "-s":
                    Summarizer.WriteHeader();
                    Summarizer.WriteTimes();
                    break;

                default:
                    DisplayHelp();
                    break;
            }
        }

        public static void DisplayHelp()
        {
            Console.WriteLine("Invalid option!");
            Console.WriteLine("-g  TEST_COUNT                       -- solve game matrix (gauss)");
            Console.WriteLine("-i  TEST_COUNT  ITERATIONS           -- solve game matrix (iterative)");
            Console.WriteLine("-s                                   -- create summary");
        }
    }
}
