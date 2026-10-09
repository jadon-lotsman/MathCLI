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
        public static explicit operator long(Fraction value)    => value.Numerator / value.Denominator;


        public bool Equals(Fraction other) =>
            Numerator * other.Denominator == other.Numerator * Denominator;
        public int CompareTo(Fraction other) =>
            (Numerator * other.Denominator).CompareTo(other.Numerator * Denominator);

        public static bool operator ==(Fraction a, Fraction b)  => a.Equals(b);
        public static bool operator !=(Fraction a, Fraction b)  => !a.Equals(b);
        public static bool operator >(Fraction a, Fraction b)   => a.CompareTo(b) > 0;
        public static bool operator <(Fraction a, Fraction b)   => a.CompareTo(b) < 0;


        public static Fraction operator +(Fraction f) =>
            new(f.Numerator, f.Denominator);
        public static Fraction operator -(Fraction f) =>
            new(-f.Numerator, f.Denominator);
        public static Fraction operator +(Fraction a, Fraction b) =>
            new(a.Numerator * b.Denominator + b.Numerator * a.Denominator, a.Denominator * b.Denominator);
        public static Fraction operator -(Fraction a, Fraction b) =>
            new(a.Numerator * b.Denominator - b.Numerator * a.Denominator, a.Denominator * b.Denominator);


        public static Fraction operator *(Fraction a, long i) =>
            new(a.Numerator * i, a.Denominator);
        public static Fraction operator /(Fraction a, long i) =>
            new(a.Numerator, a.Denominator * i);
        public static Fraction operator *(Fraction a, Fraction b) =>
            new(a.Numerator * b.Numerator, a.Denominator * b.Denominator);
        public static Fraction operator /(Fraction a, Fraction b) =>
            b.Denominator == 1 ? a / b.Numerator : a * b.GetReciprocal();
    }
}
