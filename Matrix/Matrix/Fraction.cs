using System;
using System.Numerics;

namespace Matrix
{
    public struct Fraction
    {
        public BigInteger _numerator;
        public BigInteger _denominator;

        public Fraction(BigInteger numerator, BigInteger denominator)
        {
            if(denominator == 0)
                throw new ArgumentException("Denominator cannot be equal to 0!");

            _numerator = numerator;
            _denominator = denominator;
            Simplify();
        }

        public void Simplify()
        {
            var gcd = GreatestCommonDivisor(_numerator, _denominator);
            _numerator /= gcd;
            _denominator /= gcd;
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
