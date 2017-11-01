using System.Numerics;

namespace Matrix
{
    public struct Fraction
    {
        private BigInteger _numerator;
        private BigInteger _denominator;

        public Fraction(BigInteger numerator, BigInteger denominator)
        {
            _numerator = numerator;
            _denominator = denominator;
        }
    }
}
