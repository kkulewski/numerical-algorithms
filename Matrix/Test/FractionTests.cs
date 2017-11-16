using System;
using Matrix;
using Xunit;

namespace Test
{
    public class FractionTests
    {
        [Fact]
        public void ThrowsOn_FractionInit_When_Denominator_Equals_0()
        {
            Assert.Throws<ArgumentException>(() => new Fraction(1, 0));
        }

        [Fact]
        public void Simplifies_Fractions_On_Init()
        {
            var f1 = new Fraction(2, 4);
            var f2 = new Fraction(6, 9);
            var f3 = new Fraction(12, 2);

            Assert.Equal(1, f1.Numerator);
            Assert.Equal(2, f1.Denominator);

            Assert.Equal(2, f2.Numerator);
            Assert.Equal(3, f2.Denominator);

            Assert.Equal(6, f3.Numerator);
            Assert.Equal(1, f3.Denominator);
        }

        [Fact]
        public void Simplifies_Fraction_On_Multiply()
        {
            var f1 = new Fraction(2, 3);
            var f2 = new Fraction(3, 4);
            var f3 = f1 * f2;

            Assert.Equal(1, f3.Numerator);
            Assert.Equal(2, f3.Denominator);

        }
    }
}
