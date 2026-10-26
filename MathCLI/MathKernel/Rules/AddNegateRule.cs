using MathCLI.MathKernel.Terms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel.Rules
{
    public class AddNegateRule : TermRule<Addition>
    {
        public override string Name => "x + (-y) -> x - y";
        protected override Term? Apply(Addition add)
        {
            if (add.Args[1] is NegateTerm neg) return new Subtraction(add.Args[0], neg.Args[0]);
            return null;
        }
    }
}
