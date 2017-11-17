using System;
using System.IO;
using System.Numerics;
using System.Text;

namespace Matrix
{
    public class MyMatrixIoHandler
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

        public void WriteToFileWithTimespan(string fileName, string text, int matrixSize, double time)
        {
            // append matrix size
            var sb = new StringBuilder();
            sb.AppendLine(time.ToString());
            sb.AppendLine(matrixSize.ToString());
            sb.AppendLine(text);

            File.WriteAllText(fileName, sb.ToString());
        }

        public void WriteMatrixToFile<T>(MyMatrix<T> matrix, string fileName) where T : new()
        {
            var formattedMatrix = MyMatrixFormatter.GetFormattedMatrix(matrix);
            WriteToFile(fileName, formattedMatrix, matrix.Rows);
        }

        public void WriteVectorToFile<T>(T[] vector, string fileName) where T : new()
        {
            var formattedMatrix = MyMatrixFormatter.GetFormattedVector(vector);
            WriteToFile(fileName, formattedMatrix, vector.Length);
        }

        public void WriteFractionMatrixToFile(MyMatrix<Fraction> matrix, string fileName)
        {
            var formattedMatrix = MyMatrixFormatter.GetFormattedFractionMatrix(matrix);
            WriteToFile(fileName, formattedMatrix, matrix.Rows);
        }

        public void WriteFractionVectorToFile(Fraction[] vector, string fileName)
        {
            var formattedMatrix = MyMatrixFormatter.GetFormattedFractionVector(vector);
            WriteToFile(fileName, formattedMatrix, vector.Length);
        }

        public MyMatrix<Fraction> GenerateRandomFractionMatrix(int matrixSize)
        {
            var fractionValues = new Fraction[matrixSize, matrixSize];
            for (var i = 0; i < matrixSize; i++)
            {
                for (var j = 0; j < matrixSize; j++)
                {
                    var numerator = _random.Next(10);
                    var denominator = _random.Next(1, 10);
                    if(numerator > denominator)
                        fractionValues[i, j] = new Fraction(denominator, numerator);
                    else
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
                var numerator = _random.Next(10);
                var denominator = _random.Next(1, 10);
                if (numerator > denominator)
                    fractionVector[j] = new Fraction(denominator, numerator);
                else
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

        public Tuple<MyMatrix<Fraction>, double> LoadFractionMatrix(string fileName, bool withTime)
        {
            var lineOffset = withTime ? 1 : 0;
            var matrixFile = File.ReadAllLines(fileName);
            var matrixSize = int.Parse(matrixFile[0 + lineOffset]);
            var time = double.Parse(matrixFile[0]);
            var innerMatrix = new Fraction[matrixSize, matrixSize];

            for (var i = 0; i < matrixSize; i++)
            {
                var line = matrixFile[i + lineOffset + 1];
                var values = line.Split(' ');

                for (var j = 0; j < matrixSize; j++)
                {
                    var fraction = values[j].Split('/');
                    var numerator = BigInteger.Parse(fraction[0]);
                    var denominator = BigInteger.Parse(fraction[1]);
                    innerMatrix[i, j] = new Fraction(numerator, denominator);
                }
            }

            return new Tuple<MyMatrix<Fraction>, double>(new MyMatrix<Fraction>(innerMatrix), time);
        }

        public Tuple<MyMatrix<double>, double> LoadDoubleMatrix(string fileName, bool withTime)
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

        public Tuple<MyMatrix<float>, double> LoadFloatMatrix(string fileName, bool withTime)
        {
            var lineOffset = withTime ? 1 : 0;
            var matrixFile = File.ReadAllLines(fileName);
            var matrixSize = int.Parse(matrixFile[0 + lineOffset]);
            var time = double.Parse(matrixFile[0]);
            var innerMatrix = new float[matrixSize, matrixSize];

            for (var i = 0; i < matrixSize; i++)
            {
                var line = matrixFile[i + lineOffset + 1];
                var values = line.Split(' ');

                for (var j = 0; j < matrixSize; j++)
                {
                    innerMatrix[i, j] = float.Parse(values[j]);
                }
            }

            return new Tuple<MyMatrix<float>, double>(new MyMatrix<float>(innerMatrix), time);
        }

        public Tuple<Fraction[], double> LoadFractionVector(string fileName, bool withTime)
        {
            var lineOffset = withTime ? 1 : 0;
            var matrixFile = File.ReadAllLines(fileName);
            var matrixSize = int.Parse(matrixFile[0 + lineOffset]);
            var time = double.Parse(matrixFile[0]);
            var vector = new Fraction[matrixSize];

            var line = matrixFile[1 + lineOffset];
            var values = line.Split(' ');

            for (var j = 0; j < matrixSize; j++)
            {
                var fraction = values[j].Split('/');
                var numerator = BigInteger.Parse(fraction[0]);
                var denominator = BigInteger.Parse(fraction[1]);
                vector[j] = new Fraction(numerator, denominator);
            }

            return new Tuple<Fraction[], double>(vector, time);
        }

        public Tuple<double[], double> LoadDoubleVector(string fileName, bool withTime)
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

        public Tuple<float[], double> LoadFloatVector(string fileName, bool withTime)
        {
            var lineOffset = withTime ? 1 : 0;
            var matrixFile = File.ReadAllLines(fileName);
            var matrixSize = int.Parse(matrixFile[0 + lineOffset]);
            var time = double.Parse(matrixFile[0]);
            var vector = new float[matrixSize];

            var line = matrixFile[1 + lineOffset];
            var values = line.Split(' ');

            for (var j = 0; j < matrixSize; j++)
            {
                vector[j] = float.Parse(values[j]);
            }

            return new Tuple<float[], double>(vector, time);
        }
    }
}
