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

            if (args.Length < 1)
            {
                DisplayHelp();
                return;
            }

            var param = args.Length > 1 && args[1] != null ? int.Parse(args[1]) : 1;
            var tr = new MyMatrixTestRunner();

            switch (args[0])
            {
                case "-p":
                    tr.WriteMatrices(param);
                    break;

                case "-t":
                    tr.LoadMatrices();
                    PerformMatrixOperations(tr, param);
                    break;

                case "-c":
                    var ta = new MyMatrixTestAnalyzer();
                    ta.ElementaryOperationsNormComparison();
                    ta.GaussNormComparison();
                    ta.GaussTimeComparison();
                    break;

                default:
                    DisplayHelp();
                    break;
            }
        }

        public static void DisplayHelp()
        {
            Console.WriteLine("Invalid option!");
            Console.WriteLine("-p  MATRIX_SIZE   -- prepare matrices");
            Console.WriteLine("-t  TEST_COUNT    -- perform operations");
            Console.WriteLine("-c                -- compare output");
        }

        public static void PerformMatrixOperations(MyMatrixTestRunner tr, int testCount)
        {
            tr.MatrixMulVectorTest(testCount);
            tr.MatrixAddMatrixMulVectorTest(testCount);
            tr.MatrixMulMatrixTest(testCount);

            try
            {
                tr.MatrixGaussianReductionNoPivotTest(testCount);
                tr.MatrixGaussianReductionPartialPivotTest(testCount);
                tr.MatrixGaussianReductionFullPivotTest(testCount);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
