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
            var m = new MyMatrix<Fraction>(matrixSize, matrixSize);
            File.WriteAllText(fileName, MyMatrixWriter.GetFormattedMatrix(m));
        }

        public void WriteFractionVector(int matrixSize, string fileName)
        {
            var v = new Fraction[matrixSize];
            for (var i = 0; i < matrixSize; i++)
                v[i] = new Fraction(_random.Next(), _random.Next(1, int.MaxValue));

            File.WriteAllText(fileName, MyMatrixWriter.GetFormattedVector(v));
        }
    }
}
