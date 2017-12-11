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
    }
}
