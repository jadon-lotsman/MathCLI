using MathCLI.MathKernel.Numbers;
using MathCLI.MathKernel.Terms;
using MathCLI.MathNumerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel.Rules.Pow
{
    public class PowZeroRule : TermRule<Power>
    {
        public override string Name => "x ^ 0 -> 1";
        protected override Term? Apply(Power pow)
        {
            if (pow.Args[1] is Number rZero && rZero.Value.IsZero) return new Number(Fraction.One);
            return null;
        }
    }
}
