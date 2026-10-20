using MathCLI.MathKernel.Numbers;
using MathCLI.MathKernel.Terms;
using MathCLI.MathKernel.Variables;
using MathCLI.MathNumerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel.TermVisitors
{
    public class SimplifyVisitor : ITermVisitor<ITerm>
    {
        private readonly VariableContext? _variables;

        public SimplifyVisitor(VariableContext? variables=null)
        {
            _variables = variables;
        }


        public ITerm Visit(Number number) => number;
        public ITerm Visit(Variable variable) => _variables != null && _variables.TryGetVariable(variable.Name, out var fraction) ? new Number(fraction!) : variable;


        public ITerm Visit(NegateTerm negate)
        {
            var term = negate.InnerTerm.Accept(this);

            // -(-x) → x
            if (term is NegateTerm inner)
                return inner.InnerTerm;

            // -(a) → -a
            if (term is Number a)
                return new Number(-a.Value);

            // -(a / b) → (-a) / b
            if (term is Division div)
                return new Division(new NegateTerm(div.Args[0]).Accept(this), div.Args[1]).Accept(this);

            // -(a * b) → (-a) * b
            if (term is Multiplication mul)
                return new Multiplication(new NegateTerm(mul.Args[0]).Accept(this), mul.Args[1]).Accept(this);

            return new NegateTerm(term);
        }

        public ITerm Visit(Addition addition)
        {
            var left = addition.Args[0].Accept(this);
            var right = addition.Args[1].Accept(this);

            // x + (-y) → x - y
            if (right is NegateTerm neg)
                return new Subtraction(left, neg.InnerTerm).Accept(this);

            // x + 0 → x
            if (right is Number rZero && rZero.Value.IsZero) return left;

            // 0 + x → x
            if (left is Number lZero && lZero.Value.IsZero) return right;

            // 2 + 3 → 5
            if (left is Number a && right is Number b)
                return new Number(a.Value + b.Value);

            // x + x → 2x
            if (left is Variable lVar && right is Variable rVar && lVar.Name == rVar.Name)
                return new Multiplication(new Number(Fraction.Two), left).Accept(this);

            return new Addition(left, right);
        }

        public ITerm Visit(Subtraction subtraction)
        {
            var left = subtraction.Args[0].Accept(this);
            var right = subtraction.Args[1].Accept(this);

            // a - (-b) → a + b
            if (right is NegateTerm neg)
                return new Addition(left, neg.InnerTerm).Accept(this);

            // x - 0 → x
            if (right is Number rZero && rZero.Value.IsZero)
                return left;

            // 0 - x → -x
            if (left is Number lZero && lZero.Value.IsZero)
                return new NegateTerm(right).Accept(this);

            // x - x → 0
            if (left is Variable lVar && right is Variable rVar && lVar.Name == rVar.Name)
                return new Number(Fraction.Zero);

            // a - b → a-b
            if (left is Number a && right is Number b)
                return new Number(a.Value - b.Value);

            return new Subtraction(left, right);
        }


        public ITerm Visit(Multiplication multiplication)
        {
            var left = multiplication.Args[0].Accept(this);
            var right = multiplication.Args[1].Accept(this);

            // x * 0 → 0,  0 * x → 0
            if (right is Number rZero && rZero.Value.IsZero) return new Number(Fraction.Zero);
            if (left is Number lZero && lZero.Value.IsZero) return new Number(Fraction.Zero);

            // x * 1 → x,  1 * x → x
            if (right is Number rOne && rOne.Value.IsOne) return left;
            if (left is Number lOne && lOne.Value.IsOne) return right;

            // -1 * x → -x,  x * (-1) → -x
            if (right is Number rNegative && rNegative.Value.IsNegativeOne)
                return new NegateTerm(left).Accept(this);

            if (left is Number lNegative && lNegative.Value.IsNegativeOne)
                return new NegateTerm(right).Accept(this);

            // a * b → a*b
            if (left is Number a && right is Number b)
                return new Number(a.Value * b.Value);

            // x * x → x^2
            if (left is Variable lVar && right is Variable rVar && lVar.Name == rVar.Name)
                return new Power(left, new Number(Fraction.Two)).Accept(this);

            return new Multiplication(left, right);
        }

        public ITerm Visit(Division division)
        {
            var left = division.Args[0].Accept(this);
            var right = division.Args[1].Accept(this);

            // b == 0 → ex
            if (right is Number rZero && rZero.Value.IsZero)
                throw new DivideByZeroException();

            // x / 1 → x
            if (right is Number rOne && rOne.Value.IsOne) return left;

            // 0 / x → 0  
            if (left is Number lZero && lZero.Value.IsZero)
                return new Number(Fraction.Zero);

            // x / x → 1 
            if (left is Variable lVar && right is Variable rVar && lVar.Name == rVar.Name)
                return new Number(Fraction.One);

            // a / b → a/b
            if (left is Number a && right is Number b)
                return new Number(a.Value / b.Value);

            return new Division(left, right);
        }


        public ITerm Visit(Power power)
        {
            var left = power.Args[0].Accept(this);
            var right = power.Args[1].Accept(this);

            // x ^ 0 → 1
            if (right is Number rZero && rZero.Value.IsZero)
                return new Number(Fraction.One);

            // x ^ 1 → x
            if (right is Number rOne && rOne.Value.IsOne) return left;

            // 1 ^ x → 1
            if (left is Number lOne && lOne.Value.IsOne)
                return new Number(Fraction.One);

            // 0 ^ x → 0
            if (left is Number lZero && lZero.Value.IsZero)
                return new Number(Fraction.Zero);

            // a ^ b → a^b
            if (left is Number a && right is Number b && b.Value.IsInteger)
            {
                if (b.Value.Numerator > int.MaxValue || b.Value.Numerator < int.MinValue)
                    return new Power(left, right);

                return new Number(a.Value.Pow((int)b.Value.Numerator));
            }

            return new Power(left, right);
        }
    }
}
