using MathCLI.MathKernel.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel.Terms
{
    public class Division : BinaryTerm
    {
        public override int Precedence { get => 3; }
        public Division(ITerm left, ITerm right) : base(left, ':', right) { }

        public override Fraction Execute(VariableContext context)
        {
            return Left.Execute(context) / Right.Execute(context);
        }

        public override ITerm Simplify()
        {
            Left = Left.Simplify();
            Right = Right.Simplify();

            if (Right is Fraction rZero && rZero.IsZero)
                throw new DivideByZeroException();

            // x / 1 → x
            if (Right is Fraction r1 && r1.IsOne) return Left;

            // 0 / x → 0  
            if (Left is Fraction l0 && l0.IsZero) return Fraction.Zero;

            // x / x → 1 
            if (Left == Right) return Fraction.One;

            // a / b → a/b
            if (Left is Fraction a && Right is Fraction b)
                return a / b;

            return this;
        }
    }
}
