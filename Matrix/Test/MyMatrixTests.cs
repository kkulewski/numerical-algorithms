using Xunit;
using Matrix;
using System;

namespace Test
{
    public class MyMatrixTests
    {
        [Fact]
        public void SquareMatricesAreAddedCorrectly()
        {
            var a = new MyMatrix<int>(new[,]
            {
                {1, 2},
                {3, 4}
            });

            var b = new MyMatrix<int>(new[,]
            {
                {2, 3},
                {1, 2}
            });

            var c = a + b;

            // [1 2] + [2 3] = [3 5]
            // [3 4] + [1 2] = [4 6]
            Assert.Equal(3, c[0, 0]);
            Assert.Equal(5, c[0, 1]);
            Assert.Equal(4, c[1, 0]);
            Assert.Equal(6, c[1, 1]);
        }

        [Fact]
        public void SquareMatricesMultiplicatedCorrectly()
        {
            var a = new MyMatrix<int>(new[,]
            {
                {1, 2},
                {3, 4}
            });

            var b = new MyMatrix<int>(new[,]
            {
                {2, 3},
                {1, 2}
            });

            var c = a * b;

            // [1 2] * [2 3] = [4   7]
            // [3 4] * [1 2] = [10 17]
            Assert.Equal(4, c[0, 0]);
            Assert.Equal(7, c[0, 1]);
            Assert.Equal(10, c[1, 0]);
            Assert.Equal(17, c[1, 1]);
        }

        [Fact]
        public void NonSquare1X3M3X1MatricesMultiplicatedCorrectly()
        {
            var a = new MyMatrix<int>(new[,]
            {
                {1, 2, 3 },
            });

            var b = new MyMatrix<int>(new[,]
            {
                {2 },
                {1 },
                {2 }
            });

            var c = a * b;

            // [1 2 3] * [2] = [10]
            //         * [1] = 
            //         * [2] =
            Assert.Equal(10, c[0, 0]);
        }

        [Fact]
        public void NonSquare2X3M3X2MatricesMultiplicatedCorrectly()
        {
            var a = new MyMatrix<int>(new[,]
            {
                {1, 2, 3 },
                {4, 5, 6 }
            });

            var b = new MyMatrix<int>(new[,]
            {
                {1, 2 },
                {3, 4 },
                {5, 6 }
            });

            var c = a * b;

            // [1 2 3] * [1 2] = [22 28]
            // [4 5 6] * [3 4] = [49 64]
            //         * [5 6] =
            Assert.Equal(22, c[0, 0]);
            Assert.Equal(28, c[0, 1]);
            Assert.Equal(49, c[1, 0]);
            Assert.Equal(64, c[1, 1]);
        }

        [Fact]
        public void NonSquare3X3M3X2MatricesMultiplicatedCorrectly()
        {
            var a = new MyMatrix<int>(new[,]
            {
                {1, 2, 3 },
                {4, 5, 6 },
                {1, 2, 4 }
            });

            var b = new MyMatrix<int>(new[,]
            {
                {1, 2 },
                {3, 4 },
                {5, 6 }
            });

            var c = a * b;

            // [1 2 3] * [1 2] = [22 28]
            // [4 5 6] * [3 4] = [49 64]
            // [1 2 4] * [5 6] = [27 70]
            Assert.Equal(22, c[0, 0]);
            Assert.Equal(28, c[0, 1]);
            Assert.Equal(49, c[1, 0]);
            Assert.Equal(64, c[1, 1]);
            Assert.Equal(27, c[2, 0]);
            Assert.Equal(34, c[2, 1]);
        }

        [Fact]
        public void FractionMatriceMultiplication()
        {
            var a = new MyMatrix<Fraction>(new[,]
            {
                { new Fraction(1, 2), new Fraction(1, 3), }
            });

            var b = new MyMatrix<Fraction>(new[,]
            {
                {new Fraction(1, 4) },
                {new Fraction(1, 2), }
            });

            var c = a * b;

            // [1/2 1/3] * [1/4] = [1/8 + 1/6 =  6/48 + 8/48 = 14/48 = 7/24]
            //           * [1/2] =
            Assert.Equal(7, c[0, 0].Numerator);
            Assert.Equal(24, c[0, 0].Denominator);
        }

        [Fact]
        public void IntMatrixWithVectorMultiplication()
        {
            var a = new MyMatrix<int>(new[,]
            {
                {1, 2 },
                {3, 4 }
            });

            var b = new MyMatrix<int>(new [,]
            {
                {2 },
                {4 }
            });

            var c = a * b;

            // [1 2] * [2] = [10]
            // [3 4] * [4] = [22]
            Assert.Equal(10, c[0, 0]);
            Assert.Equal(22, c[1, 0]);
        }

        [Fact]
        public void MatrixGaussianReduction()
        {
            var m = new MyMatrix<int>(new[,]
            {
                {1, 2, 3 },
                {4, 5, 6 },
                {7, 8, 8 }
            });

            var v = new[] { 1, 2, 4 };

            // [1 2 3 | 1 ] = [1  2  3 | 1 ]
            // [4 5 6 | 2 ] = [0 -3 -6 |-2 ]
            // [7 8 9 | 4 ] = [0  0 -1 | 1 ]
            MyMatrix<int>.GaussReduction(m, v);
            
            // [1 2 3 | 1 ] = [1  2  3 | 1 ]
            Assert.Equal(1, m[0, 0]);
            Assert.Equal(2, m[0, 1]);
            Assert.Equal(3, m[0, 2]);
            Assert.Equal(1, v[0]);

            // [4 5 6 | 2 ] = [0 -3 -6 |-2 ]
            Assert.Equal(0, m[1, 0]);
            Assert.Equal(-3, m[1, 1]);
            Assert.Equal(-6, m[1, 2]);
            Assert.Equal(-2, v[1]);

            // [7 8 9 | 4 ] = [0  0 -1 | 1 ]
            Assert.Equal(0, m[2, 0]);
            Assert.Equal(0, m[2, 1]);
            Assert.Equal(-1, m[2, 2]);
            Assert.Equal(1, v[2]);
        }

