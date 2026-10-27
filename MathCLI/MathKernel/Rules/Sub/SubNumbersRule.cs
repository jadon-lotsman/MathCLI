using MathCLI.MathKernel.Numbers;
using MathCLI.MathKernel.Terms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel.Rules.Sub
{
    public class SubNumbersRule : TermRule<Subtraction>
    {
        public override string Name => "a - b -> c";
        protected override Term? Apply(Subtraction sub)
        {
            if (sub.Args[0] is Number a && sub.Args[1] is Number b) return new Number(a.Value - b.Value);
            return null;
        }
    }
}
