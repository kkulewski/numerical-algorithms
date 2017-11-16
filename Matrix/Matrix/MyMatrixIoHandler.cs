using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrix
{
    public class MatrixIoHandler
    {
        private readonly Random _random = new Random();
        
        private void WriteToFile(string fileName, string text, int matrixSize)
        {
            // append matrix size
            var sb = new StringBuilder();
            sb.AppendLine(matrixSize.ToString());
            sb.AppendLine(text);

            File.WriteAllText(fileName, sb.ToString());
        }

        public void WriteDoubleMatrix(int matrixSize, string fileName)
        {
            var matrix = new MyMatrix<double>(matrixSize, matrixSize);
            var formattedMatrix = MyMatrixWriter.GetFormattedMatrix(matrix);

            WriteToFile(fileName, formattedMatrix, matrixSize);
        }

        public void WriteDoubleVector(int matrixSize, string fileName)
        {
            var vector = new double[matrixSize];
            for (var i = 0; i < matrixSize; i++)
                vector[i] = _random.NextDouble();
            var formattedVector = MyMatrixWriter.GetFormattedVector(vector);

            WriteToFile(fileName, formattedVector, matrixSize);
        }

        public MyMatrix<double> LoadDoubleMatrix(string fileName)
        {
            var matrixFile = File.ReadAllLines(fileName);
            var matrixSize = int.Parse(matrixFile[0]);
            var innerMatrix = new double[matrixSize, matrixSize];

            for (var i = 0; i < matrixSize; i++)
            {
                var line = matrixFile[i + 1].Replace("[", "").Replace("]", "");
                var values = line.Split(';');

                for (var j = 0; j < matrixSize; j++)
                {
                    innerMatrix[i, j] = double.Parse(values[j]);
                }
            }

            return new MyMatrix<double>(innerMatrix);
        }

        public double[] LoadDoubleVector(string fileName)
        {
            var matrixFile = File.ReadAllLines(fileName);
            var matrixSize = int.Parse(matrixFile[0]);
            var vector = new double[matrixSize];

            var line = matrixFile[1].Replace("[", "").Replace("]", "");
            var values = line.Split(';');

            for (var j = 0; j < matrixSize; j++)
            {
                vector[j] = double.Parse(values[j]);
            }

            return vector;
        }
    }
}
