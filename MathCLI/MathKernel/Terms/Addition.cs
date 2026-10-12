using MathCLI.MathKernel.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel.Terms
{
    public class Addition : BinaryTerm
    {
        public override int Precedence { get => 1; }
        public Addition(ITerm left, ITerm right) : base(left, '+', right) { }

        public override Fraction Execute(VariableContext context)
        {
            return Left.Execute(context) + Right.Execute(context);
        }

        public override ITerm Simplify()
        {
            Left = Left.Simplify();
            Right = Right.Simplify();

            // a + (-b) → a - b
            if (Right is NegateTerm neg)
                return new Subtraction(Left, neg.Term).Simplify();

            if (Right is Fraction r && r.IsZero) return Left;

            if (Left is Fraction l && l.IsZero) return Right;

            if (Left is Fraction a && Right is Fraction b)
                return a + b;

            if (Left is Variable && Right is Variable && Left == Right)
                return new Multiplication(new Fraction(2, 1), Left);

            return this;
        }
    }
}
