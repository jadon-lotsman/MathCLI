using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel
{
    public partial class Fraction : ITerm
    {
        public static implicit operator double(Fraction value)  => (double)value.Numerator / value.Denominator;
        public static explicit operator int(Fraction value)     => value.Numerator / value.Denominator;


        public static bool operator ==(Fraction a, Fraction b)  => a.Denominator * b.Numerator == b.Denominator * a.Numerator;
        public static bool operator !=(Fraction a, Fraction b)  => !(a == b);
        public static bool operator >(Fraction a, Fraction b)   => a - b > 0;
        public static bool operator <(Fraction a, Fraction b)   => a - b < 0;



        public static Fraction operator +(Fraction a, Fraction b)
        {
            int numerator = a.Numerator * b.Denominator + b.Numerator * a.Denominator;
            int denominator = a.Denominator * b.Denominator;

            return new Fraction(numerator, denominator);
        }

        public static Fraction operator -(Fraction a, Fraction b)
        {
            return a + (-b);
        }

        public static Fraction operator +(Fraction f) => f;
        public static Fraction operator -(Fraction f) => new Fraction(-f.Numerator, f.Denominator);



        public static Fraction operator *(Fraction a, Fraction b)
        {
            int numerator = a.Numerator * b.Numerator;
            int denominator = a.Denominator * b.Denominator;

            return new Fraction(numerator, denominator);
        }

        public static Fraction operator *(Fraction a, int i)
        {
            int numerator = a.Numerator * i;
            int denominator = a.Denominator;

            return new Fraction(numerator, denominator);
        }

        public static Fraction operator /(Fraction a, int i)
        {
            a.Denominator *= i;

            return a;
        }

        public static Fraction operator /(Fraction a, Fraction b)
        {
            if (b.Denominator == 1)
                return a / b.Numerator;

            return a * b.GetReciprocal();
        }
    }
}
