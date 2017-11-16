using System;
using System.IO;
using System.Text;

namespace Matrix
{
    public class MyMatrixIoHandler
    {
        public const string PrefixFraction = "fraction_";
        public const string PrefixDouble = "double_";
        public const string PrefixFloat = "float_";
        
        private readonly Random _random = new Random();
        
        private void WriteToFile(string fileName, string text, int matrixSize)
        {
            // append matrix size
            var sb = new StringBuilder();
            sb.AppendLine(matrixSize.ToString());
            sb.AppendLine(text);

            File.WriteAllText(fileName, sb.ToString());
        }

        public MyMatrix<Fraction> GenerateRandomFractionMatrix(int matrixSize)
        {
            var fractionValues = new Fraction[matrixSize, matrixSize];
            for (var i = 0; i < matrixSize; i++)
            {
                for (var j = 0; j < matrixSize; j++)
                {
                    var numerator = _random.Next();
                    var denominator = _random.Next(1, int.MaxValue);
                    fractionValues[i, j] = new Fraction(numerator, denominator);
                }
            }

            return new MyMatrix<Fraction>(fractionValues);
        }

        public Fraction[] GenerateRandomFractionVector(int matrixSize)
        {
            var fractionVector = new Fraction[matrixSize];
            for (var j = 0; j < matrixSize; j++)
            {
                var numerator = _random.Next();
                var denominator = _random.Next(1, int.MaxValue);
                fractionVector[j] = new Fraction(numerator, denominator);
            }

            return fractionVector;
        }

        public MyMatrix<double> DoubleMatrixFromFractionMatrix(MyMatrix<Fraction> m)
        {
            var values = new double[m.Rows, m.Cols];
            for (var i = 0; i < m.Rows; i++)
            {
                for (var j = 0; j < m.Cols; j++)
                {
                    values[i, j] = (double) m.Matrix[i, j].Numerator /
                                   (double) m.Matrix[i, j].Denominator;
                }
            }

            return new MyMatrix<double>(values);
        }

        public double[] DoubleVectorFromFractionVector(Fraction[] vector)
        {
            var values = new double[vector.Length];
            for (var j = 0; j < vector.Length; j++)
            {
                values[j] = (double) vector[j].Numerator / (double) vector[j].Denominator;
            }

            return values;
        }

        public MyMatrix<float> FloatMatrixFromFractionMatrix(MyMatrix<Fraction> m)
        {
            var values = new float[m.Rows, m.Cols];
            for (var i = 0; i < m.Rows; i++)
            {
                for (var j = 0; j < m.Cols; j++)
                {
                    values[i, j] = (float) m.Matrix[i, j].Numerator /
                                   (float) m.Matrix[i, j].Denominator;
                }
            }

            return new MyMatrix<float>(values);
        }

        public float[] FloatVectorFromFractionVector(Fraction[] vector)
        {
            var values = new float[vector.Length];
            for (var j = 0; j < vector.Length; j++)
            {
                values[j] = (float) vector[j].Numerator / (float) vector[j].Denominator;
            }

            return values;
        }

        public void WriteDoubleMatrix(int matrixSize, string fileName)
        {
            var matrix = new MyMatrix<double>(matrixSize, matrixSize);
            var formattedMatrix = MyMatrixFormatter.GetFormattedMatrix(matrix);

            WriteToFile(fileName, formattedMatrix, matrixSize);
        }

        public void WriteFloatMatrix(int matrixSize, string fileName)
        {
            var matrix = new MyMatrix<float>(matrixSize, matrixSize);
            var formattedMatrix = MyMatrixFormatter.GetFormattedMatrix(matrix);

            WriteToFile(fileName, formattedMatrix, matrixSize);
        }

        public void WriteDoubleVector(int matrixSize, string fileName)
        {
            var vector = new double[matrixSize];
            for (var i = 0; i < matrixSize; i++)
                vector[i] = _random.NextDouble();
            var formattedVector = MyMatrixFormatter.GetFormattedVector(vector);

            WriteToFile(fileName, formattedVector, matrixSize);
        }

        public void WriteFloatVector(int matrixSize, string fileName)
        {
            var vector = new float[matrixSize];
            for (var i = 0; i < matrixSize; i++)
                vector[i] = (float) _random.NextDouble();
            var formattedVector = MyMatrixFormatter.GetFormattedVector(vector);

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

        public MyMatrix<float> LoadFloatMatrix(string fileName)
        {
            var matrixFile = File.ReadAllLines(fileName);
            var matrixSize = int.Parse(matrixFile[0]);
            var innerMatrix = new float[matrixSize, matrixSize];

            for (var i = 0; i < matrixSize; i++)
            {
                var line = matrixFile[i + 1].Replace("[", "").Replace("]", "");
                var values = line.Split(';');

                for (var j = 0; j < matrixSize; j++)
                {
                    innerMatrix[i, j] = float.Parse(values[j]);
                }
            }

            return new MyMatrix<float>(innerMatrix);
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

        public float[] LoadFloatVector(string fileName)
        {
            var matrixFile = File.ReadAllLines(fileName);
            var matrixSize = int.Parse(matrixFile[0]);
            var vector = new float[matrixSize];

            var line = matrixFile[1].Replace("[", "").Replace("]", "");
            var values = line.Split(';');

            for (var j = 0; j < matrixSize; j++)
            {
                vector[j] = float.Parse(values[j]);
            }

            return vector;
        }
    }
}
