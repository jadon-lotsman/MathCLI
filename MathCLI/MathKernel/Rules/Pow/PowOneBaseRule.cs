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
    public class PowOneBaseRule : TermRule<Power>
    {
        public override string Name => "1 ^ x -> 1";
        protected override Term? Apply(Power pow)
        {
            if (pow.Args[0] is Number lOne && lOne.Value.IsOne) return new Number(Fraction.One);
            return null;
        }
    }
}
