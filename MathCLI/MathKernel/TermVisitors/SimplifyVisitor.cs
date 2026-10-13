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
        public ITerm Visit(Variable variable) => _variables == null? variable : _variables.GetVariable(variable.Name);


        public ITerm Visit(NegateTerm negate)
        {
            var term = negate.Term.Accept(this);

            if (term is Fraction a)
                return -a;

            return new NegateTerm(term);
        }

        public ITerm Visit(Addition addition)
        {
            var left = addition.Left.Accept(this);
            var right = addition.Right.Accept(this);

            if (right is NegateTerm neg)
                return new Subtraction(left, neg.Term).Accept(this);

            if (right is Fraction r && r.IsZero) return left;

            if (left is Fraction l && l.IsZero) return right;

            if (left is Fraction a && right is Fraction b)
                return a + b;

            if (left is Variable && right is Variable && left == right)
                return new Multiplication(new Fraction(2, 1), left);

            return new Addition(left, right);
        }

        public ITerm Visit(Subtraction subtraction)
        {
            var left = subtraction.Left.Accept(this);
            var right = subtraction.Right.Accept(this);

            // a - (-b) → a + b
            if (right is NegateTerm neg)
                return new Addition(left, neg.Term).Accept(this);

            // x - 0 → x
            if (right is Fraction r0 && r0.IsZero) return left;

            // 0 - x → -x
            if (left is Fraction l0 && l0.IsZero) return new NegateTerm(right);

            // x - x → 0
            if (left == right) return Fraction.Zero;

            // a - b → a-b
            if (left is Fraction a && right is Fraction b)
                return a - b;

            return new Subtraction(left, right);
        }


        public ITerm Visit(Multiplication multiplication)
        {
            var left = multiplication.Left.Accept(this);
            var right = multiplication.Right.Accept(this);

            // x * 0 → 0,  0 * x → 0
            if (right is Fraction r0 && r0.IsZero) return Fraction.Zero;
            if (left is Fraction l0 && l0.IsZero) return Fraction.Zero;

            // x * 1 → x,  1 * x → x
            if (right is Fraction r1 && r1.IsOne) return left;
            if (left is Fraction l1 && l1.IsOne) return right;

            // -1 * x → -x,  x * (-1) → -x
            var minusOne = new Fraction(-1, 1);
            if (right is Fraction nr && nr == minusOne) return new NegateTerm(left);
            if (left is Fraction nl && nl == minusOne) return new NegateTerm(right);

            // a * b → a*b
            if (left is Fraction a && right is Fraction b)
                return a * b;

            // x * x → x^2
            if (left == right)
                return new Power(left, new Fraction(2, 1));

            return new Multiplication(left, right);
        }

        public ITerm Visit(Division division)
        {
            var left = division.Left.Accept(this);
            var right = division.Right.Accept(this);

            // b == 0 → ex
            if (right is Fraction rZero && rZero.IsZero)
                throw new DivideByZeroException();

            // x / 1 → x
            if (right is Fraction rOne && rOne.IsOne) return left;

            // 0 / x → 0  
            if (left is Fraction l0 && l0.IsZero) return Fraction.Zero;

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

            if (right is Fraction rZero && rZero.IsZero) return Fraction.One;
            if (right is Fraction rOne && rOne.IsOne) return left;
            if (left is Fraction lOne && lOne.IsOne) return Fraction.One;
            if (left is Fraction lZero && lZero.IsZero) return Fraction.Zero;

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
