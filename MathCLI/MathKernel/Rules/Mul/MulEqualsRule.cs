using MathCLI.MathKernel.Numbers;
using MathCLI.MathKernel.Terms;
using MathCLI.MathNumerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel.Rules.Mul
{
    public class MulEqualsRule : TermRule<Multiplication>
    {
        public override string Name => "x * x -> x^2";
        protected override Term? Apply(Multiplication mul)
        {
            if (mul.Args[0] == mul.Args[1])
                return new Power(mul.Args[0], new Number(Fraction.Two));
            return null;
        }
    }
}
