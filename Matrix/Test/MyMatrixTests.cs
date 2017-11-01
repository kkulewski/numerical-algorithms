using Xunit;
using Matrix;

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
    }
}
