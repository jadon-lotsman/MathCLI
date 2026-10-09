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
    public partial class Fraction : ITerm
    {
        public long Numerator { get; private set; }
        public long Denominator { get; private set; }
        public int Precedence => int.MaxValue;
        public bool IsValue => true;

        public Fraction(long numerator, long denominator)
        {
            if (denominator == 0)
                throw new DivideByZeroException();

            Numerator   = numerator;
            Denominator = denominator;

            if (Denominator < 0)
            {
                Numerator   = -Numerator;
                Denominator = -Denominator;
            }
        }

        public Fraction(double value)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
                throw new ArgumentException("Value must be finite", nameof(value));

            if (value == 0.0)
            {
                Numerator = 0;
                Denominator = 1;
                return;
            }

            int precision = (int)Math.Log10(int.MaxValue) - 1;
            int busy = (int)Math.Log10(value) + 1;

            Denominator = 1;
            for (int i = busy; i < precision; i++)
            {
                value *= 10;
                Denominator *= 10;
            }

            Numerator = (int)value;

            Fraction f = GetReduced();
            Numerator = f.Numerator;
            Denominator = f.Denominator;
        }

        public Fraction Execute(VariableContext context) => this;
        public ITerm ReduceStep(VariableContext context) => this;
        public ITerm Substitute(VariableContext context) => this;


        public Fraction Pow(int i)
        {
            if (i < 0)
                return GetReciprocal().Pow(-i);

            long num = 1, den = 1;
            for (int k = 0; k < i; k++)
            {
                num *= Numerator;
                den *= Denominator;
            }

            return new Fraction((int)num, (int)den).GetReduced();
        }

        public Fraction GetReduced()
        {
            if (Numerator == 0)
                return new Fraction(0, 1);

            var gcd = FindGCD(Numerator, Denominator);
            return new Fraction(Numerator / gcd, Denominator / gcd);
        }

        public Fraction GetReciprocal()
        {
            if (Numerator == 0)
                throw new DivideByZeroException();

            return new Fraction(Denominator, Numerator);
        }

        public static long FindGCD(long a, long b)
        {
            a = Math.Abs(a);
            b = Math.Abs(b);

            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }

            return a | b;
        }

        public static bool TryParseFraction(string? str, out Fraction? fraction)
        {
            fraction = null;

            if (string.IsNullOrWhiteSpace(str))
                return false;

            str = str.Trim();
            if (str.Contains('/'))
            {
                var splitted = str.Split('/');

                if (splitted.Length != 2)
                    return false;

                if (!int.TryParse(splitted[0], CultureInfo.InvariantCulture, out int numerator))
                    return false;

                if (!int.TryParse(splitted[1], CultureInfo.InvariantCulture, out int denominator))
                    return false;

                if (denominator == 0)
                    return false;

                fraction = new Fraction(numerator, denominator);
            }
            else
            {
                if (!double.TryParse(str, NumberStyles.Float, CultureInfo.InvariantCulture, out double number))
                    return false;

                fraction = new Fraction(number);
            }

            return true;
        }

        public override string ToString()
        {
            if (Denominator == 0)
                return "0";

            if (Numerator == Denominator)
                return "1";

            if (Denominator == 1)
                return $"{Numerator}";

            else if (Denominator % 10 == 0)
                return $"{(double)this}";

            return $"{Numerator}/{Denominator}";
        }
    }
}
