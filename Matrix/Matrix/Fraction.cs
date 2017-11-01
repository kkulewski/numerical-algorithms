using System;
using System.Numerics;

namespace Matrix
{
    public struct Fraction
    {
        public BigInteger Numerator { get; set; }
        public BigInteger Denominator { get; set; }

        public Fraction(BigInteger numerator, BigInteger denominator)
        {
            if(denominator == 0)
                throw new ArgumentException("Denominator cannot be equal to 0!");

            Numerator = numerator;
            Denominator = denominator;
            Simplify();
        }

        public void Simplify()
        {
            var gcd = GreatestCommonDivisor(Numerator, Denominator);
            Numerator /= gcd;
            Denominator /= gcd;

            if (Denominator < 0)
            {
                Denominator *= -1;
                Numerator *= -1;
            }
        }

        public static Fraction operator *(Fraction a, Fraction b)
        {
            var result = new Fraction(a.Numerator * b.Numerator, a.Denominator * b.Denominator);
            result.Simplify();

            return result;
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
