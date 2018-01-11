using Mushrooms.IO;
using System;
using System.IO;

namespace Mushrooms.Helpers
{
    public static class Summarizer
    {
        public static void WriteHeader()
        {
            var output = "size;generation-time;gauss-partial;gauss-partial-sparse;gauss-seidel;eigen-sparse";
            File.WriteAllText(IoConsts.SummaryMethodsTime, output + Environment.NewLine);
        }

        public static void WriteTimes()
        {
            var size = File.ReadAllLines(IoConsts.Matrix)[0];

            var generationTime = File.ReadAllLines(IoConsts.MatrixGenerationTime)[0];
            var gaussPartialTime = File.ReadAllLines(IoConsts.CsharpGaussPartialPivot)[0];
            var gaussPartialSparseTime = File.ReadAllLines(IoConsts.CsharpGaussPartialPivotSparse)[0];
            var gaussSeidelTime = File.ReadAllLines(IoConsts.CsharpGaussSeidel)[0];
            var eigenSparseTime = File.ReadAllLines(IoConsts.EigenGaussPartialPivotSparse)[0];

            var output = $"{size};{generationTime};{gaussPartialTime};{gaussPartialSparseTime};{gaussSeidelTime};{eigenSparseTime}";
            File.AppendAllText(IoConsts.SummaryMethodsTime, output + Environment.NewLine);
        }
    }
}
