using MathCLI.MathKernel.Terms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel.Rules.Neg
{
    public class NegDivisionRule : TermRule<NegateTerm>
    {
        public override string Name => "-(a / b) -> (-a) / b";
        protected override Term? Apply(NegateTerm negate)
        {
            if (negate.Args[0] is Division div)
                return new Division(new NegateTerm(div.Args[0]), div.Args[1]);
            return null;
        }
    }
}
