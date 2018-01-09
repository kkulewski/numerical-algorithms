using System;
using System.IO;
using System.Text;

namespace Mushrooms.IO
{
    public static class MyMatrixIoHandler
    {
        private static void WriteToFile(string fileName, string text, int matrixSize)
        {
            var sb = new StringBuilder();
            sb.AppendLine(matrixSize.ToString());
            sb.AppendLine(text);

            File.WriteAllText(fileName, sb.ToString());
        }

        public static void WriteToFileWithTimespan(string fileName, string text, int matrixSize, double time)
        {
            var sb = new StringBuilder();
            sb.AppendLine(time.ToString());
            sb.AppendLine(matrixSize.ToString());
            sb.AppendLine(text);

            File.WriteAllText(fileName, sb.ToString());
        }

        public static void WriteMatrixToFile<T>(MyMatrix<T> matrix, string fileName) where T : new()
        {
            var formattedMatrix = MyMatrixFormatter.GetFormattedMatrix(matrix);
            WriteToFile(fileName, formattedMatrix, matrix.Rows);
        }

        public static void WriteVectorToFile<T>(T[] vector, string fileName) where T : new()
        {
            var formattedVector = MyMatrixFormatter.GetFormattedVector(vector);
            WriteToFile(fileName, formattedVector, vector.Length);
        }

        public static void WriteTimespanToFile(double totalMiliseconds, string fileName)
        {
            WriteToFileWithTimespan(fileName, string.Empty, 0, totalMiliseconds);
        }

        public static Tuple<MyMatrix<double>, double> LoadDoubleMatrix(string fileName, bool withTime)
        {
            var lineOffset = withTime ? 1 : 0;
            var matrixFile = File.ReadAllLines(fileName);
            var matrixSize = int.Parse(matrixFile[0 + lineOffset]);
            var time = double.Parse(matrixFile[0]);
            var innerMatrix = new double[matrixSize, matrixSize];

            for (var i = 0; i < matrixSize; i++)
            {
                var line = matrixFile[i + lineOffset + 1];
                var values = line.Split(' ');

                for (var j = 0; j < matrixSize; j++)
                {
                    innerMatrix[i, j] = double.Parse(values[j]);
                }
            }

            return new Tuple<MyMatrix<double>, double>(new MyMatrix<double>(innerMatrix), time);
        }

        public static Tuple<double[], double> LoadDoubleVector(string fileName, bool withTime)
        {
            var lineOffset = withTime ? 1 : 0;
            var matrixFile = File.ReadAllLines(fileName);
            var matrixSize = int.Parse(matrixFile[0 + lineOffset]);
            var time = double.Parse(matrixFile[0]);
            var vector = new double[matrixSize];

            var line = matrixFile[1 + lineOffset];
            var values = line.Split(' ');

            for (var j = 0; j < matrixSize; j++)
            {
                vector[j] = double.Parse(values[j]);
            }

            return new Tuple<double[], double>(vector, time);
        }
    }
}
