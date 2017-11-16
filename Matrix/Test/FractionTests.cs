using System;
using Matrix;
using Xunit;

namespace Test
{
    public class FractionTests
    {
        [Fact]
        public static void ThrowsOn_FractionInit_When_Denominator_Equals_0()
        {
            Assert.Throws<ArgumentException>(() => new Fraction(1, 0));
        }
    }
}
