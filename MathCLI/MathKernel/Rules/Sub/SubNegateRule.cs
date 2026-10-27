using MathCLI.MathKernel.Terms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel.Rules.Sub
{
    public class SubNegateRule : TermRule<Subtraction>
    {
        public override string Name => "x - (-y) -> x + y";
        protected override Term? Apply(Subtraction sub)
        {
            if (sub.Args[1] is NegateTerm neg) return new Addition(sub.Args[0], neg.Args[0]);
            return null;
        }
    }
}
