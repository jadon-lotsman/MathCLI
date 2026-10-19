using MathCLI.MathKernel.Terms;
using MathCLI.MathKernel.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel.TermVisitors
{
    public class SimplifyVisitor : ITermVisitor
    {
        private VariableContext? _variables;

        public SimplifyVisitor(VariableContext? variables=null)
        {
            _variables = variables;
        }


        public ITerm Visit(Fraction fraction) => fraction;
        public ITerm Visit(Variable variable) => _variables != null && _variables.TryGetVariable(variable.Name, out var fraction) ? fraction! : variable;


        public ITerm Visit(NegateTerm negate)
        {
            var term = negate.Term.Accept(this);

            // -(-x) → x
            if (term is NegateTerm inner)
                return inner.Term;

            // -(a) → -a
            if (term is Fraction a)
                return -a;

            return new NegateTerm(term);
        }

        public ITerm Visit(Addition addition)
        {
            var left = addition.Args[0].Accept(this);
            var right = addition.Args[1].Accept(this);

            // x + (-y) → x - y
            if (right is NegateTerm neg)
                return new Subtraction(left, neg.Term).Accept(this);

            // x + 0 → x
            if (right is Fraction rZero && rZero.IsZero) return left;

            // 0 + x → x
            if (left is Fraction lZero && lZero.IsZero) return right;

            // 2 + 3 → 5
            if (left is Fraction a && right is Fraction b)
                return a + b;

            // x + x → 2x
            if (left is Variable && right is Variable && left == right)
                return new Multiplication(new Fraction(2, 1), left);

            return new Addition(left, right);
        }

        public ITerm Visit(Subtraction subtraction)
        {
            var left = subtraction.Args[0].Accept(this);
            var right = subtraction.Args[1].Accept(this);

            // a - (-b) → a + b
            if (right is NegateTerm neg)
                return new Addition(left, neg.Term).Accept(this);

            // x - 0 → x
            if (right is Fraction rZero && rZero.IsZero) return left;

            // 0 - x → -x
            if (left is Fraction lZero && lZero.IsZero) return new NegateTerm(right);

            // x - x → 0
            if (left == right) return Fraction.Zero;

            // a - b → a-b
            if (left is Fraction a && right is Fraction b)
                return a - b;

            return new Subtraction(left, right);
        }


        public ITerm Visit(Multiplication multiplication)
        {
            var left = multiplication.Args[0].Accept(this);
            var right = multiplication.Args[1].Accept(this);

            // x * 0 → 0,  0 * x → 0
            if (right is Fraction rZero && rZero.IsZero) return Fraction.Zero;
            if (left is Fraction lZero && lZero.IsZero) return Fraction.Zero;

            // x * 1 → x,  1 * x → x
            if (right is Fraction rOne && rOne.IsOne) return left;
            if (left is Fraction lOne && lOne.IsOne) return right;

            // -1 * x → -x,  x * (-1) → -x
            var minusOne = new Fraction(-1, 1);
            if (right is Fraction rMinus && rMinus == minusOne) return new NegateTerm(left);
            if (left is Fraction lMinus && lMinus == minusOne) return new NegateTerm(right);

            // a * b → a*b
            if (left is Fraction a && right is Fraction b)
                return a * b;

            // x * x → x^2
            if (left is Variable lVar && right is Variable rVar && lVar.Name == rVar.Name)
                return new Power(left, new Fraction(2, 1));

            return new Multiplication(left, right);
        }

        public ITerm Visit(Division division)
        {
            var left = division.Args[0].Accept(this);
            var right = division.Args[1].Accept(this);

            // b == 0 → ex
            if (right is Fraction rZero && rZero.IsZero)
                throw new DivideByZeroException();

            // x / 1 → x
            if (right is Fraction rOne && rOne.IsOne) return left;

            // 0 / x → 0  
            if (left is Fraction lZero && lZero.IsZero) return Fraction.Zero;

            // x / x → 1 
            if (left == right) return Fraction.One;

            // a / b → a/b
            if (left is Fraction a && right is Fraction b)
                return a / b;

            return new Division(left, right);
        }


        public ITerm Visit(Power power)
        {
            var left = power.Args[0].Accept(this);
            var right = power.Args[1].Accept(this);

            // x ^ 0 → 1
            if (right is Fraction rZero && rZero.IsZero) return Fraction.One;

            // x ^ 1 → x
            if (right is Fraction rOne && rOne.IsOne) return left;

            // 1 ^ x → 1
            if (left is Fraction lOne && lOne.IsOne) return Fraction.One;

            // 0 ^ x → 0
            if (left is Fraction lZero && lZero.IsZero) return Fraction.Zero;

            // a ^ b → a^b
            if (left is Fraction a && right is Fraction b)
            {
                if (b.Numerator > int.MaxValue || b.Numerator < int.MinValue)
                    return new Power(left, right);

                return a.Pow((int)b.Numerator);
            }

            return new Power(left, right);
        }
    }
}