        [Fact]
        public void MatrixGaussianReductionDouble()
        {
            var m = new MyMatrix<double>(new[,]
            {
                {1.0, 2.0, 3.0 },
                {4.0, 5.0, 6.0 },
                {7.0, 8.0, 8.0 }
            });

            var v = new[] { 1.0, 2.0, 4.0 };

            // [1 2 3 | 1 ] = [1  2  3 | 1 ]
            // [4 5 6 | 2 ] = [0 -3 -6 |-2 ]
            // [7 8 9 | 4 ] = [0  0 -1 | 1 ]
            MyMatrix<double>.GaussReduction(m, v);

            Assert.Equal(1.0, m[0, 0]);
            Assert.Equal(2.0, m[0, 1]);
            Assert.Equal(3.0, m[0, 2]);
            Assert.Equal(1.0, v[0]);
            
            Assert.Equal(0.0, m[1, 0]);
            Assert.Equal(-3.0, m[1, 1]);
            Assert.Equal(-6.0, m[1, 2]);
            Assert.Equal(-2.0, v[1]);
            
            Assert.Equal(0.0, m[2, 0]);
            Assert.Equal(0.0, m[2, 1]);
            Assert.Equal(-1.0, m[2, 2]);
            Assert.Equal(1.0, v[2]);
        }

        [Fact]
        public void MatrixGaussianReductionCoefficentDouble()
        {
            var m = new MyMatrix<double>(new[,]
            {
                {1.0, 2.0, 3.0 },
                {4.0, 5.0, 6.0 },
                {7.0, 8.0, 8.0 }
            });

            var v = new[] { 1.0, 2.0, 4.0 };

            MyMatrix<double>.GaussReduction(m, v);

            // [1  2  3 | 1 ] = [1  0  0 | -1.33 ]
            // [0 -3 -6 |-2 ] = [0 -3  0 |    -8 ]
            // [0  0 -1 | 1 ] = [0  0 -1 |     1 ]
            MyMatrix<double>.GaussReductionCoefficents(m, v);

            Assert.Equal(1.0, m[0, 0]);
            Assert.Equal(0.0, m[0, 1]);
            Assert.Equal(0.0, m[0, 2]);
            Assert.InRange(v[0], -1.4, -1.3);

            Assert.Equal(0.0, m[1, 0]);
            Assert.Equal(-3.0, m[1, 1]);
            Assert.Equal(0.0, m[1, 2]);
            Assert.Equal(-8.0, v[1]);

            Assert.Equal(0.0, m[2, 0]);
            Assert.Equal(0.0, m[2, 1]);
            Assert.Equal(-1.0, m[2, 2]);
            Assert.Equal(1.0, v[2]);
        }

        [Fact]
        public void MatrixGaussianReductionCoefficentIdentityDouble()
        {
            var m = new MyMatrix<double>(new[,]
            {
                {1.0, 2.0, 3.0 },
                {4.0, 5.0, 6.0 },
                {7.0, 8.0, 8.0 }
            });

            var v = new[] { 1.0, 2.0, 4.0 };

            MyMatrix<double>.GaussReduction(m, v);
            MyMatrix<double>.GaussReductionCoefficents(m, v);

            // [1  0  0 | -1.33 ] = [1 0 0 | -1.33 ]
            // [0 -3  0 |    -8 ] = [0 1 0 |  2.66 ]
            // [0  0 -1 |     1 ] = [0 0 1 | -1.00 ]
            MyMatrix<double>.GaussReductionIdentityMatrix(m, v);

            Assert.Equal(1.0, m[0, 0]);
            Assert.Equal(0.0, m[0, 1]);
            Assert.Equal(0.0, m[0, 2]);
            Assert.InRange(v[0], -1.4, -1.3);

            Assert.Equal(0.0, m[1, 0]);
            Assert.Equal(1.0, m[1, 1]);
            Assert.Equal(0.0, m[1, 2]);
            Assert.InRange(v[1], 2.6, 2.7);

            Assert.Equal(0.0, m[2, 0]);
            Assert.Equal(0.0, m[2, 1]);
            Assert.Equal(1.0, m[2, 2]);
            Assert.Equal(-1.0, v[2]);
        }


        [Fact]
        public void ThrowsOn_MatrixGaussianReduction_When_Diagonal_ContainsZero()
        {
            var m = new MyMatrix<int>(new[,]
            {
                {1, 2, 3 },
                {4, 8, 6 },
                {7, 8, 8 }
            });

            var v = new[] { 1, 2, 4 };

            // [1 2 3 | 1 ] = x
            // [4 8 6 | 2 ] = x
            // [7 8 9 | 4 ] = x

            // [1  2    3 |  1 ]
            // [0  0   -6 | -2 ] <-- leading zero in 2nd row, 2nd column
            // [0 -6  -12 | -3 ]

            Assert.Throws<ArgumentException>(() => MyMatrix<int>.GaussReduction(m, v));
        }
    }
}
