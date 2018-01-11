using System;
using System.Globalization;
using System.Threading;
using Mushrooms.IO;
using Mushrooms.Helpers;

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
            const int defaultTestCount = 1;
            const int defaultBoardSize = 10;
            const double defaultIterativeAccuracy = 1e-10;
            var gtr = new GameTestRunner();

            switch (args[0])
            {
                case "-p":
                    var config = new GameConfig(IoConsts.GameConfig);
                    gtr.CreateGame(config);
                    break;

                case "-p2":
                    var size = args.Length > 1 && args[1] != null
                        ? int.Parse(args[1])
                        : defaultBoardSize;


                    break;

                case "-g":
                    gtr.SolveGameGaussPartial(defaultTestCount);
                    gtr.SolveGameGaussPartialSparse(defaultTestCount);
                    break;

                case "-i":
                    gtr.SolveGameGaussSeidel(defaultTestCount, defaultIterativeAccuracy);
                    break;

                case "-a":
                    var gameConfig = new GameConfig(IoConsts.GameConfig);
                    gtr.CreateGame(gameConfig);

                    gtr.SolveGameGaussPartial(defaultTestCount);
                    gtr.SolveGameGaussPartialSparse(defaultTestCount);
                    gtr.SolveGameGaussSeidel(defaultTestCount, defaultIterativeAccuracy);
                    break;

                case "-s":
                    Summarizer.SummarizeTime();
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
