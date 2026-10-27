using MathCLI.MathKernel.Numbers;
using MathCLI.MathKernel.Terms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel.Rules.Div
{
    public class DivNumbersRule : TermRule<Division>
    {
        public override string Name => "a / b -> c";
        protected override Term? Apply(Division div)
        {
            if (div.Args[0] is Number a && div.Args[1] is Number b)
                return new Number(a.Value / b.Value);
            return null;
        }
    }
}
