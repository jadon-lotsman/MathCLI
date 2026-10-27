using MathCLI.MathKernel.Numbers;
using MathCLI.MathKernel.Terms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel.Rules.Pow
{
    public class PowNumbersRule : TermRule<Power>
    {
        public override string Name => "a ^ b -> c";
        protected override Term? Apply(Power pow)
        {
            if (pow.Args[0] is not Number a || pow.Args[1] is not Number b) return null;
            if (!b.Value.IsInteger) return null;

            if (b.Value.Numerator > int.MaxValue || b.Value.Numerator < int.MinValue)
                return null;

            return new Number(a.Value.Pow((int)b.Value.Numerator));
        }
    }
}
