using MathCLI.MathKernel.Numbers;
using MathCLI.MathKernel.Terms;
using MathCLI.MathNumerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel.Rules.Sub
{
    public class SubEqualsRule : TermRule<Subtraction>
    {
        public override string Name => "x - x -> 0";
        protected override Term? Apply(Subtraction sub)
        {
            if (sub.Args[0] == sub.Args[1]) return new Number(Fraction.Zero);
            return null;
        }
    }
}
