using MathCLI.MathKernel.Numbers;
using MathCLI.MathKernel.Terms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel.Rules.Neg
{
    public class NegNumberRule : TermRule<NegateTerm>
    {
        public override string Name => "-(a) -> -a";
        protected override Term? Apply(NegateTerm negate)
        {
            if (negate.Args[0] is Number a) return new Number(-a.Value);
            return null;
        }
    }
}
