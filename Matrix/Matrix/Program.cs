using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Matrix
{
    class Program
    {
        static void Main(string[] args)
        {
            CultureInfo ci = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;

            var tester = new MyMatrixTester(3);
            tester.WriteMatrices();
            
            tester.MatrixMulVectorTest();
            tester.MatrixAddMatrixMulVectorTest();
            tester.MatrixMulMatrixTest();

            try
            {
                tester.MatrixGaussianReductionNoPivotTest();
                tester.MatrixGaussianReductionPartialPivotTest();
                tester.MatrixGaussianReductionFullPivotTest();
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
