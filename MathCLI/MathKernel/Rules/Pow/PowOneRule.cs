using MathCLI.MathKernel.Numbers;
using MathCLI.MathKernel.Terms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel.Rules.Pow
{
    public class PowOneRule : TermRule<Power>
    {
        public override string Name => "x ^ 1 -> x";
        protected override Term? Apply(Power pow)
        {
            if (pow.Args[1] is Number rOne && rOne.Value.IsOne) return pow.Args[0];
            return null;
        }
    }
}
