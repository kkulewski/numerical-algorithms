using Xunit;
using Matrix;
using System;

namespace Test
{
    public class MyMatrixTests
    {
        private double doubleMargin = 0.001;

        [Fact]
        public void Adds_Int_SquareMatrices()
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
        public void Multiplicates_Int_SquareMatrices()
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
        public void Multiplicates_Int_Vectors()
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
        public void Multiplicates_Int_2x3_And_3x2_Matrices()
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
        public void Multiplicates_Int_3x3_And_3x2_Matrices()
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
        public void Multiplicates_Fraction_Vectors()
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
        public void Multiplicates_Int_Matrix_With_Vector()
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
        public void Reduces_LeftBottomTriangle_of_Int_Matrix()
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
            m.ReduceLeftBottomTriangle(v);
            
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
        public void Reduces_LeftBottomTriangle_Of_Double_Matrix()
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
            m.ReduceLeftBottomTriangle(v);
            
            Assert.InRange(m[0, 0], 1 - doubleMargin, 1 + doubleMargin);
            Assert.InRange(m[0, 1], 2 - doubleMargin, 2 + doubleMargin);
            Assert.InRange(m[0, 2], 3 - doubleMargin, 3 + doubleMargin);
            Assert.InRange(v[0], 1 - doubleMargin, 1 + doubleMargin);
            
            Assert.InRange(m[1, 0], 0 - doubleMargin, 0 + doubleMargin);
            Assert.InRange(m[1, 1], -3 - doubleMargin, -3 + doubleMargin);
            Assert.InRange(m[1, 2], -6 - doubleMargin, -6 + doubleMargin);
            Assert.InRange(v[1], -2 - doubleMargin, -2 + doubleMargin);

            Assert.InRange(m[2, 0], 0 - doubleMargin, 0 + doubleMargin);
            Assert.InRange(m[2, 1], 0 - doubleMargin, 0 + doubleMargin);
            Assert.InRange(m[2, 2], -1 - doubleMargin, -1 + doubleMargin);
            Assert.InRange(v[2], 1 - doubleMargin, 1 + doubleMargin);
        }

        [Fact]
        public void Reduces_LeftBottomTriangle_And_RightTopTriangle_Of_Double_SquareMatrix()
        {
            var m = new MyMatrix<double>(new[,]
            {
                {1.0, 2.0, 3.0 },
                {4.0, 5.0, 6.0 },
                {7.0, 8.0, 8.0 }
            });

            var v = new[] { 1.0, 2.0, 4.0 };

            m.ReduceLeftBottomTriangle(v);

            // after left bottom triangle reduction:
            // [1  2  3 | 1 ] = [1  0  0 | -1.33 ]
            // [0 -3 -6 |-2 ] = [0 -3  0 |    -8 ]
            // [0  0 -1 | 1 ] = [0  0 -1 |     1 ]
            m.ReduceRightTopTriangle(v);

            Assert.InRange(m[0, 0], 1 - doubleMargin, 1 + doubleMargin);
            Assert.InRange(m[0, 1], 0 - doubleMargin, 0 + doubleMargin);
            Assert.InRange(m[0, 2], 0 - doubleMargin, 0 + doubleMargin);
            Assert.InRange(v[0], -1.334, -1.333); // 1.(3)

            Assert.InRange(m[1, 0], 0 - doubleMargin, 0 + doubleMargin);
            Assert.InRange(m[1, 1], -3 - doubleMargin, -3 + doubleMargin);
            Assert.InRange(m[1, 2], 0 - doubleMargin, 0 + doubleMargin);
            Assert.InRange(v[1], -8 - doubleMargin, -8 + doubleMargin);

            Assert.InRange(m[2, 0], 0 - doubleMargin, 0 + doubleMargin);
            Assert.InRange(m[2, 1], 0 - doubleMargin, 0 + doubleMargin);
            Assert.InRange(m[2, 2], -1 - doubleMargin, -1 + doubleMargin);
            Assert.InRange(v[2], 1 - doubleMargin, 1 + doubleMargin);
        }

