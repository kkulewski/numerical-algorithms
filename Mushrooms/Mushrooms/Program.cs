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
                    var param = args.Length > 1 && args[1] != null ? args[1] : "input.txt";
                    var config = new GameConfig(param);
                    tr.CreateGame(config);
                    break;

                case "-t":
                    var param1 = args.Length > 1 && args[1] != null ? int.Parse(args[1]) : 1;
                    var param2 = args.Length > 2 && args[2] != null ? int.Parse(args[2]) : 10;
                    tr.SolveGameMatrix(param1, param2);
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
            Console.WriteLine("-p  CONFIG_FILE             -- prepare game + run Monte-Carlo");
            Console.WriteLine("-t  TEST_COUNT  ITERATIONS  -- perform operations");
            Console.WriteLine("-c                          -- compare output");
        }
    }
}
