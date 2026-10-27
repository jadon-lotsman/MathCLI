using MathCLI.MathKernel.Numbers;
using MathCLI.MathKernel.Terms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel.Rules.Mul
{
    public class MulNegativeOneRule : TermRule<Multiplication>
    {
        public override string Name => "-1 * x -> -x, x * (-1) -> -x";
        protected override Term? Apply(Multiplication mul)
        {
            if (mul.Args[1] is Number rNeg && rNeg.Value.IsNegativeOne)
                return new NegateTerm(mul.Args[0]);
            if (mul.Args[0] is Number lNeg && lNeg.Value.IsNegativeOne)
                return new NegateTerm(mul.Args[1]);
            return null;
        }
    }
}