        [Fact]
        public void Returns_IdentityMatrix_Of_Double_SquareMatrix()
        {
            var m = new MyMatrix<double>(new[,]
            {
                {1.0, 2.0, 3.0 },
                {4.0, 5.0, 6.0 },
                {7.0, 8.0, 8.0 }
            });

            var v = new[] { 1.0, 2.0, 4.0 };

            m.ReduceLeftBottomTriangle(v);
            m.ReduceRightTopTriangle(v);

            // after left bottom + right top triangle reduction:
            // [1  0  0 | -1.33 ] = [1 0 0 | -1.33 ]
            // [0 -3  0 |    -8 ] = [0 1 0 |  2.66 ]
            // [0  0 -1 |     1 ] = [0 0 1 | -1.00 ]
            m.ToIdentityMatrix(v);
            
            Assert.InRange(m[0, 0], 1 - doubleMargin, 1 + doubleMargin);
            Assert.InRange(m[0, 1], 0 - doubleMargin, 0 + doubleMargin);
            Assert.InRange(m[0, 2], 0 - doubleMargin, 0 + doubleMargin);
            Assert.InRange(v[0], -1.334, -1.333); // -1.(3)

            Assert.InRange(m[1, 0], 0 - doubleMargin, 0.001);
            Assert.InRange(m[1, 1], 1 - doubleMargin, 1.001);
            Assert.InRange(m[1, 2], 0 - doubleMargin, 0.001);
            Assert.InRange(v[1], 2.666, 2.667); // 2.(6)

            Assert.InRange(m[2, 0], 0 - doubleMargin, 0 + doubleMargin);
            Assert.InRange(m[2, 1], 0 - doubleMargin, 0 + doubleMargin);
            Assert.InRange(m[2, 2], 1 - doubleMargin, 1 + doubleMargin);
            Assert.InRange(v[2], -1 - doubleMargin, -1 + doubleMargin);
        }


        [Fact]
        public void ThrowsOn_GaussReduceWithNoPivot_When_Diagonal_ContainsZero()
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

            Assert.Throws<ArgumentException>(() => m.GaussianReductionNoPivot(v));
        }

        [Fact]
        public void SolvesEquation_Using_GaussianReductionNoPivot()
        {
            var m = new MyMatrix<double>(new[,]
            {
                {1.0, 2.0, 3.0 },
                {4.0, 5.0, 6.0 },
                {7.0, 8.0, 8.0 }
            });

            var v = new[] { 1.0, 2.0, 4.0 };

            m.GaussianReductionNoPivot(v);
            Assert.InRange(v[0], -1.334, -1.333); // -1.(3)
            Assert.InRange(v[1], 2.666, 2.667); // 2.(6)
            Assert.InRange(v[2], -1 - doubleMargin, -1 + doubleMargin); // -1.0
        }

        [Fact]
        public void SwapsRow_Of_Double_Matrix()
        {
            var m = new MyMatrix<double>(new[,]
            {
                {1.0, 2.0 },
                {3.0, 4.0 },
                {5.0, 6.0 }
            });

            m.SwapRow(0, 1);

            Assert.InRange(m[0, 0], 3 - doubleMargin, 3 + doubleMargin);
            Assert.InRange(m[0, 1], 4 - doubleMargin, 4 + doubleMargin);
            Assert.InRange(m[1, 0], 1 - doubleMargin, 1 + doubleMargin);
            Assert.InRange(m[1, 1], 2 - doubleMargin, 2 + doubleMargin);
            Assert.InRange(m[2, 0], 5 - doubleMargin, 5 + doubleMargin);
            Assert.InRange(m[2, 1], 6 - doubleMargin, 6 + doubleMargin);
        }
    }
}
