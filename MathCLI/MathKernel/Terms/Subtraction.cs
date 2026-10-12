using MathCLI.MathKernel.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel.Terms
{
    public class Subtraction : BinaryTerm
    {
        public override int Precedence { get => 1; }
        public Subtraction(ITerm left, ITerm right) : base(left, '-', right) { }

        public override Fraction Execute(VariableContext context)
        {
            return Left.Execute(context) - Right.Execute(context);
        }

        public override ITerm Simplify()
        {
            Left = Left.Simplify();
            Right = Right.Simplify();

            // a - (-b) → a + b
            if (Right is NegateTerm neg)
                return new Addition(Left, neg.Term).Simplify();

            // x - 0 → x
            if (Right is Fraction r0 && r0.IsZero) return Left;

            // 0 - x → -x
            if (Left is Fraction l0 && l0.IsZero) return new NegateTerm(Right);

            // x - x → 0
            if (Left.Equals(Right)) return new Fraction(0, 1);

            // a - b → a-b
            if (Left is Fraction a && Right is Fraction b)
                return a - b;

            return this;
        }
    }
}
