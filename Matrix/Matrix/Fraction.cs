using System;
using System.Numerics;

namespace Matrix
{
    public struct Fraction
    {
        private BigInteger _numerator;
        private BigInteger _denominator;

        public Fraction(BigInteger numerator, BigInteger denominator)
        {
            if(denominator == 0)
                throw new ArgumentException("Denominator cannot be equal to 0!");

            _numerator = numerator;
            _denominator = denominator;
        }

        public BigInteger GreatestCommonDivisor(BigInteger a, BigInteger b)
        {
            while (b != 0)
            {
                var temp = b;
                b = a % b;
                a = temp;
            }

            return a;
        }
    }
}
