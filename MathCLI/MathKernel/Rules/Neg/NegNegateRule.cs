using MathCLI.MathKernel.Terms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel.Rules.Neg
{
    public class NegNegateRule : TermRule<NegateTerm>
    {
        public override string Name => "-(-x) -> x";
        protected override Term? Apply(NegateTerm negate)
        {
            if (negate.Args[0] is NegateTerm inner) return inner.Args[0];
            return null;
        }
    }
}
