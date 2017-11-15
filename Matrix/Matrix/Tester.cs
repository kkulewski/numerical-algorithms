using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrix
{
    public class Tester
    {
        private readonly Random _random = new Random();

        public void WriteFractionMatrix(int matrixSize, string fileName)
        {
            var matrix = new MyMatrix<Fraction>(matrixSize, matrixSize);
            var formattedMatrix = MyMatrixWriter.GetFormattedMatrix(matrix);

            WriteToFile(fileName, formattedMatrix, matrixSize);
        }

        public void WriteFractionVector(int matrixSize, string fileName)
        {
            var vector = new Fraction[matrixSize];
            for (var i = 0; i < matrixSize; i++)
                vector[i] = new Fraction(_random.Next(), _random.Next(1, int.MaxValue));
            var formattedVector = MyMatrixWriter.GetFormattedVector(vector);

            WriteToFile(fileName, formattedVector, matrixSize);
        }

        private void WriteToFile(string fileName, string text, int matrixSize)
        {
            // append matrix size
            var sb = new StringBuilder();
            sb.AppendLine(matrixSize.ToString());
            sb.AppendLine(text);

            File.WriteAllText(fileName, sb.ToString());
        }
    }
}
