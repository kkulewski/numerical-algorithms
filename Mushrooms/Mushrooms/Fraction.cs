using System;
using System.Collections.Generic;
using System.Numerics;

namespace Mushrooms
{
    public class Fraction
    {
        public BigInteger Numerator { get; set; }
        public BigInteger Denominator { get; set; }

        public Fraction()
        {
            Numerator = 0;
            Denominator = 1;
        }

        public Fraction(BigInteger numerator, BigInteger denominator)
        {
            if(denominator == 0)
                throw new ArgumentException("Denominator cannot be equal to 0!");

            Numerator = numerator;
            Denominator = denominator;
            Simplify();
        }

        public override string ToString()
        {
            return $"{(double) this}";
        }

        public static explicit operator double(Fraction fraction)
        {
            fraction.Simplify();
            return (double) fraction.Numerator / (double) fraction.Denominator;
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
            return new Fraction(a.Numerator * b.Numerator, a.Denominator * b.Denominator);
        }

        public static Fraction operator /(Fraction a, Fraction b)
        {
           return new Fraction(a.Numerator * b.Denominator, a.Denominator * b.Numerator);
        }

        public static Fraction operator +(Fraction a, Fraction b)
        {
            return new Fraction(
                a.Numerator * b.Denominator + b.Numerator * a.Denominator,
                a.Denominator * b.Denominator
            );
        }

        public static Fraction operator -(Fraction a)
        {
            return new Fraction(-a.Numerator, a.Denominator);
        }

        public static Fraction operator -(Fraction a, Fraction b)
        {
            return a + -b;
        }

        public static BigInteger GreatestCommonDivisor(BigInteger a, BigInteger b)
        {
            while (b != 0)
            {
                var temp = b;
                b = a % b;
                a = temp;
            }

            return a;
        }

        public static bool operator ==(Fraction left, Fraction right)
        {
            return Compare(left, right) == 0;
        }

        public static bool operator !=(Fraction left, Fraction right)
        {
            return !(left == right);
        }

        public static bool operator <(Fraction left, Fraction right)
        {
            return Compare(left, right) < 0;
        }

        public static bool operator >(Fraction left, Fraction right)
        {
            return Compare(left, right) > 0;
        }

        public static int Compare(Fraction left, Fraction right)
        {
            if (left.Numerator * right.Denominator == right.Numerator * left.Denominator)
                return 0;

            return left.Numerator * right.Denominator > right.Numerator * left.Denominator ? 1 : -1;
        }

        public override bool Equals(object obj)
        {
            var fraction = obj as Fraction;
            return fraction != null &&
                   Numerator.Equals(fraction.Numerator) &&
                   Denominator.Equals(fraction.Denominator);
        }

        public override int GetHashCode()
        {
            var hashCode = -1534900553;
            hashCode = hashCode * -1521134295 + EqualityComparer<BigInteger>.Default.GetHashCode(Numerator);
            hashCode = hashCode * -1521134295 + EqualityComparer<BigInteger>.Default.GetHashCode(Denominator);
            return hashCode;
        }
    }
}
