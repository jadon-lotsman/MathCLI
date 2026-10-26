using MathCLI.MathKernel.Numbers;
using MathCLI.MathKernel.Terms;
using MathCLI.MathNumerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel.Rules
{
    public class AddEqualsRule : TermRule<Addition>
    {
        public override string Name => "x + x -> 2 * x";
        protected override Term? Apply(Addition add)
        {
            if (add.Args[0] == add.Args[1]) return new Multiplication(new Number(Fraction.Two), add.Args[0]);
            return null;
        }
    }
}
