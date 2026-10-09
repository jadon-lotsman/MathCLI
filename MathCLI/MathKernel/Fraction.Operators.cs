using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel
{
    public partial class Fraction : ITerm, IEquatable<Fraction>
    {
        public static implicit operator double(Fraction value) => (double)value.Numerator / value.Denominator;
        public static explicit operator long(Fraction value) => value.Numerator / value.Denominator;


        public bool Equals(Fraction? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Numerator * other.Denominator == other.Numerator * Denominator;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Fraction);
        }

        public override int GetHashCode()
        {
            var gcd = Fraction.FindGCD(Numerator, Denominator);
            return HashCode.Combine(Numerator / gcd, Denominator / gcd);
        }

        public int CompareTo(Fraction? other)
        {
            if (other is null) return 1;
            return (Numerator * other.Denominator).CompareTo(other.Numerator * Denominator);
        }

        
        public static bool operator ==(Fraction? a, Fraction? b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (a is null || b is null) return false;
            return a.Equals(b);
        }
        public static bool operator !=(Fraction? a, Fraction? b) => !(a == b);
        public static bool operator >(Fraction? a, Fraction? b) => a!.CompareTo(b) > 0;
        public static bool operator <(Fraction? a, Fraction? b) => a!.CompareTo(b) < 0;


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
