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
    public class MulZeroRule : TermRule<Multiplication>
    {
        public override string Name => "x * 0 -> 0, 0 * x -> 0";
        protected override Term? Apply(Multiplication mul)
        {
            if (mul.Args[1] is Number rZero && rZero.Value.IsZero) return new Number(Fraction.Zero);
            if (mul.Args[0] is Number lZero && lZero.Value.IsZero) return new Number(Fraction.Zero);
            return null;
        }
    }
}
