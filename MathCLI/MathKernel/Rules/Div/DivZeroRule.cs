using MathCLI.MathKernel.Numbers;
using MathCLI.MathKernel.Terms;
using MathCLI.MathNumerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel.Rules.Div
{
    public class DivZeroRule : TermRule<Division>
    {
        public override string Name => "0 / x -> 0";
        protected override Term? Apply(Division div)
        {
            if (div.Args[0] is Number lZero && lZero.Value.IsZero) return new Number(Fraction.Zero);
            return null;
        }
    }
}
