using MathCLI.MathKernel.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel.Terms
{
    public class Multiplication : BinaryTerm
    {
        public override int Precedence => 3;
        public Multiplication(ITerm left, ITerm right) : base(left, '*', right) { }

        public override Fraction Execute(VariableContext context)
        {
            return Left.Execute(context) * Right.Execute(context);
        }


        public override ITerm Simplify()
        {
            Left = Left.Simplify();
            Right = Right.Simplify();

            // x * 0 → 0,  0 * x → 0
            if (Right is Fraction r0 && r0.IsZero) return Fraction.Zero;
            if (Left is Fraction l0 && l0.IsZero) return Fraction.Zero;

            // x * 1 → x,  1 * x → x
            if (Right is Fraction r1 && r1.IsOne) return Left;
            if (Left is Fraction l1 && l1.IsOne) return Right;

            // -1 * x → -x,  x * (-1) → -x
            var minusOne = new Fraction(-1, 1);
            if (Right is Fraction rm && rm == minusOne) return new NegateTerm(Left);
            if (Left is Fraction lm && lm == minusOne) return new NegateTerm(Right);

            // a * b → a*b
            if (Left is Fraction a && Right is Fraction b)
                return a * b;

            // x * x → x^2
            if (Left == Right)
                return new Power([Left, new Fraction(2, 1)]);

            return this;
        }
    }
}
