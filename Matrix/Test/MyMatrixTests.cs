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

            // expected:
            // [3 5]
            // [4 6]
            Assert.Equal(3, c[0, 0]);
            Assert.Equal(5, c[0, 1]);
            Assert.Equal(4, c[1, 0]);
            Assert.Equal(6, c[1, 1]);
        }
    }
}
