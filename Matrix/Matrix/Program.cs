using System;
using System.Globalization;
using System.Threading;

namespace Matrix
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");

            if (args.Length != 1)
            {
                DisplayHelp();
                return;
            }

            var tester = new MyMatrixTestRunner(50);
            switch (args[0])
            {
                case "-p":
                    PrepareMatrices(tester);
                    PerformMatrixOperations(tester);
                    break;

                case "-c":
                    CompareOutput(tester);
                    break;

                default:
                    DisplayHelp();
                    break;
            }
        }

        public static void DisplayHelp()
        {
            Console.WriteLine("Invalid option!");
            Console.WriteLine("-p prepare matrices + perform operations");
            Console.WriteLine("-c compare output");
        }

        public static void PrepareMatrices(MyMatrixTestRunner tr)
        {
            tr.WriteMatrices();
        }

        public static void PerformMatrixOperations(MyMatrixTestRunner tr)
        {
            tr.MatrixMulVectorTest();
            tr.MatrixAddMatrixMulVectorTest();
            tr.MatrixMulMatrixTest();

            try
            {
                tr.MatrixGaussianReductionNoPivotTest();
                tr.MatrixGaussianReductionPartialPivotTest();
                tr.MatrixGaussianReductionFullPivotTest();
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void CompareOutput(MyMatrixTestRunner tester)
        {
            
        }
    }
}
