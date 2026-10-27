using MathCLI.MathKernel.Numbers;
using MathCLI.MathKernel.Terms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel.Rules.Div
{
    public class DivOneRule : TermRule<Division>
    {
        public override string Name => "x / 1 -> x";
        protected override Term? Apply(Division div)
        {
            if (div.Args[1] is Number rOne && rOne.Value.IsOne) return div.Args[0];
            return null;
        }
    }
}
