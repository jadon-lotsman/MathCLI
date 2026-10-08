using MathCLI.MathKernel.Variables;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel
{
    public class Fraction : ITerm
    {
        public int Numerator { get; private set; }
        public int Denominator { get; private set; }
        public int Precedence { get => Numerator < 0 ? int.MinValue : int.MaxValue; }
        public bool IsValue { get => true; }

        public Fraction(int numerator, int denominator = 1)
        {
            Numerator = numerator;
            Denominator = denominator;
        }

        public Fraction(double value)
        {
            int precision = (int)Math.Log10(int.MaxValue) - 1;
            int busy = (int)Math.Log10(value) + 1;

            Denominator = 1;
            for (int i = busy; i < precision; i++)
            {
                value *= 10;
                Denominator *= 10;
            }

            Numerator = (int)value;

            Fraction f = this.Simplify();
            Numerator = f.Numerator;
            Denominator = f.Denominator;
        }

        public Fraction Execute(VariableContext context) => this;
        public ITerm ReduceStep(VariableContext context) => this;
        public ITerm Substitute(VariableContext context) => this;

        public static implicit operator double(Fraction value)
        {
            return (double)value.Numerator / value.Denominator;
        }

        public static explicit operator int(Fraction value)
        {
            return (int)value.Numerator / value.Denominator;
        }

        public static bool operator ==(Fraction a, Fraction b)
        {
            int first = a.Denominator * b.Numerator;
            int second = b.Denominator * a.Numerator;
            return first == second;
        }

        public static bool operator !=(Fraction a, Fraction b)
        {
            return !(a == b);
        }

        public static bool operator >(Fraction a, Fraction b)
        {
            return a - b > 0;
        }

        public static bool operator <(Fraction a, Fraction b)
        {
            return a - b < 0;
        }

        public static Fraction operator +(Fraction f)
        {
            return f;
        }

        public static Fraction operator -(Fraction f)
        {
            return new Fraction(-f.Numerator, f.Denominator);
        }

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

        public static Fraction operator *(Fraction a, Fraction b)
        {
            int numerator = a.Numerator * b.Numerator;
            int denominator = a.Denominator * b.Denominator;

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
            {
                return a / b.Numerator;
            }

            return a * b.Flip();
        }


        public Fraction Flip()
        {
            return new Fraction(Denominator, Numerator);
        }

        public Fraction Simplify()
        {
            if (Numerator == 0)
            {
                return new Fraction(0);
            }

            int gcd = GCD();

            int numerator = Numerator / gcd;
            int denominator = Denominator / gcd;

            return new Fraction(numerator, denominator);
        }

        public int GCD()
        {
            int a = Numerator;
            int b = Denominator;

            if (a < 0)
            {
                a = -a;
            }
            if (b < 0)
            {
                b = -b;
            }

            while (a != 0 && b != 0)
            {
                if (a > b)
                {
                    a %= b;
                }
                else
                {
                    b %= a;
                }
            }

            return a | b;
        }


        public static bool TryParseFraction(string str, out Fraction? fr)
        {
            fr = null;

            if (string.IsNullOrWhiteSpace(str))
                return false;

            if (str.Contains("/"))
            {
                var splitted = str.Split('/');

                if (splitted.Length != 2)
                    return false;

                if (!int.TryParse(splitted[0], CultureInfo.InvariantCulture, out int numerator))
                    return false;

                if (!int.TryParse(splitted[1], CultureInfo.InvariantCulture, out int denominator))
                    return false;

                fr = new Fraction(numerator, denominator);
            }
            else
            {
                if (!double.TryParse(str, NumberStyles.Float, CultureInfo.InvariantCulture, out double number))
                    return false;

                fr = new Fraction(number);
            }

            return true;
        }

        public override string ToString()
        {
            if (Denominator == 1)
                return $"{Numerator}";

            else if (Denominator % 10 == 0)
                return $"{(double)this}";

            return $"{Numerator}/{Denominator}";
        }
    }
}
