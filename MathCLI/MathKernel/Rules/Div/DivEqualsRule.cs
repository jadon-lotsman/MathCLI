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
    public class DivEqualsRule : TermRule<Division>
    {
        public override string Name => "x / x -> 1";
        protected override Term? Apply(Division div)
        {
            if (div.Args[0] == div.Args[1]) return new Number(Fraction.One);
            return null;
        }
    }
}
