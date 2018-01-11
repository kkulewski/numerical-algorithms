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

        public static void WriteTimePerMethod()
        {
            MyMatrixIoHandler.WriteVectorToFile(LoadVectorFromSummary(0), IoConsts.PrefixTime + "size" + IoConsts.FileType);
            MyMatrixIoHandler.WriteVectorToFile(LoadVectorFromSummary(1), IoConsts.PrefixTime + "generation" + IoConsts.FileType);
            MyMatrixIoHandler.WriteVectorToFile(LoadVectorFromSummary(2), IoConsts.PrefixTime + IoConsts.CsharpGaussPartialPivot);
            MyMatrixIoHandler.WriteVectorToFile(LoadVectorFromSummary(3), IoConsts.PrefixTime + IoConsts.CsharpGaussPartialPivotSparse);
            MyMatrixIoHandler.WriteVectorToFile(LoadVectorFromSummary(4), IoConsts.PrefixTime + IoConsts.CsharpGaussSeidel);
            MyMatrixIoHandler.WriteVectorToFile(LoadVectorFromSummary(5), IoConsts.PrefixTime + IoConsts.EigenGaussPartialPivotSparse);
        }

        public static double[] LoadVectorFromSummary(int column)
        {
            var lines = File.ReadAllLines(IoConsts.SummaryMethodsTime);
            var linesCount = lines.Length;

            var boardSizes = new double[linesCount - 1];
            for (var i = 1; i < linesCount; i++)
            {
                boardSizes[i - 1] = double.Parse(lines[i].Split(';')[column]);
            }

            return boardSizes;
        }
    }
}
